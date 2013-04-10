using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.XPath;
using SDILReader;

namespace MSTest
{
    public class ScreenLib
    {
        static void WriteShallowNode(XmlReader reader, XmlWriter writer)
        {

            if (reader == null)
            {

                throw new ArgumentNullException("reader");

            }

            if (writer == null)
            {

                throw new ArgumentNullException("writer");

            }



            switch (reader.NodeType)
            {

                case XmlNodeType.Element:

                    writer.WriteStartElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);

                    writer.WriteAttributes(reader, true);

                    if (reader.IsEmptyElement)
                    {

                        writer.WriteEndElement();

                    }

                    break;

                case XmlNodeType.Text:

                    writer.WriteString(reader.Value);

                    break;

                case XmlNodeType.Whitespace:

                case XmlNodeType.SignificantWhitespace:

                    writer.WriteWhitespace(reader.Value);

                    break;

                case XmlNodeType.CDATA:

                    writer.WriteCData(reader.Value);

                    break;

                case XmlNodeType.EntityReference:

                    writer.WriteEntityRef(reader.Name);

                    break;

                case XmlNodeType.XmlDeclaration:

                case XmlNodeType.ProcessingInstruction:

                    writer.WriteProcessingInstruction(reader.Name, reader.Value);

                    break;

                case XmlNodeType.DocumentType:

                    writer.WriteDocType(reader.Name, reader.GetAttribute("PUBLIC"), reader.GetAttribute("SYSTEM"), reader.Value);

                    break;

                case XmlNodeType.Comment:

                    writer.WriteComment(reader.Value);

                    break;

                case XmlNodeType.EndElement:

                    writer.WriteFullEndElement();

                    break;

            }

        }

        public class ScreenConfiguration
        {
            public class EventArgsTextboxChecked: EventArgs
            {
                public string Frame;
                public string Screen;
                public string Component;
                public string Query;
                public int ComponentLength;
                public string ColumnName;
                public int ColumnLength;
            }
            public delegate void TextboxCheckedEventHandler(Object sender, EventArgsTextboxChecked e);
            public event TextboxCheckedEventHandler OnTextboxChecked;

            public class EventArgsSqlChecked : EventArgs
            {
                public string Screen;
                public string Query;
                public string ColumnName;
                public string AliasName;
            }
            public delegate void SqlCheckedEventHandler(Object sender, EventArgsSqlChecked e);
            public event SqlCheckedEventHandler OnSqlChecked;

            public class Screen
            {
                public delegate void TextboxCheckedEventHandler(Object sender, EventArgsTextboxChecked e);
                public event TextboxCheckedEventHandler OnTextboxChecked;
                public delegate void SqlCheckedEventHandler(Object sender, EventArgsSqlChecked e);
                public event SqlCheckedEventHandler OnSqlChecked;
                enum EnumSqlType
                {
                    Pre,
                    Post,
                    Undefined
                }

                internal class Sql: ISql
                {
                    private int _id;
                    private string _q;
                    private EnumSqlType _t;
                    public Sql(XmlReader xrSqls)
                    {
                        xrSqls.ReadToFollowing("id");
                        xrSqls.Read();
                        _id = int.Parse(xrSqls.Value);
                        xrSqls.ReadToFollowing("q");
                        xrSqls.Read();
                        _q = xrSqls.Value;
                        xrSqls.ReadToFollowing("t");
                        if (xrSqls.IsEmptyElement)
                        {
                            _t = EnumSqlType.Undefined;
                        }
                        else
                        {
                            xrSqls.Read();
                            if (xrSqls.Value == "")
                            {
                                _t = EnumSqlType.Undefined;
                            }
                            else
                            {
                                _t = (EnumSqlType)Enum.Parse(typeof(EnumSqlType), xrSqls.Value);
                            }
                        }
                    }

                    public string q
                    {
                        get { return _q; }
                    }

                    public int Id
                    {
                        get { return _id; }
                    }

                    public string ParseQuery(Hashtable objParams)
                    {
                        return ParseQuery(_q, objParams);
                    }

                    public static string ParseQuery(string strQueryText, Hashtable objParams)
                    {
                        foreach (var key in objParams.Keys)
                        {
                            string value;
                            if (objParams[key] != null)
                            {
                                value = objParams[key].ToString();
                            }
                            else
                            {
                                try
                                {
                                    value = BaseTest.Params[key.ToString().ToLower()].ToString();
                                }
                                catch (Exception e)
                                {
                                    throw new Exception("Exception value in Hashtable key define the following key: objParams.Add(\"" + key.ToString().ToLower() + "\", null);", e);
                                }
                            }
                            strQueryText = strQueryText.Replace(string.Format("${0}$", key), value);
                        }
                        return strQueryText;
                    }
                }
                class ComponentFactory
                {
                    public enum ComponentType
                    {
                        label,
                        textbox,
                        combobox,
                        checkbox,
                        button,
                        grid,
                        listcombo,
                        numericspin,
                        datetime,
                        signature,
                        symbolscanner,
                        webservice,
                        tomtom,
                        image,
                        duration,
                        empty
                    }
                    public static Component CreateComponent(XmlReader component, Frame frame)
                    {
                        component.ReadToFollowing("id");
                        component.Read();
                        string id = component.Value;
                        component.ReadToFollowing("type");
                        component.Read();
                        ComponentType componentType = (ComponentType)Enum.Parse(typeof(ComponentType), component.Value);
                        switch (componentType)
                        {
                            case ComponentType.label:
                            case ComponentType.checkbox:
                            case ComponentType.datetime:
                            case ComponentType.textbox:
                            case ComponentType.listcombo:
                            case ComponentType.signature:
                            case ComponentType.numericspin:
                            case ComponentType.symbolscanner:
                            case ComponentType.webservice:
                            case ComponentType.tomtom:
                            case ComponentType.image:
                            case ComponentType.duration:
                            case ComponentType.empty:
                                return new FVSimpleComponent(id, frame, component);
                            case ComponentType.button:
                                return new FVButton(id, frame, component);
                            case ComponentType.grid:
                                return new FVGrid(id, frame, component);
                            case ComponentType.combobox:
                                return new FVCombobox(id, frame, component);
                            default:
                                throw new ArgumentException("The component type " + componentType + " is not recognized.");
                        }
                    }

                    public static Component CreateComponent(XmlReader component, Assembly scripting, Frame frame)
                    {
                        component.ReadToFollowing("id");
                        component.Read();
                        string id = component.Value;
                        component.ReadToFollowing("type");
                        component.Read();
                        ComponentType componentType = (ComponentType)Enum.Parse(typeof(ComponentType), component.Value);
                        switch (componentType)
                        {
                            case ComponentType.label:
                            case ComponentType.checkbox:
                            case ComponentType.datetime:
                            case ComponentType.textbox:
                            case ComponentType.listcombo:
                            case ComponentType.signature:
                            case ComponentType.numericspin:
                            case ComponentType.symbolscanner:
                            case ComponentType.webservice:
                            case ComponentType.tomtom:
                            case ComponentType.image:
                            case ComponentType.duration:
                            case ComponentType.empty:
                                return new FVSimpleComponent(id, frame, component);
                            case ComponentType.button:
                                return new FVButton(id, frame, component, scripting);
                            case ComponentType.grid:
                                return new FVGrid(id, frame, component, scripting);
                            case ComponentType.combobox:
                                return new FVCombobox(id, frame, component);
                            default:
                                throw new ArgumentException("The component type " + componentType + " is not recognized.");
                        }
                    }
                }

                internal class FVSimpleComponent : Component
                {
                    public FVSimpleComponent(string sId, Frame parent, XmlReader component)
                    {
                        id = sId;
                        Parent = parent;
                    }

                    new Boolean HasScripting { get { return false; }}
                }

                internal class FVButton : Component
                {
                    private string _onClick;
                    private int? _onClickOpen;

                    public FVButton(string sId, Frame parent, XmlReader component)
                    {
                        id = sId;
                        Parent = parent;
                        component.ReadToFollowing("click");
                        if(!component.EOF)
                        {
                            component.Read();
                            _onClickOpen = int.Parse(component.Value);
                        }
                        component.MoveToFirstAttribute(); 
                    }

                    public FVButton(string sId, Frame parent, XmlReader component, Assembly scripting)
                    {
                        var stream = new MemoryStream(4096);
                        var writer = XmlTextWriter.Create(stream);
                        writer.WriteStartDocument();
                        writer.WriteStartElement("Component");
                        writer.WriteElementString("id", sId);
                        writer.WriteStartElement("type");
                        while (component.Read())
                        {
                            WriteShallowNode(component, writer);
                        }
                        writer.Flush();
                        stream.Position = 0;
                        var newReader = XmlReader.Create(stream);
                        id = sId;
                        Parent = parent;
                        newReader.ReadToFollowing("click");
                        if (!newReader.EOF)
                        {
                            newReader.Read();
                            _onClickOpen = int.Parse(newReader.Value);
                        }
                        stream.Position = 0;
                        component = XmlReader.Create(stream);
                        _onClick = ParseMethod(component, scripting, "OnClick");
                    }

                    public int? OnClickOpen
                    {
                        get {
                            return _onClickOpen;
                        }
                        set {
                            _onClickOpen = value;
                        }
                    }

                    public string OnClick()
                    {
                        return _onClick;
                    }
                }
                internal class FVGrid : Component
                {
                    private string _onRowClick;

                    public FVGrid(string sId, Frame parent, XmlReader component)
                    {
                        id = sId;
                        Parent = parent;
                        component.ReadToFollowing("querytext");
                        if (component.EOF)
                        {
                            querytext = "";
                        }
                        else
                        {
                            component.Read();
                            querytext = component.Value;
                        }
                    }

                    public FVGrid(string sId, Frame parent, XmlReader component, Assembly scripting)
                    {
                        id = sId;
                        Parent = parent;
                        component.ReadToFollowing("querytext");
                        if (component.EOF)
                        {
                            querytext = "";
                        }
                        else
                        {
                            component.Read();
                            querytext = component.Value;
                        }
                        _onRowClick = ParseMethod(component, scripting, "OnRowClick");
                    }
                }
                internal class FVCombobox : Component
                {
                    public FVCombobox(string sId, Frame parent, XmlReader component)
                    {
                        id = sId;
                        Parent = parent;
                        component.ReadToFollowing("querytext");
                        if (component.EOF)
                        {
                            querytext = "";
                        }
                        else
                        {
                            component.Read();
                            querytext = component.Value;
                        }
                    }
                    new Boolean HasScripting { get { return false; } }
                }

                public abstract class Component
                {
                    private Frame _parent;
                    protected string _scriptingId;
                    public bool blnIsTextbox;
                    public bool blnIsGridOrCombobox;
                    public int maxlength;
                    public string querytext;
                    private int _id;

                    public delegate void SqlCheckedEventHandler(Object sender, EventArgsSqlChecked e);
                    public event SqlCheckedEventHandler OnSqlChecked;

                    protected Component()
                    {
                    }

                    protected void ParseComponent(XmlReader xrComponent)
                    {
                        if (blnIsTextbox)
                        {
                            xrComponent.ReadToFollowing("maxlength");
                            if (xrComponent.EOF)
                            {
                                maxlength = 50;
                            }
                            else
                            {
                                xrComponent.Read();
                                maxlength = int.Parse(xrComponent.Value);
                            }
                        }
                        if (blnIsGridOrCombobox)
                        {
                            
                        }
                    }

                    protected Frame Parent
                    {
                        get { return _parent; }
                        set { _parent = value; }
                    }

                    public string id { get; protected set; }
                    protected bool HasScripting { get; set; }

                    public int Id
                    {
                        get {
                            return _id;
                        }
                    }

                    public int ComponentId
                    {
                        get
                        {
                            if (_scriptingId == null) return 0;
                            var matches = new Regex(@"Fo[0-9]*_Fr[0-9]*_T[0-9]*_C(?<component>[0-9][0-9]*)").Matches(_scriptingId);
                              if(matches.Count==0) return 0;
                              return int.Parse(matches[0].Groups["component"].Captures[0].Value);
                        }
                        
                    }

                    public Collection<string> Querys;

                    public void CheckSql(Collection<ColumnInfo> items)
                    {
                        if (querytext == null) return;
                        var tables = (from a in items
                                  select new {a.TableName}).Distinct();
                        foreach (var table in tables)
                        {
                            Regex searchTable =
                            new Regex(table.TableName);
                            Regex queryVariables = new Regex(@"as (?<name>[^,\n]?[^,\n]*)(\n|,)");
                            if(searchTable.IsMatch(querytext))
                            {
                                //var variables = queryVariables.Matches(querytext);
                                var variables = from Match match in queryVariables.Matches(querytext)
                                                from Capture name in match.Groups["name"].Captures
                                                select name.Value;
                                if (variables.Count() > 0)
                                {
                                    foreach(var variable in variables)
                                    {
                                        //Check if allias is used as a column name in the table
                                        var tableFields = from fields in items
                                                          where (fields.TableName == table.TableName)
                                                          && (fields.ColumnName == variable)
                                                          select fields;
                                        foreach (var field in tableFields)
                                        {
                                            //Trigger event for suspected textboxes.
                                            if (new Regex(field.ColumnName).IsMatch(querytext))
                                            {
                                                OnSqlChecked(this, new EventArgsSqlChecked
                                                {
                                                    Query = querytext,
                                                    ColumnName = field.ColumnName,
                                                    AliasName = variable
                                                });
                                            }
                                        }
                                    }
                                }

                            }
                        }
                        
                    }

                    protected string ParseMethod(XmlReader component, Assembly scripting, string methodName)
                    {
                        if (_scriptingId == null)
                        {
                            component.ReadToFollowing("ScriptingID");
                            if (component.EOF)
                            {
                                _scriptingId = "";
                                HasScripting = false;
                                return "";
                            }
                            component.Read();
                            _scriptingId = component.Value;
                        }
                        var gt = scripting.GetModules()[0].GetType("Tensing.FieldVision.Scripting.GeneratedPlugin." + _scriptingId,true,true);
                        MethodInfo mi = gt.GetMethod(methodName);
                        using (MethodBodyReader objMethodBodyReader = new SDILReader.MethodBodyReader(mi))
                        {
                            objMethodBodyReader.OnSqlInScript += objMethodBodyReader_OnSqlInScript;
                            return objMethodBodyReader.GetBodyCode();
                        }
                    }
                    private void objMethodBodyReader_OnSqlInScript(object sender, MethodBodyReader.EventArgsSqlInScript e)
                    {
                        if(Querys == null) Querys = new Collection<string>();
                        Querys.Add(e.Operand.ToString());
                    }
                }

                public class Frame
                {
                    private Screen _parent;
                    private readonly string _id;
                    public readonly List<Component> Components;
                    public delegate void SqlCheckedEventHandler(Object sender, EventArgsSqlChecked e);
                    public event SqlCheckedEventHandler OnSqlChecked;

                    public Frame(XmlReader xrFrame, Screen parent)
                    {
                        _parent = parent;
                        Components = new List<Component>();
                        xrFrame.ReadToFollowing("id");
                        xrFrame.Read();
                        _id = xrFrame.Value;
                        while(xrFrame.ReadToFollowing("Component"))
                        {
                            XmlReader xrComponent = xrFrame.ReadSubtree();
                            Component objComponent = ComponentFactory.CreateComponent(xrComponent, this);
                            Components.Add(objComponent);
                            objComponent.OnSqlChecked += new Component.SqlCheckedEventHandler(ObjComponentOnSqlChecked);
                        }
                    }

                    public Frame(XmlReader reader, Assembly scripting, Screen parent)
                    {
                        _parent = parent;
                        Components = new List<Component>();
                        reader.ReadToFollowing("id");
                        reader.Read();
                        _id = reader.Value;
                        while (reader.ReadToFollowing("Component"))
                        {
                            XmlReader xrComponent = reader.ReadSubtree();
                            Component objComponent = ComponentFactory.CreateComponent(xrComponent, scripting, this);
                            Components.Add(objComponent);
                            objComponent.OnSqlChecked += new Component.SqlCheckedEventHandler(ObjComponentOnSqlChecked);
                        }
                    }

                    void ObjComponentOnSqlChecked(object sender, EventArgsSqlChecked e)
                    {
                        OnSqlChecked(this, new EventArgsSqlChecked
                        {
                            Query = e.Query,
                            ColumnName = e.ColumnName,
                            AliasName = e.AliasName,
                        });
                    }

                    public string id
                    {
                        get { return Id; }
                    }

                    public Screen Parent
                    {
                        get { return _parent; }
                    }

                    public string Id
                    {
                        get { return _id; }
                    }

                    internal void CheckSql(Collection<ColumnInfo> items)
                    {
                       foreach(Component component in Components)
                       {
                           component.CheckSql(items);
                       }
                    }
                }

                private readonly int _id;
                private readonly string _title;
                private readonly List<Screen.List> _Lists;
                private readonly List<Sql> _Sqls;
                private readonly List<Frame> _Frames;
                public Collection<string> Querys;

                public Screen(XmlReader xrScreen)
                {
                    _Sqls = new List<Sql>();
                    _Lists = new List<Screen.List>();
                    _Frames = new List<Frame>();
                    xrScreen.ReadToFollowing("id");
                    xrScreen.Read();
                    _id = int.Parse(xrScreen.Value);
                    xrScreen.ReadToFollowing("title");
                    xrScreen.Read();
                    _title = xrScreen.Value;
                    xrScreen.ReadToFollowing("Lists");
                    if (!xrScreen.IsEmptyElement)
                    {
                        XmlReader xrLists = xrScreen.ReadSubtree();
                        while (xrLists.ReadToFollowing("List"))
                        {
                            _Lists.Add(new Screen.List(xrLists,_id));
                        }
                    }
                    xrScreen.ReadToFollowing("Sqls");
                    if (!xrScreen.IsEmptyElement)
                    {
                        XmlReader xrSqls = xrScreen.ReadSubtree();
                        while (xrSqls.ReadToFollowing("Sql"))
                        {
                            _Sqls.Add(new Sql(xrSqls));
                        }
                    }
                    while (xrScreen.ReadToFollowing("Frame"))
                    {
                        XmlReader xrFrame = xrScreen.ReadSubtree();
                        Frame objFrame = new Frame(xrFrame, this);
                        _Frames.Add(objFrame);
                        objFrame.OnSqlChecked += new Frame.SqlCheckedEventHandler(ObjFrameOnSqlChecked);
                    }
                }

                public class List
                {
                    private string _id;
                    private string _sql;
                    private int _screenId;
                    public List(XmlReader xrLists, int screenId)
                    {
                        _screenId = screenId;
                        xrLists.ReadToFollowing("id");
                        xrLists.Read();
                        _id = xrLists.Value;
                        xrLists.ReadToFollowing("sql");
                        xrLists.Read();
                        _sql = xrLists.Value;
                    }

                    public string Id
                    {
                        get { return _id; }
                        set { throw new NotImplementedException(); }
                    }

                    public string ParseQuery(Hashtable objParams)
                    {
                        string strQueryText = _sql;
                        foreach (var key in objParams.Keys)
                        {
                            string value;
                            if (objParams[key] != null)
                            {
                                value = objParams[key].ToString();
                            }
                            else
                            {
                                try
                                {
                                    value = BaseTest.Params[key.ToString().ToLower()].ToString();
                                }
                                catch (Exception e)
                                {
                                    throw new Exception("Exception value in Hashtable key define the following key: objParams.Add(\"" + key.ToString().ToLower() + "\", null);", e);
                                }
                            }
                            strQueryText = strQueryText.Replace(string.Format("${0}$", key), value);
                        }
                        return strQueryText;
                    }
                }
                public Screen(XmlReader reader, Assembly scripting)
                {
                    _Sqls = new List<Sql>();
                    _Lists = new List<Screen.List>();
                    _Frames = new List<Frame>();
                    reader.ReadToFollowing("id");
                    reader.Read();
                    _id = int.Parse(reader.Value);
                    reader.ReadToFollowing("title");
                    reader.Read();
                    _title = reader.Value;
                    reader.ReadToFollowing("Lists");
                    if (!reader.IsEmptyElement)
                    {
                        XmlReader xrLists = reader.ReadSubtree();
                        while (xrLists.ReadToFollowing("List"))
                        {
                            _Lists.Add(new Screen.List(xrLists,_id));
                        }
                    }
                    reader.ReadToFollowing("Sqls");
                    if (!reader.IsEmptyElement)
                    {
                        XmlReader xrSqls = reader.ReadSubtree();
                        while (xrSqls.ReadToFollowing("Sql"))
                        {
                            _Sqls.Add(new Sql(xrSqls));
                        }
                    }
                    while (reader.ReadToFollowing("Frame"))
                    {
                        XmlReader xrFrame = reader.ReadSubtree();
                        Frame objFrame = new Frame(xrFrame, scripting, this);
                        _Frames.Add(objFrame);
                        objFrame.OnSqlChecked += new Frame.SqlCheckedEventHandler(ObjFrameOnSqlChecked);
                    }
                    string _scriptingId = String.Format("Fo{0}_Fr_T_C", _id);
                    ParseMethod(scripting, _scriptingId, "OnShow");
                    ParseMethod(scripting, _scriptingId, "OnValidate");
                    ParseMethod(scripting, _scriptingId, "OnExit");
                }

                private void ParseMethod(Assembly scripting, string form, string methodName)
                {
                    try
                    {
                        MethodInfo mi = scripting.GetModules()[0].GetType("Tensing.FieldVision.Scripting.GeneratedPlugin." + form).GetMethod(methodName);
                        using (MethodBodyReader objMethodBodyReader = new SDILReader.MethodBodyReader(mi))
                        {
                            objMethodBodyReader.OnSqlInScript += objMethodBodyReader_OnSqlInScript;
                            objMethodBodyReader.GetBodyCode();
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.Message);
                    }
                }

                private void objMethodBodyReader_OnSqlInScript(object sender, MethodBodyReader.EventArgsSqlInScript e)
                {
                    if (Querys == null) Querys = new Collection<string>();
                    if (((MethodInfo)e.Operand).Name == "Format")
                    {
                        int i = 2;
                        while (!((ILInstruction)((List<ILInstruction>)e.Instructions)[e.StackPointer - i]).Code.Name.StartsWith("ldstr"))
                        {
                            i++;
                        }
                        Querys.Add(((List<ILInstruction>)e.Instructions)[e.StackPointer - i].Operand.ToString());
                    }
                    Querys.Add(e.Operand.ToString());
                }

                void ObjFrameOnSqlChecked(object sender, EventArgsSqlChecked e)
                {
                    OnSqlChecked(this, new EventArgsSqlChecked
                    {
                        Query = e.Query,
                        ColumnName = e.ColumnName,
                        AliasName = e.AliasName,
                        Screen = _title
                    });
                }

                public void CheckTextBoxLengths(IEnumerable<ScreenLib.VarcharColumn> items)
                {
                    //Foreach distinct table check if it matches a query, 
                    //if so parse query and check length of textbox component in query
                    //Then, output length of textbox and lenght of field
                    var tables = (from a in items
                                  where a.TypeLength > 50
                                  select new {a.TableName}).Distinct();
                    foreach(var table in tables)
                    {
                        Regex searchTable = 
                            new Regex(table.TableName);
                        Regex queryVariables = new Regex(@"\$(sub|form)\.(?<name>[^\$]?[^\$]*)\$");

                        var matchingSqls = from sql in _Sqls
                                           let query = sql.q
                                           let matches = queryVariables.Matches(query)
                                           where searchTable.Matches(query).Count > 0
                                           select new
                                                      {
                                                          query,
                                                          variables = from Match match in matches
                                                                      from Capture name in match.Groups["name"].Captures
                                                                      select name.Value
                                                      };
                        if (matchingSqls.Count()>0)
                        {
                            foreach(var matchingSql in matchingSqls)
                            {
                                
                                if (matchingSql.variables.Count() > 0)
                                {
                                    foreach (var variable in matchingSql.variables)
                                    {
                                        //Check if variables is a textbox component in each frame
                                        foreach(Frame frame in _Frames)
                                        {
                                            var matchingTextboxes =
                                                from tb in frame.Components
                                                where tb.blnIsTextbox && tb.id == variable 
                                                select tb;
                                            if (matchingTextboxes.Count() > 0)
                                            {
                                                var tableFields = from fields in items
                                                                  where fields.TableName == table.TableName
                                                                  select fields;
                                                foreach(var field in tableFields)
                                                {
                                                    //Trigger event for suspected textboxes.
                                                    if (new Regex(field.ColumnName).IsMatch(matchingSql.query))
                                                    {
                                                        OnTextboxChecked(this, new EventArgsTextboxChecked
                                                                                   {
                                                                                       Query = matchingSql.query,
                                                                                       ColumnLength = field.TypeLength,
                                                                                       ColumnName = field.ColumnName,
                                                                                       Component = matchingTextboxes.First().id,
                                                                                       ComponentLength = matchingTextboxes.First().maxlength,
                                                                                       Frame = frame.id,
                                                                                       Screen = _title
                                                                                   });
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        
                    }
                        

                }

                public void CheckSql(Collection<ColumnInfo> items)
                {
                    foreach (var frame in _Frames)
                    {
                        frame.CheckSql(items);
                    }

                    var tables = (from a in items
                                  select new {a.TableName}).Distinct();
                    foreach (var table in tables)
                    {
                        Regex searchTable =
                            new Regex(table.TableName);
                        //Parse each SQL and get alliasses.
                        //For example in the query snippet 'SELECT max(KM) as maxKM, '' as KM'
                        //both KM as maxKM are aliasses.
                        Regex queryVariables = new Regex(@"as (?<name>[^,\n]?[^,\n]*)(\n|,)");

                        var matchingSqls = from sql in _Sqls
                                           let query = sql.q
                                           let matches = queryVariables.Matches(query)
                                           where searchTable.Matches(query).Count > 0
                                           select new
                                                      {
                                                          query,
                                                          variables = from Match match in matches
                                                                      from Capture name in match.Groups["name"].Captures
                                                                      select name.Value
                                                      };
                        if (matchingSqls.Count() > 0)
                        {
                            foreach (var matchingSql in matchingSqls)
                            {

                                if (matchingSql.variables.Count() > 0)
                                {
                                    foreach (var variable in matchingSql.variables)
                                    {
                                        //Check if allias is used as a column name in the table
                                        var tableFields = from fields in items
                                                          where (fields.TableName == table.TableName)
                                                          && (fields.ColumnName == variable)
                                                          select fields;
                                        foreach (var field in tableFields)
                                        {
                                            //Trigger event for suspected textboxes.
                                            if (new Regex(field.ColumnName).IsMatch(matchingSql.query))
                                            {
                                                OnSqlChecked(this, new EventArgsSqlChecked
                                                {
                                                    Query = matchingSql.query,
                                                    ColumnName = field.ColumnName,
                                                    AliasName = variable,
                                                    Screen = _title
                                                });
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                public bool CheckId(int screenId)
                {
                    return (screenId == _id);
                }

                public Component GetComponent(string s)
                {
                    foreach (var frame in _Frames)
                    {
                        foreach(var component in frame.Components)
                        {
                            if(component.id == s) return component;
                        }
                    }
                    return null;
                }

                public string GetQueryById(int queryId, Hashtable objParams)
                {
                    foreach(Sql _sql in _Sqls)
                    {
                        if (_sql.Id == queryId)
                        {
                            return _sql.ParseQuery(objParams);
                        }
                    }
                    return null;
                }

                public bool HasComponent(int componentId)
                {
                    foreach (var frame in _Frames)
                    {
                        foreach (var component in frame.Components)
                        {
                            if (component.ComponentId == componentId) return true;
                        }
                    }
                    return false;
                }

                public Component GetComponent(int componentId)
                {
                    foreach (var frame in _Frames)
                    {
                        foreach (var component in frame.Components)
                        {
                            if (component.ComponentId == componentId) return component;
                        }
                    }
                    return null;
                }

                public IEnumerable<Component> GetComponentsByOnClickOpenReference(int onClickOpenId)
                {
                    var ret = new List<Component>();
                    foreach (Frame frame in _Frames)
                    {
                        foreach (var component in frame.Components)
                        {
                            if(component is FVButton)
                            {
                                if(((FVButton)component).OnClickOpen == onClickOpenId)
                                {
                                    ret.Add(component);
                                }
                                else
                                {
                                    string strScript = ((FVButton)component).OnClick();
                                    if (strScript.Contains("ldc.i4.s"+onClickOpenId) && strScript.Contains("ShowInformationScreen"))
                                    {
                                        ret.Add(component);
                                    }
                                }
                                
                            }
                        }
                    }
                    return ret;
                }

                public void PrintAllQueries(TextWriter @out)
                {
                    if (Querys != null)
                    {
                        foreach (var q in Querys)
                        {
                            @out.WriteLine(q);
                        }
                    }
                    if (_Sqls != null)
                    {
                        foreach (var q in _Sqls)
                        {
                            @out.WriteLine(q.q);
                        }
                    }
                    foreach (var f in _Frames)
                    {
                        foreach (var c in f.Components)
                        {
                            if (c.Querys != null)
                            {
                                foreach (var q in c.Querys)
                                {
                                    @out.WriteLine(q);
                                }
                            }
                            if (c.querytext != null)
                            {
                                @out.WriteLine(c.querytext);
                            }

                        }
                    }

                }

                public string GetListById(string listId, Hashtable objParams)
                {
                    foreach(Screen.List _List in _Lists)
                    {
                        if (_List.Id == listId)
                        {
                            return _List.ParseQuery(objParams);
                        }
                    }
                    return null;
                }

                public void GenerateAllExecutionPlansScripts(TextWriter @out)
                {
                    if (Querys != null)
                    {
                        foreach (var q in Querys)
                        {
                            if (q == "") continue;
                            @out.Write(@"DECLARE @SQL nvarchar(max);
SET @SQL = '");
                            @out.Write(ReplaceParametersWithBaseDefault(q.Replace("'", "''")));
                            @out.Write(@"'
EXEC [dbo].[GetEstimatedExecutionPlanInfo] @SQL
GO
");
                        }
                    }
                    if (_Sqls != null)
                    {
                        foreach (var q in _Sqls)
                        {
                            if(q.q == "") continue;
                            @out.Write(@"DECLARE @SQL nvarchar(max);
SET @SQL = '");
                            @out.Write(ReplaceParametersWithBaseDefault(q.q.Replace("'", "''")));
                            @out.Write(@"'
EXEC [dbo].[GetEstimatedExecutionPlanInfo] @SQL
GO
");
                        }
                    }
                    foreach (var f in _Frames)
                    {
                        foreach (var c in f.Components)
                        {
                            if (c.Querys != null)
                            {
                                foreach (string s in c.Querys)
                                {
                                    if (s == "") continue;
                                    @out.Write(@"DECLARE @SQL nvarchar(max);
SET @SQL = '");
                                    @out.Write(ReplaceParametersWithBaseDefault(s.Replace("'", "''")));
                                    @out.Write(@"'
EXEC [dbo].[GetEstimatedExecutionPlanInfo] @SQL
GO
");
                                }
                            }
                            if (c.querytext != null)
                            {
                                if (c.querytext == "") continue;
                                @out.Write(@"DECLARE @SQL nvarchar(max);
SET @SQL = '");
                                @out.Write(ReplaceParametersWithBaseDefault(c.querytext.Replace("'","''")));
                                @out.Write(@"'
EXEC [dbo].[GetEstimatedExecutionPlanInfo] @SQL
GO
");
                            }

                        }
                    }
                }

                private static string ReplaceParametersWithBaseDefault(string s)
                {
                    string strQueryText = s;
                    foreach (var key in BaseTest.Params.Keys)
                    {
                        string value;
                        try
                        {
                               value = BaseTest.Params[key.ToString().ToLower()].ToString();
                        }
                        catch (Exception e)
                        {
                            throw new Exception("Exception value in Hashtable key define the following key: objParams.Add(\"" + key.ToString().ToLower() + "\", null);", e);
                        }
                        strQueryText = strQueryText.Replace(string.Format("${0}$", key), value);
                    }
                    return strQueryText;
                }
            }
            private readonly List<Screen> _screens;
/*
            public Screenfiguration(XmlReader xrScreenConfiguration)
            {
                _screens = new List<Screen>();
                // Check all screen id's from screen
                xrScreenConfiguration.ReadToFollowing("Screens");
                if (!xrScreenConfiguration.IsEmptyElement)
                {
                    while (xrScreenConfiguration.ReadToFollowing("Screen"))
                    {
                        XmlReader xrScreen = xrScreenConfiguration.ReadSubtree();
                        var objScreen = new Screen(xrScreen);
                        _screens.Add(objScreen);
                        objScreen.OnTextboxChecked += new Screen.TextboxCheckedEventHandler(objScreen_OnTextboxChecked);
                        objScreen.OnSqlChecked += new Screen.SqlCheckedEventHandler(objScreen_OnSqlChecked);
                    }
                }
            }
*/
            internal class Flow
            {
                private int _id;
                private int _start;

                internal class Step
                {
                    private readonly int _id;
                    private readonly int _form_id;

                    public class Transition
                    {
                        public class Condition: ISql
                        {
                            public readonly string _query;
                            private string _val2chk;
                            private string _operator;

                            public Condition(XmlReader reader, int i)
                            {
                                Nr = i;
                                reader.ReadToFollowing("query");
                                reader.Read();
                                _query = reader.Value;
                                reader.ReadToFollowing("val2chk");
                                reader.Read();
                                _val2chk = reader.Value;
                                reader.ReadToFollowing("operator");
                                reader.Read();
                                _operator = reader.Value;
                            }

                            public string Operator
                            {
                                get { return _operator; }
                            }

                            public string Val2Chk
                            {
                                get { return _val2chk; }
                            }

                            public int Nr { get; set; }

                            public string ParseQuery(Hashtable objParams)
                            {
                                string strQueryText = _query;
                                foreach (var key in objParams.Keys)
                                {
                                    strQueryText = strQueryText.Replace(string.Format("${0}$", key), objParams[key].ToString());
                                }
                                return strQueryText;
                            }
                        }

                        public List<Condition> _Conditions;
                        private readonly int _next;
                        public Transition(XmlReader reader)
                        {
                            _Conditions = new List<Condition>();
                            reader.ReadToFollowing("next");
                            reader.Read();
                            _next = int.Parse(reader.Value);
                            reader.ReadToFollowing("Conditions");
                            if (!reader.IsEmptyElement)
                            {
                                int i = 0;
                                XmlReader xrConditions = reader.ReadSubtree();
                                while (xrConditions.ReadToFollowing("Condition"))
                                {
                                    _Conditions.Add(new Condition(xrConditions,i++));    
                                }
                            }
                        }

                        public int NextStepId
                        {
                            get { return _next; }
                        }
                    }

                    public readonly List<Transition> _Transitions;

                    public Step(XmlReader reader)
                    {
                        _Transitions = new List<Transition>();
                        reader.ReadToFollowing("id");
                        reader.Read();
                        _id = int.Parse(reader.Value);
                        reader.ReadToFollowing("form_id");
                        reader.Read();
                        _form_id = int.Parse(reader.Value);
                        reader.ReadToFollowing("Transitions");
                        if (!reader.IsEmptyElement)
                        {
                            XmlReader xrTransitions = reader.ReadSubtree();
                            while (xrTransitions.ReadToFollowing("Transition"))
                            {
                                _Transitions.Add(new Transition(xrTransitions));
                            }
                        }
                    }

                    public int Id
                    {
                        get { return _id; }
                    }
                }

                private readonly List<Step> _Steps;

                public Flow(XmlReader reader)
                {
                    _Steps = new List<Step>();
                    reader.ReadToFollowing("id");
                    reader.Read();
                    _id = int.Parse(reader.Value);
                    reader.ReadToFollowing("start");
                    reader.Read();
                    _start = int.Parse(reader.Value);
                    reader.ReadToFollowing("Steps");
                    if (!reader.IsEmptyElement)
                    {
                        XmlReader xrSteps = reader.ReadSubtree();
                        while (xrSteps.ReadToFollowing("Step"))
                        {
                            _Steps.Add(new Step(xrSteps));
                        }
                    }
                }

                public bool CheckId(int flowId)
                {
                    return flowId == _id;
                }

                public Step.Transition.Condition GetCondition(int stepId, int nextStepId, int conditionNr)
                {
                    foreach (var step in _Steps)
                    {
                        if (step.Id == stepId)
                        {
                            foreach (var transition in step._Transitions)
                            {
                                if (transition.NextStepId == nextStepId)
                                {
                                    foreach (var condtion in transition._Conditions)
                                    {
                                        if (condtion.Nr == conditionNr)
                                        {
                                            return condtion;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    return null;
                }
            }

            private readonly List<Flow> _flows;
            public ScreenConfiguration(XmlReader configuration, Assembly scripting)
            {
                _screens = new List<Screen>();
                _flows = new List<Flow>();
                // Check all screen id's from screen
                configuration.ReadToFollowing("Flows");
                XmlReader xrFlow = configuration.ReadSubtree();
                if (!xrFlow.IsEmptyElement)
                {
                    while (xrFlow.ReadToFollowing("Flow"))
                    {
                        var objFlow = new Flow(xrFlow);
                        _flows.Add(objFlow);
                    }
                }
                if (!configuration.IsEmptyElement)
                {
                    while (configuration.ReadToFollowing("Screen"))
                    {
                        XmlReader xrScreen = configuration.ReadSubtree();
                        var objScreen = new Screen(xrScreen, scripting);
                        _screens.Add(objScreen);
                        objScreen.OnTextboxChecked += new Screen.TextboxCheckedEventHandler(objScreen_OnTextboxChecked);
                        objScreen.OnSqlChecked += new Screen.SqlCheckedEventHandler(objScreen_OnSqlChecked);
                    }
                }
            }

            void objScreen_OnSqlChecked(object sender, ScreenConfiguration.EventArgsSqlChecked e)
            {
                OnSqlChecked(sender, e);
            }

            void objScreen_OnTextboxChecked(object sender, EventArgsTextboxChecked e)
            {
                OnTextboxChecked(sender, e);
            }

            public void CheckTextBoxLengths(IEnumerable<ScreenLib.VarcharColumn> items)
            {

                //CheckTextBoxLengths for each screen
                foreach(Screen objScreen in _screens)
                {
                    objScreen.CheckTextBoxLengths(items);
                }
            }

            public void CheckSql(Collection<ColumnInfo> items)
            {
                //CheckSql for each screen
                foreach (Screen objScreen in _screens)
                {
                    objScreen.CheckSql(items);
                }
            }

            public string GetQueryFromComponent(int screenid,string s, Hashtable objParams)
            {
                Screen objScreen = this.GetScreen(screenid);
                if(objScreen!=null)
                {
                    var objComponent = objScreen.GetComponent(s);
                    if(objComponent != null)
                    {
                        string strQueryText = objComponent.querytext;
                        foreach (var key in objParams.Keys)
                        {   
                            string value;
                            if (objParams[key] != null)
                            {
                                value = objParams[key].ToString();
                            }
                            else
                            {
                                try
                                {
                                    value = BaseTest.Params[key.ToString().ToLower()].ToString();
                                }
                                catch (Exception e)
                                {
                                    throw new Exception("Exception value in Hashtable key define the following key: objParams.Add(\"" + key.ToString().ToLower() + "\", null);", e);
                                }
                            }
                            strQueryText = strQueryText.Replace(string.Format("${0}$", key), value);
                        }
                        return strQueryText;
                    }
                }
                return null;
            }

            private Screen GetScreen(int screenid)
            {
                foreach (Screen objScreen in _screens)
                {
                    if(objScreen.CheckId(screenid))
                    {
                        return objScreen;
                    }
                }
                return null;
            }



            public string GetQueryById(int queryId, Hashtable objParams)
            {
                foreach (Screen objScreen in _screens)
                {
                    if(objScreen.GetQueryById(queryId, objParams)!= null)
                    {
                        return objScreen.GetQueryById(queryId, objParams);
                    }
                }
                return null;
            }

            public string ParseOnClick(int componentId)
            {
                foreach(Screen objScreen in _screens)
                {
                    if (objScreen.HasComponent(componentId))
                    {
                        Screen.Component objComponent = objScreen.GetComponent(componentId);
                        if(objComponent is Screen.FVButton)
                        {
                            return ((Screen.FVButton) objComponent).OnClick();
                        }
                    }
                }
                return null;
            }


            public int? OnClickOpen(int componentId)
            {
                foreach (Screen objScreen in _screens)
                {
                    if (objScreen.HasComponent(componentId))
                    {
                        Screen.Component objComponent = objScreen.GetComponent(componentId);
                        if (objComponent is Screen.FVButton)
                        {
                            return ((Screen.FVButton)objComponent).OnClickOpen;
                        }
                    }
                }
                return null;
            }

            public IEnumerable<Screen.Component> GetComponentsByOnClickOpenReference(int onClickOpenId)
            {
                var ret = new List<Screen.Component>();
                foreach (Screen objScreen in _screens)
                {
                    ret.AddRange(objScreen.GetComponentsByOnClickOpenReference(onClickOpenId));                 
                }
                return ret;
            }

            public int? OnClickOpen(int formId, string componentId)
            {
                var objScreen = GetScreen(formId);
                if (objScreen == null) return null;
                var objComponent = objScreen.GetComponent(componentId);
                if (objComponent == null) return null;
                return ((Screen.FVButton) objComponent).OnClickOpen;
            }

            public string GetQueryFromCondition(/* Flow id */ int flowId, /* Step id */ int stepId, /* Next step id */ int nextStepId, /* Condition Nr. */ int conditionNr, Hashtable objParams)
            {
                Flow objFlow = this.GetFlow(flowId);
                if (objFlow != null)
                {
                    var objCondition = objFlow.GetCondition(stepId, nextStepId, conditionNr);
                    if (objCondition != null)
                    {
                        string strQueryText = objCondition._query;
                        foreach (var key in objParams.Keys)
                        {
                            string value;
                            if (objParams[key] != null)
                            {
                                value = objParams[key].ToString();
                            }
                            else
                            {
                                try
                                {
                                    value = BaseTest.Params[key.ToString().ToLower()].ToString();
                                }
                                catch (Exception e)
                                {
                                    throw new Exception("Exception value in Hashtable key define the following key: objParams.Add(\"" + key.ToString().ToLower() + "\", null);", e);
                                }
                            }
                            strQueryText = strQueryText.Replace(string.Format("${0}$", key), value);
                        }
                        return strQueryText;
                    }
                }
                return null;
            }

            private Flow GetFlow(int flowId)
            {
                foreach (Flow objFlow in _flows)
                {
                    if (objFlow.CheckId(flowId))
                    {
                        return objFlow;
                    }
                }
                return null;
            }

            public string GetQueryInScriptFromComponent(int screenid, string s, Hashtable objParams, int queryIndex)
            {
                Screen objScreen = this.GetScreen(screenid);
                if (objScreen != null)
                {
                    var objComponent = objScreen.GetComponent(s);
                    if (objComponent != null)
                    {
                        string strQueryText = objComponent.Querys[queryIndex-1];
                        foreach (var key in objParams.Keys)
                        {
                            string value;
                            if (objParams[key] != null)
                            {
                                value = objParams[key].ToString();
                            }
                            else
                            {
                                try
                                {
                                    value = BaseTest.Params[key.ToString().ToLower()].ToString();
                                }
                                catch (Exception e)
                                {
                                    throw new Exception("Exception value in Hashtable key define the following key: objParams.Add(\"" + key.ToString().ToLower() + "\", null);", e);
                                }
                            }
                            strQueryText = strQueryText.Replace(string.Format("${0}$", key), value);
                        }
                        return strQueryText;
                    }
                }
                return null;
            }

            public string GetQueryInScriptFromForm(int screenid, string scriptingId, Hashtable objParams, int queryIndex)
            {
                Screen objScreen = this.GetScreen(screenid);
                if (objScreen != null)
                {
                    if (objScreen.Querys != null)
                    {
                        string strQueryText = objScreen.Querys[queryIndex - 1];
                        foreach (var key in objParams.Keys)
                        {
                            string value;
                            if (objParams[key] != null)
                            {
                                value = objParams[key].ToString();
                            }
                            else
                            {
                                try
                                {
                                    value = BaseTest.Params[key.ToString().ToLower()].ToString();
                                }
                                catch (Exception e)
                                {
                                    throw new Exception("Exception value in Hashtable key define the following key: objParams.Add(\"" + key.ToString().ToLower() + "\", null);", e);
                                }
                            }
                            strQueryText = strQueryText.Replace(string.Format("${0}$", key), value);
                        }
                        return strQueryText;
                    }
                }
                return null;
            }

            internal string GetListByScreenNrAndId(string listId, int screenid, Hashtable objParams)
            {
                Screen objScreen = this.GetScreen(screenid);
                if (objScreen != null)
                {
                    if(objScreen.GetListById(listId, objParams)!= null)
                    {
                        return objScreen.GetListById(listId, objParams);
                    }
                }
                return null;
            }

            public void PrintAllQueries(TextWriter @out)
            {
                foreach (var screen in _screens)
                {
                    screen.PrintAllQueries(@out);
                }
            }
            public void GenerateAllExecutionPlansScripts(TextWriter @out)
            {
                foreach (var screen in _screens)
                {
                    screen.GenerateAllExecutionPlansScripts(@out);
                }
            }
        }

        public enum enumTypeName
        {
            TNbigint,
            TNbinary,
            TNbit,
            TNchar,
            TNdatetime,
            TNdecimal,
            TNfloat,
            TNimage,
            TNint,
            TNmoney,
            TNnchar,
            TNntext,
            TNnumeric,
            TNnvarchar,
            TNreal,
            TNsmalldatetime,
            TNsmallint,
            TNsmallmoney,
            TNsql_variant,
            TNsysname,
            TNtext,
            TNtimestamp,
            TNtinyint,
            TNuniqueidentifier,
            TNvarbinary,
            TNvarchar
        }

        public class ColumnInfo
        {
            public string ColumnName;
            public string TableName;
            public ScreenLib.enumTypeName TypeName;
            public int TypeLength;
        }

        public class VarcharColumn
        {
            public string ColumnName;
            public string TableName;
            public int TypeLength;
            public VarcharColumn(string strColumnName, string strTableName, int intTypeLength)
            {
                ColumnName = strColumnName;
                TableName = strTableName;
                TypeLength = intTypeLength;
            }
        }
    }
}

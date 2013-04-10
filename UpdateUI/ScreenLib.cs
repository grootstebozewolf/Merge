using System;
/*using System.ComponentModel;*/
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
/*using System.Reflection;*/
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
//using System.Windows.Forms;
using System.Xml;
using SDILReader;

namespace UpdateUI
{

    public class ScreenLib
    {
        public static void GenerateParamList(StreamWriter objWriter, string s)
        {
            string[] strarr = s.Split('$');
            Collection<string> previousStrings = new Collection<string>();
            foreach(string str in strarr)
            {
                if (previousStrings.Contains(str)) continue;
                previousStrings.Add(str);
                if (str.StartsWith("flow.") || str.StartsWith("app.") || str.StartsWith("App.") || str.StartsWith("sub.") || str.StartsWith("form.") || str.StartsWith("args."))
                {
                    objWriter.Write(@"
            objParams.Add(""{0}"", null);",str);
                }
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

            public string GetQueryFromComponent(int screenid, string s, Hashtable objParams)
            {
                Screen objScreen = this.GetScreen(screenid);
                if (objScreen != null)
                {
                    var objComponent = objScreen.GetComponent(s);
                    if (objComponent != null)
                    {
                        string strQueryText = objComponent.querytext;
                        foreach (var key in objParams.Keys)
                        {
                            strQueryText = strQueryText.Replace(string.Format("${0}$", key), objParams[key].ToString());
                        }
                        return strQueryText;
                    }
                }
                return null;
            }

            public string GetQueryFromCondition(int flowId, int stepId, int nextStepId, int conditionNr, Hashtable objParams)
            {
                Flow objFlow = this.GetFlow(flowId);
                if(objFlow != null)
                {
                    var objCondition = objFlow.GetCondition(stepId, nextStepId, conditionNr);
                    if(objCondition != null)
                    {
                        string strQueryText = objCondition.querystring;
                        foreach (var key in objParams.Keys)
                        {
                            strQueryText = strQueryText.Replace(string.Format("${0}$", key), objParams[key].ToString());
                        }
                        return strQueryText;
                    }
                }
                return null;
            }

            private Flow GetFlow(int flowId)
            {
                foreach (Flow objFlow in flows)
                {
                    if (objFlow.CheckId(flowId))
                    {
                        return objFlow;
                    }
                }
                return null;
            }

            private Screen GetScreen(int screenid)
            {
                foreach (Screen objScreen in screens)
                {
                    if (objScreen.CheckId(screenid))
                    {
                        return objScreen;
                    }
                }
                return null;
            }

            class Screen
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
                private class List
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
                    public void GenerateTestSnippet(StreamWriter objWriter)
                    {
                        objWriter.Write(
                        @"        [TestMethod]
        public void TestListId{0}()
        {{
            var sqlCeConnection = Factory.CreateConnection();

            var objParams = new Hashtable();
", this._id);
                        GenerateParamList(objWriter, this._sql);
                        objWriter.Write(
                        @"
            //objParams.Add(""app.localeid"", 1043);
            //objParams.Add(""flow.activity_id"", Guid.Empty);

            String strSql = _configuration.GetListByScreenNrAndId(""{0}"",{1}, objParams);
            var command = Factory.CreateCommand(strSql, sqlCeConnection);

            try
            {{
                //TODO: Expected result for query
                /*{2}*/
                sqlCeConnection.Open();
                IDataReader dr = command.ExecuteReader();
                Assert.AreEqual(false, dr.Read());
            }}
            catch (Exception objException)
            {{
                throw Factory.CreateException(String.Format(""Fout tijdens uitlezen gegegevens.\r\nList:\r\n{{0}}"", strSql), objException);
            }}
            finally
            {{
                if (sqlCeConnection != null) sqlCeConnection.Close();
            }}
        }}
", this._id,this._screenId,this._sql);
                    }
                }
                class Sql
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

                    public void GenerateTestSnippet(StreamWriter objWriter)
                    {
                        objWriter.Write(
                        @"        [TestMethod]
        public void TestQueryNr{0}()
        {{
            var sqlCeConnection = Factory.CreateConnection();
            var objParams = new Hashtable();
", this._id);
                        GenerateParamList(objWriter, this._q);
                        objWriter.Write(
                        @"
            //objParams.Add(""app.localeid"", 1043);
            //objParams.Add(""flow.activity_id"", Guid.Empty);

            String strSql = _configuration.GetQueryById({0}, objParams);
            var command = Factory.CreateCommand(strSql, sqlCeConnection);

            try
            {{
                //TODO: Expected result for query
                /*{1}*/
                sqlCeConnection.Open();
                IDataReader dr = command.ExecuteReader();
                Assert.AreEqual(false, dr.Read());
            }}
            catch (Exception objException)
            {{
                throw Factory.CreateException(String.Format(""Fout tijdens uitlezen gegegevens.\r\nQuery:\r\n{{0}}"", strSql), objException);
            }}
            finally
            {{
                if (sqlCeConnection != null) sqlCeConnection.Close();
            }}
        }}
", this._id, this._q);
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

                    public FVButton(string sId, Frame parent, XmlReader component)
                    {
                        id = sId;
                        Parent = parent;
                    }

                    public FVButton(string sId, Frame parent, XmlReader component, Assembly scripting)
                    {
                        id = sId;
                        Parent = parent;

                        _onClick = ParseMethod(component, scripting, "OnClick");
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

                internal abstract class Component
                {
                    private Frame _parent;
                    protected string _scriptingId;
                    public bool blnIsTextbox;
                    public bool blnIsGridOrCombobox;
                    public int maxlength;
                    public string querytext;
                    private Collection<string> sqlsInScript;

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
                        sqlsInScript = new Collection<string>();
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
                        MethodInfo mi = scripting.GetModules()[0].GetType("Tensing.FieldVision.Scripting.GeneratedPlugin." + _scriptingId).GetMethod(methodName);
                        using (MethodBodyReader objMethodBodyReader = new SDILReader.MethodBodyReader(mi))
                        {
                            objMethodBodyReader.OnSqlInScript += objMethodBodyReader_OnSqlInScript;
                            return objMethodBodyReader.GetBodyCode();
                        }
                    }

                    private void objMethodBodyReader_OnSqlInScript(object sender, MethodBodyReader.EventArgsSqlInScript e)
                    {
                        if (e.Operand.GetType() == typeof(String))
                        {
                            sqlsInScript.Add(e.Operand.ToString());
                        }
                        else
                        {
                            if (((MethodInfo)e.Operand).Name == "Format")
                            {
                                int i = 2;
                                while (
                                    !((ILInstruction)((List<ILInstruction>)e.Instructions)[e.StackPointer - i]).Code.Name.
                                        StartsWith("ldstr"))
                                {
                                    i++;
                                }
                                sqlsInScript.Add(String.Format(((List<ILInstruction>)e.Instructions)[e.StackPointer - i].Operand.ToString(), "$args.value1$", "$args.value2$", "$args.value3$", "$args.value4$"));
                            }
                        }
                    }

                    public void GenerateTestSnippet(StreamWriter objWriter)
                    {
                        if((this is FVGrid)||(this is FVCombobox))
                        {
                            objWriter.Write(String.Format(@"        [TestMethod]
        // Component name: {0}
        public void TestComponent{1}()", id,id.Replace(".","_")));
                            objWriter.Write(
                                @"
        {
            var sqlCeConnection = Factory.CreateConnection();
            
            //MSBuild always nest testresults three levels deep. (TestResults\UniqueFolder\Out)
            var objParams = new Hashtable();");
                        GenerateParamList(objWriter, querytext);
                        objWriter.Write(
                        @"
            //objParams.Add(""app.userid"", 7);");
                            objWriter.WriteLine(string.Format(@"
            
            string strSql = _configuration.GetQueryFromComponent({0}, ""{1}"", objParams);", Parent.Parent._id, id));
                            objWriter.WriteLine(@"

            var command = Factory.CreateCommand(strSql, sqlCeConnection);

            try
            {
                //TODO: Expected result
                sqlCeConnection.Open();
                IDataReader dr = command.ExecuteReader();
                Assert.AreEqual(false, dr.Read());
            }
            catch (Exception objException)
            {
                throw Factory.CreateException(String.Format(""Fout tijdens uitlezen gegegevens.\r\nQuery:\r\n{0}"", strSql), objException);
            }
            finally
            {
                if (sqlCeConnection != null) sqlCeConnection.Close();
            }
        }");
                        }
                        int i = 0;
                        if(sqlsInScript!=null)
                        {
                            foreach (var sql in sqlsInScript)
                            {
                                objWriter.Write(String.Format(@"        [TestMethod]
        // Component name: {0}
        public void TestComponent{1}_SqlInScripting{2}()", id, _scriptingId ?? id,++i));
                                objWriter.Write(
                                    @"
        {
            var sqlCeConnection = Factory.CreateConnection();
            //MSBuild always nest testresults three levels deep. (TestResults\UniqueFolder\Out)
            var objParams = new Hashtable();");
                                GenerateParamList(objWriter, sql);
                                objWriter.Write(
                                @"
            //objParams.Add(""app.userid"", 7);");
                                objWriter.WriteLine(string.Format(@"
            
            string strSql = _configuration.GetQueryInScriptFromComponent({0}, ""{1}"", objParams, {2});", Parent.Parent._id, id, i));
                                objWriter.WriteLine(string.Format(@"

            var command = Factory.CreateCommand(strSql, sqlCeConnection);

            try
            {{
                //TODO: Expected result for query
                /*{0}*/
                sqlCeConnection.Open();
                command.ExecuteNonQuery();
            }}
            catch (Exception objException)
            {{
                throw Factory.CreateException(String.Format(""Fout tijdens uitlezen gegegevens.\r\nQuery:\r\n{{0}}"", strSql), objException);
            }}
            finally
            {{
                if (sqlCeConnection != null) sqlCeConnection.Close();
            }}
        }}", sql));
                            }
                        }
                    }
                }

                internal class Frame
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
                        get { return _id; }
                    }

                    public Screen Parent
                    {
                        get { return _parent; }
                    }

                    internal void CheckSql(Collection<ColumnInfo> items)
                    {
                       foreach(Component component in Components)
                       {
                           component.CheckSql(items);
                       }
                    }

                    public void GenerateTestSnippet(StreamWriter objWriter)
                    {
                        foreach(Component component in this.Components)
                        {
                            component.GenerateTestSnippet(objWriter);
                        }
                    }
                }

                private int _id;
                private string _title;
                private readonly List<Sql> _Sqls;
                private readonly List<Frame> _Frames;

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
                    if(!xrScreen.IsEmptyElement)
                    {
                        XmlReader xrLists = xrScreen.ReadSubtree();
                        while(xrLists.ReadToFollowing("List"))
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
                            _Lists.Add(new Screen.List(xrLists, _id));
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
                        Console.Write(e.Message);
                    }
                }

                private void objMethodBodyReader_OnSqlInScript(object sender, MethodBodyReader.EventArgsSqlInScript e)
                {
                    if (sqlsInScripts == null) sqlsInScripts = new Collection<string>();
                    if (e.Operand.GetType() == typeof(String))
                    {
                        sqlsInScripts.Add(e.Operand.ToString());    
                    }
                    else
                    {
                        if(((MethodInfo)e.Operand).Name == "Format")
                        {
                            int i = 2;
                            while (
                                !((ILInstruction)((List<ILInstruction>)e.Instructions)[e.StackPointer - i]).Code.Name.
                                    StartsWith("ldstr"))
                            {
                                i++;
                            }
                            sqlsInScripts.Add(String.Format(
                                ((List<ILInstruction>)e.Instructions)[e.StackPointer - i].Operand.ToString(), "$args.value1$", "$args.value2$", "$args.value3$", "$args.value4$"));
                        }
                    }
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
                        Regex queryVariables = new Regex(@"\$(sub|form|args)\.(?<name>[^\$]?[^\$]*)\$");

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

                private static Collection<string> FileNames = new Collection<string>();
                private Collection<string> sqlsInScripts;
                private List<Screen.List> _Lists;

                public void GenerateMSTest(string path)
                {

                    //■< (less than)
                    //■> (greater than)
                    //■: (colon)
                    //■" (double quote)
                    //■/ (forward slash)
                    //■\ (backslash)
                    //■| (vertical bar or pipe)
                    //■? (question mark)
                    //■* (asterisk)
                    int i = 0;
                    string filename = path + this._title.Replace(" ", "").Replace("<", "").Replace(":", "").Replace("/", "").Replace("\\", "").Replace("|", "").Replace("?", "").Replace("*", "") + ".cs";
                    while(FileNames.Contains(filename.ToLower()))
                    {
                        i++;
                        filename = path + this._title.Replace(" ", "").Replace("<", "").Replace(":", "").Replace("/", "").Replace("\\", "").Replace("|", "").Replace("?", "").Replace("*", "") + i.ToString() + ".cs";
                    }
                    FileNames.Add(filename.ToLower());
                    StreamWriter objWriter = new StreamWriter(filename);
                    objWriter.WriteLine(@"using System;
using System.Collections;
using System.Data;
using System.Data.SqlServerCe;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SDILReader;

namespace MSTest
{"); objWriter.WriteLine(@"
    //Form name: {0}
    [TestClass]
    public class Form{1} : BaseTest",this._title,this._id); objWriter.WriteLine(@"
    {
        [TestInitialize()]
        public override void SetupEnviroment()
        {
            Globals.LoadOpCodes();
            _configuration = new ScreenLib.ScreenConfiguration(_xrScreen, _assemblyScripting);
            var sqlCeConnection = Factory.CreateConnection();
            String strSql;
            if (MSTest.Properties.Settings.Default.RecreateDB)
            {
                try
                {
                    if (File.Exists(_cMyTestFieldserviceSdf))
                    {
                        File.Delete(_cMyTestFieldserviceSdf);
                    }
                    SqlCeEngine engine = Factory.CreateSqlEngine();
                    engine.CreateDatabase();
                    engine.Dispose();
                    using (StreamReader objReader = new StreamReader(_cMyTestImportTxt))
                    {
                        while (!objReader.EndOfStream)
                        {
                            strSql = objReader.ReadLine();
                            var command = Factory.CreateCommand(
                                strSql,
                                sqlCeConnection
                                );
                            try
                            {
                                sqlCeConnection.Open();
                                int numResults = command.ExecuteNonQuery();
                            }
                            catch (Exception objException)
                            {
                                throw Factory.CreateException(String.Format(""Fout tijdens uitlezen gegegevens.\r\nQuery:\r\n{0}"", strSql), objException);
                            }
                            finally
                            {
                                if (sqlCeConnection != null) sqlCeConnection.Close();
                            }
                        }
                    }
                }
                catch (System.IO.IOException)
                {

                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
");
                    foreach(Frame frame in this._Frames)
                    {
                        frame.GenerateTestSnippet(objWriter);
                    }
                    foreach(Screen.List list in this._Lists)
                    {
                        list.GenerateTestSnippet(objWriter);
                    }
                    foreach(Sql sql in this._Sqls)
                    {
                        sql.GenerateTestSnippet(objWriter);
                    }
                    int q = 0;
                    if (sqlsInScripts != null)
                    {
                        foreach (var sql in sqlsInScripts)
                        {
                            objWriter.Write(String.Format(@"        
        // Scripting name: {1}
        [TestMethod]
        public void TestForm{1}_SqlInScripting{2}()", _id,  String.Format("Fo{0}_Fr_T_C", _id), ++q));
                            objWriter.Write(
                                @"
        {
            var sqlCeConnection = Factory.CreateConnection();
            //MSBuild always nest testresults three levels deep. (TestResults\UniqueFolder\Out)
            var objParams = new Hashtable();");
                            GenerateParamList(objWriter, sql);
                            objWriter.Write(
                            @"
            //objParams.Add(""app.userid"", 7);");
                            objWriter.WriteLine(string.Format(@"
            
            string strSql = _configuration.GetQueryInScriptFromForm({0}, ""{1}"", objParams, {2});", _id, String.Format("Fo{0}_Fr_T_C", _id), q));
                            objWriter.WriteLine(string.Format(@"

            var command = Factory.CreateCommand(strSql, sqlCeConnection);

            try
            {{
                //TODO: Expected result for query
                /*{0}*/
                sqlCeConnection.Open();
                command.ExecuteNonQuery();
            }}
            catch (Exception objException)
            {{
                throw new Exception(String.Format(""Fout tijdens uitlezen gegegevens.\r\nQuery:\r\n{{0}}"", strSql), objException);
            }}
            finally
            {{
                if (sqlCeConnection != null) sqlCeConnection.Close();
            }}
        }}", sql));
                        }
                    }

                    objWriter.WriteLine(@"
    }
}");
                    objWriter.Flush();
                }

                public bool CheckId(int screenId)
                {
                    return (screenId == _id);
                }

                public Component GetComponent(string s)
                {
                    foreach (var frame in _Frames)
                    {
                        foreach (var component in frame.Components)
                        {
                            if (component.id == s) return component;
                        }
                    }
                    return null;
                }
            }
            private readonly List<Screen> screens;
            private readonly List<Flow> flows;
            public ScreenConfiguration(XmlReader xrScreenConfiguration)
            {
                screens = new List<Screen>();
                // Check all screen id's from screen
                xrScreenConfiguration.ReadToFollowing("Screens");
                if (!xrScreenConfiguration.IsEmptyElement)
                {
                    while (xrScreenConfiguration.ReadToFollowing("Screen"))
                    {
                        XmlReader xrScreen = xrScreenConfiguration.ReadSubtree();
                        var objScreen = new Screen(xrScreen);
                        screens.Add(objScreen);
                        objScreen.OnTextboxChecked += new Screen.TextboxCheckedEventHandler(objScreen_OnTextboxChecked);
                        objScreen.OnSqlChecked += new Screen.SqlCheckedEventHandler(objScreen_OnSqlChecked);
                    }
                }
            }

            public ScreenConfiguration(XmlReader configuration, Assembly scripting)
            {
                flows = new List<Flow>();
                configuration.ReadToFollowing("Flows");
                XmlReader xrFlows = configuration.ReadSubtree();
                if (!xrFlows.IsEmptyElement)
                {
                    while (xrFlows.ReadToFollowing("Flow"))
                    {
                        XmlReader xrFlow = xrFlows.ReadSubtree();
                        var objFlow = new Flow(xrFlow);
                        flows.Add(objFlow);
                    }
                }
                screens = new List<Screen>();
                // Check all screen id's from screen
                if (!configuration.IsEmptyElement)
                {
                    while (configuration.ReadToFollowing("Screen"))
                    {
                        XmlReader xrScreen = configuration.ReadSubtree();
                        var objScreen = new Screen(xrScreen, scripting);
                        screens.Add(objScreen);
                        objScreen.OnTextboxChecked += new Screen.TextboxCheckedEventHandler(objScreen_OnTextboxChecked);
                        objScreen.OnSqlChecked += new Screen.SqlCheckedEventHandler(objScreen_OnSqlChecked);
                    }
                }
            }

            public class Flow
            {
                public readonly List<Step> Steps;
                private int id;
                private int start;

                public Flow(XmlReader xrFlow)
                {
                    Steps = new List<Step>();
                    xrFlow.ReadToFollowing("id");
                    xrFlow.Read();
                    id = int.Parse(xrFlow.Value);
                    xrFlow.ReadToFollowing("start");
                    xrFlow.Read();
                    start = int.Parse(xrFlow.Value);
                    xrFlow.ReadToFollowing("Steps");
                    if (!xrFlow.IsEmptyElement)
                    {
                        XmlReader xrStep = xrFlow.ReadSubtree();
                        while (xrStep.ReadToFollowing("Step"))
                        {
                            Steps.Add(new Step(xrStep,this));
                        }
                    }
                }

                public class Step
                {
                    private int id;
                    private int formId;
                    public readonly List<Transition> Transitions;
                    public Flow Parent;
                    public Step(XmlReader xrStep, Flow parent)
                    {
                        Parent = parent;
                        Transitions = new List<Transition>();
                        xrStep.ReadToFollowing("id");
                        xrStep.Read();
                        id = int.Parse(xrStep.Value);
                        xrStep.ReadToFollowing("form_id");
                        xrStep.Read();
                        formId = int.Parse(xrStep.Value);
                        xrStep.ReadToFollowing("Transitions");
                        if (!xrStep.IsEmptyElement)
                        {
                            XmlReader xrTransition = xrStep.ReadSubtree();
                            while (xrTransition.ReadToFollowing("Transition"))
                            {
                                Transitions.Add(new Transition(xrTransition,this));
                            }
                        }
                    }

                    public class Transition
                    {
                        public readonly List<Condition> Conditions;
                        private int next;

                        public Step Parent;

                       public Transition(XmlReader xrTransition, Step parent)
                       {
                           Parent = parent;
                            Conditions = new List<Condition>();
                            xrTransition.ReadToFollowing("next");
                            xrTransition.Read();
                            next = int.Parse(xrTransition.Value);
                            xrTransition.ReadToFollowing("Conditions");
                            if (!xrTransition.IsEmptyElement)
                            {
                                XmlReader xrCondition = xrTransition.ReadSubtree();
                                int i = 0;
                                while (xrCondition.ReadToFollowing("Condition"))
                                {
                                    Conditions.Add(new Condition(xrCondition, this, i++));
                                }
                            }
                        }

                        public class Condition
                        {
                            private string _operator;
                            public string querystring;
                            private string _val2chk;

                            public Transition Parent;
                            public int Nr;

                            public Condition(XmlReader xrCondition, Transition parent, int i)
                            {
                                Nr = i;
                                Parent = parent;
                                xrCondition.ReadToFollowing("query");
                                xrCondition.Read();
                                querystring = xrCondition.Value;
                                xrCondition.ReadToFollowing("val2chk");
                                xrCondition.Read();
                                _val2chk = xrCondition.Value;
                                xrCondition.ReadToFollowing("operator");
                                xrCondition.Read();
                                _operator = xrCondition.Value;
                            }

                            public void GenerateTestSnippet(StreamWriter objWriter,int i)
                            {
                                objWriter.Write(String.Format(@"        [TestMethod]
        public void TestConditionFlow{0}Step{1}NextStep{2}ConditionNr{3}()", Parent.Parent.Parent.id, Parent.Parent.id, Parent.next, i));
                                objWriter.Write(
                                    @"
        {
            var sqlCeConnection = Factory.CreateConnection();
            //MSBuild always nest testresults three levels deep. (TestResults\UniqueFolder\Out)
            var objParams = new Hashtable();");
                        GenerateParamList(objWriter, querystring);
                        objWriter.Write(@"
            //objParams.Add(""app.userid"", 7);");

                                objWriter.WriteLine(string.Format(@"
            
            string strSql = _configuration.GetQueryFromCondition(/* Flow id */ {0}, /* Step id */ {1}, /* Next step id */ {2}, /* Condition Nr. */ {3}
, objParams);",Parent.Parent.Parent.id,Parent.Parent.id, Parent.next,i));
                                objWriter.WriteLine(@"

            var command = Factory.CreateCommand(strSql, sqlCeConnection);

            try
            {
                //TODO: Expected result
                sqlCeConnection.Open();
                var ret = command.ExecuteScalar();
                Assert.AreEqual(0, ret);
            }
            catch (Exception objException)
            {
                throw Factory.CreateException(String.Format(@""Fout tijdens uitlezen gegegevens.\r\nQuery:\r\n{0}"", strSql), objException);
            }
            finally
            {
                if (sqlCeConnection != null) sqlCeConnection.Close();
            }
        }"); 
                            }
                        }

                        public int NextStepId
                        {
                            get { return next; }
                        }

                        public void GenerateTestSnippet(StreamWriter objWriter)
                        {
                            int i = 0;
                            foreach(Condition condition in this.Conditions)
                            {
                                condition.GenerateTestSnippet(objWriter, i++);
                            }
                        }
                    }

                    public int Id
                    {
                        get { return id; }
                    }

                    public void GenerateTestSnippet(StreamWriter objWriter)
                    {
                        foreach(Transition transition in this.Transitions)
                        {
                            transition.GenerateTestSnippet(objWriter);
                        }
                    }
                }

                public void GenerateMSTest(string path)
                {
                    string filename = path + "Flow" + id.ToString() + ".cs";
                    StreamWriter objWriter = new StreamWriter(filename);

                    objWriter.WriteLine(@"using System;
using System.Collections;
using System.Data;
using System.Data.SqlServerCe;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SDILReader;

namespace MSTest
{"); objWriter.WriteLine(@"
    [TestClass]
    public class Flow{0} : BaseTest", this.id); objWriter.WriteLine(@"
    {
        [TestInitialize()]
        public override void SetupEnviroment()
        {
            Globals.LoadOpCodes();
            _configuration = new ScreenLib.ScreenConfiguration(_xrScreen, _assemblyScripting);
            var sqlCeConnection = Factory.CreateConnection();
            String strSql;
            if (MSTest.Properties.Settings.Default.RecreateDB)
            {
                try
                {
                    if (File.Exists(_cMyTestFieldserviceSdf))
                    {
                        File.Delete(_cMyTestFieldserviceSdf);
                    }
                    SqlCeEngine engine = Factory.CreateSqlEngine();
                    engine.CreateDatabase();
                    engine.Dispose();
                    using (StreamReader objReader = new StreamReader(_cMyTestImportTxt))
                    {
                        while (!objReader.EndOfStream)
                        {
                            strSql = objReader.ReadLine();
                            var command = Factory.CreateCommand(
                                strSql,
                                sqlCeConnection
                                );
                            try
                            {
                                sqlCeConnection.Open();
                                int numResults = command.ExecuteNonQuery();
                            }
                            catch (Exception objException)
                            {
                                throw Factory.CreateException(String.Format(@""Fout tijdens uitlezen gegegevens.\r\nQuery:\r\n{0}"", strSql), objException);
                            }
                            finally
                            {
                                if (sqlCeConnection != null) sqlCeConnection.Close();
                            }
                        }
                    }
                }
                catch (System.IO.IOException)
                {

                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
");
                    foreach (Step step in this.Steps)
                    {
                        step.GenerateTestSnippet(objWriter);
                    }
                    objWriter.WriteLine(@"
    }
}");
                    objWriter.Flush();
                }

                public bool CheckId(int flowId)
                {
                    return flowId == id;
                }

                public Step.Transition.Condition GetCondition(int stepId, int nextStepId, int conditionNr)
                {
                    foreach (var step in Steps)
                    {
                        if(step.Id == stepId)
                        {
                            foreach (var transition in step.Transitions)
                            {
                                if(transition.NextStepId == nextStepId)
                                {
                                    foreach (var condtion in transition.Conditions)
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
                foreach(Screen objScreen in screens)
                {
                    objScreen.CheckTextBoxLengths(items);
                }
            }

            public void CheckSql(Collection<ColumnInfo> items)
            {
                //CheckSql for each screen
                foreach (Screen objScreen in screens)
                {
                    objScreen.CheckSql(items);
                }
            }

            public void GenerateMSTest(string path)
            {
                if (path == null) throw new ArgumentNullException("path");
                foreach (Flow objFlow in flows)
                {
                    objFlow.GenerateMSTest(path);
                }
                foreach (Screen objScreen in screens)
                {
                    objScreen.GenerateMSTest(path);
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
            TNvarchar,
            TNxml
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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using Tensing.Camera.Interface;
using Tensing.FieldVision.Scripting;
using Tensing.Navigation;
using Tensing.Phone.Interface;
using Tensing.Printer;

public class ScriptingSystem
{
    public static string MockDoNCSql;
    public static Collection<MockSql> SqlCollection;
    public class MockSql:ISql
    {
        private string _sql;
        public MockSql(string strSql)
        {
            _sql = strSql;
        }

        public string ParseQuery(Hashtable objParams)
        {
            string ret = _sql;
            foreach (var key in objParams.Keys)
            {
                ret = ret.Replace(string.Format("${0}$", key), objParams[key].ToString());
            }
            return ret;
        }
    }

    private bool _browsing;

    private OleDbConnection _dbConnection;

    public int Alert(object txt)
    {
        throw new NotImplementedException();
    }

    public int Alert(object txt, object caption)
    {
        throw new NotImplementedException();
    }

    public int Alert(object txt, object caption, string buttons)
    {
        throw new NotImplementedException();
    }

    public int Alert(object txt, object caption, string buttons, string icon)
    {
        throw new NotImplementedException();
    }

    public string CheckDutchBankAccount(string bankRekeningNummer)
    {
        throw new NotImplementedException();
    }

    public string Concat(params object[] str)
    {
        throw new NotImplementedException();
    }

    public DateTime Date(object date)
    {
        throw new NotImplementedException();
    }

    public string datediff(DateTime a, DateTime b, string datepart)
    {
        throw new NotImplementedException();
    }

    public string DateTimeCombine(string date, string time)
    {
        throw new NotImplementedException();
    }

    public int DoSql(string sql)
    {
        throw new NotImplementedException();
    }

    public IDataReader ExecuteSelectSQL(string sql)
    {
        throw new NotImplementedException();
    }

    public void EnableBackButton(string enabled)
    {
        throw new NotImplementedException();
    }

    public void EnableBreakButton(string enabled)
    {
        throw new NotImplementedException();
    }

    public void EnableInfoButton(string enabled)
    {
        throw new NotImplementedException();
    }

    public void EnableInfoButton(string id, string enabled)
    {
        throw new NotImplementedException();
    }

    public void EnableOptionButton(string enabled)
    {
        throw new NotImplementedException();
    }

    public void EnableCommButton(string enabled)
    {
        throw new NotImplementedException();
    }

    public void EnableControl(string id, string enabled)
    {
        throw new NotImplementedException();
    }

    public void EnableProceedButton(string enabled)
    {
        throw new NotImplementedException();
    }

    public void EnableScreen(string enabled)
    {
        throw new NotImplementedException();
    }

    public string FormatDateTimeString(string date, string formatSpecifier)
    {
        throw new NotImplementedException();
    }

    public string FormatDecimalString(string dec, string formatSpecifier)
    {
        throw new NotImplementedException();
    }

    public string GenerateEngineerSequenceNumber(string userID)
    {
        throw new NotImplementedException();
    }

    public string GenerateUniqueNumber()
    {
        throw new NotImplementedException();
    }

    public string GetFVar(string name)
    {
        throw new NotImplementedException();
    }

    public int GetLength(string s)
    {
        throw new NotImplementedException();
    }

    public string GetWeekNumber(string date)
    {
        throw new NotImplementedException();
    }

    public string guid()
    {
        throw new NotImplementedException();
    }

    public bool isMatch(string input, string pattern)
    {
        throw new NotImplementedException();
    }

    public bool isSameDay(DateTime Reference, DateTime dt)
    {
        throw new NotImplementedException();
    }

    public bool isToday(DateTime dt)
    {
        throw new NotImplementedException();
    }

    public void Print(int bonnr)
    {
        throw new NotImplementedException();
    }

    public string ResolveParam(string str)
    {
        throw new NotImplementedException();
    }

    public string RoundDown(string nr, int increment)
    {
        throw new NotImplementedException();
    }

    public string RoundMinuteDown(string dtime, int increment)
    {
        throw new NotImplementedException();
    }

    public string RoundMinuteUp(string dtime, int increment)
    {
        throw new NotImplementedException();
    }

    public string RoundUp(string nr, int increment)
    {
        throw new NotImplementedException();
    }

    public void Run(string app, string args)
    {
        throw new NotImplementedException();
    }

    public void RunAndWait(string app, string args)
    {
        throw new NotImplementedException();
    }

    public static void SetAppVar(string name, object var)
    {
        throw new NotImplementedException();
    }

    public void SetControlFocus(string id)
    {
        throw new NotImplementedException();
    }

    public void SetFlag(string name, bool val)
    {
        throw new NotImplementedException();
    }

    public void SetFormText(string text)
    {
        throw new NotImplementedException();
    }

    public void SetProceedButtonVisible(string visible)
    {
        throw new NotImplementedException();
    }

    public void SetRequired(string id, bool required)
    {
        throw new NotImplementedException();
    }

    public void SetVar(string name, object var)
    {
        throw new NotImplementedException();
    }

    public void SetWaitCursor(bool enabled)
    {
        throw new NotImplementedException();
    }

    public void ShellExecute(string app)
    {
        throw new NotImplementedException();
    }

    public string substract(int a, int b)
    {
        throw new NotImplementedException();
    }

    public DateTime toDate(object date)
    {
        throw new NotImplementedException();
    }

    public double toDouble(object d)
    {
        throw new NotImplementedException();
    }

    public int toInt(object Int)
    {
        throw new NotImplementedException();
    }

    public string TrimSpecial(string str)
    {
        throw new NotImplementedException();
    }

    public void UndoUpdateMaterialStocks(string activityID, string matid)
    {
        throw new NotImplementedException();
    }

    public string UpdateMaterialStocks(string activityID)
    {
        throw new NotImplementedException();
    }

    public void DoDateTimeSync()
    {
        throw new NotImplementedException();
    }

    public IScriptingNavigation Navigation()
    {
        throw new NotImplementedException();
    }

    public INavigation GetNavigation()
    {
        throw new NotImplementedException();
    }

    public IPrinterBase GetPrinter()
    {
        throw new NotImplementedException();
    }

    public ICamera GetCamera()
    {
        throw new NotImplementedException();
    }

    public IPhone GetPhone()
    {
        throw new NotImplementedException();
    }

    public ISpinControl Spin(string id)
    {
        throw new NotImplementedException();
    }

    public static ICheckBoxControl CheckBox(string id)
    {
        throw new NotImplementedException();
    }

    public IDateTimePickerControl DateTimePicker(string id)
    {
        throw new NotImplementedException();
    }

    public IListComboBoxControl ListComboBox(string id)
    {
        throw new NotImplementedException();
    }

    public IDynamicForm DynamicForm()
    {
        throw new NotImplementedException();
    }

    public IScreen CurrentScreen()
    {
        throw new NotImplementedException();
    }

    public ISymbolScanningDevice Scan(string id)
    {
        throw new NotImplementedException();
    }

    public IWebserviceControl Web(string id)
    {
        throw new NotImplementedException();
    }

    public static IScriptingControl GetFormControl(string id)
    {
        throw new NotImplementedException();
    }

    public object[] GetFormControls(Type controlType)
    {
        throw new NotImplementedException();
    }

    public ISignatureControl Signature(string id)
    {
        throw new NotImplementedException();
    }

    public IImageControl Image(string id)
    {
        throw new NotImplementedException();
    }

    public int SendTable(string tableName)
    {
        throw new NotImplementedException();
    }

    public int SendFile(string sourceFileName, bool delete)
    {
        throw new NotImplementedException();
    }

    public int SendFile(string sourceFileName, string destinationFileName)
    {
        throw new NotImplementedException();
    }

    public int SendFile(string sourceFileName, string destinationFileName, bool delete)
    {
        throw new NotImplementedException();
    }

    public int SendFolder()
    {
        throw new NotImplementedException();
    }

    public int SendFolder(string sourceFolder)
    {
        throw new NotImplementedException();
    }

    public int SendFolder(string sourceFolder, bool MoveOrDelete)
    {
        throw new NotImplementedException();
    }

    public int SendFolder(string sourceFolder, string destinationFolder)
    {
        throw new NotImplementedException();
    }

    public int SendFolder(string sourceFolder, string destinationFolder, bool MoveOrDelete)
    {
        throw new NotImplementedException();
    }

    public int SendFolder(string sourceFolder, string destinationFolder, string moveFolder)
    {
        throw new NotImplementedException();
    }

    public int SendFolder(string sourceFolder, string destinationFolder, string moveFolder, bool MoveOrDelete)
    {
        throw new NotImplementedException();
    }

    public bool StartCommunication()
    {
        throw new NotImplementedException();
    }

    public bool StopCommunication()
    {
        throw new NotImplementedException();
    }

    public bool Browsing
    {
        get { return _browsing; }
    }

    public OleDbConnection DBConnection
    {
        get { return _dbConnection; }
    }

    public static void DoNCSql(string strSQl)
    {
        Debug.WriteLine("DoNCSql");
        Debug.WriteLine(strSQl);
        MockDoNCSql = strSQl;
        if(SqlCollection != null)
        {
            SqlCollection.Add(new MockSql(strSQl));
        }
    }

    public static object GetAppVar(string strVarName)
    {
        Debug.WriteLine("GetAppVar");
        Debug.WriteLine(strVarName);
        return "31";
    }
    public class MockButton
    {
        public bool Enabled;
    }
    public static MockButton Button(string strControlName)
    {
        Debug.WriteLine("new Button(strControlName)");
        Debug.WriteLine(strControlName);
        return new MockButton();
    }
    public class MockText
    {
        public bool Enabled;
        public bool Visible;
    }
    public static MockText Text(string strControlName)
    {
        Debug.WriteLine("new Text(strControlName)");
        Debug.WriteLine(strControlName);
        return new MockText();
    }
    public class MockCombobBox: IComboBoxControl
    {
        private readonly string _strControlName;
        public bool _enabled;
        public bool _visible;

        public MockCombobBox(string strControlName)
        {
            _strControlName = strControlName;
        }

        public bool Visible
        {
            get { return _visible; }
            set { _visible = value;
            Debug.WriteLine(string.Format("ComboBox(\"{0}\").Visible = {1}",_strControlName,_visible));
            }
        }

        public void Disable(bool b)
        {
            throw new NotImplementedException();
        }

        public bool Validate()
        {
            throw new NotImplementedException();
        }

        public void SetError()
        {
            throw new NotImplementedException();
        }

        public void ClearError()
        {
            throw new NotImplementedException();
        }

        public bool Focus()
        {
            throw new NotImplementedException();
        }

        public Color BackColor
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public Color ForeColor
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value;
            Debug.WriteLine(string.Format("ComboBox(\"{0}\").Visible = {1}", _strControlName, _visible));
            }
        }

        #region Implementation of IScriptingControl

        public string InternalID
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string ID
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        #endregion

        #region Implementation of ICombiControl

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void Fill()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of IComboBoxControl

        public string value
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string text
        {
            get { throw new NotImplementedException(); }
        }

        public bool ReadOnly
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public int ItemCount
        {
            get { throw new NotImplementedException(); }
        }

        public bool InitialFilling
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public bool DynamicFilling
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        #endregion
    }
    public static MockCombobBox ComboBox(string strControlName)
    {
        Debug.WriteLine("new ComboBox(strControlName)");
        Debug.WriteLine(strControlName);
        return new MockCombobBox(strControlName);
    }

    public class MockGrid
    {
        private readonly string _gridName;

        public MockGrid(string gridName)
        {
            _gridName = gridName;
        }

        public void Clear()
        {
            Debug.WriteLine("Grid.Clear()");
        }

        public void SetQuery(string toString)
        {
            Debug.WriteLine("Grid.SetQuery(string toString)");
            Debug.WriteLine(toString);
        }

        public void FillTheGrid()
        {
            Debug.WriteLine("Grid.FillTheGrid()");
        }

        public void SetRowColor(int p0, string p1, Color fromArgb, Color p3)
        {
            throw new NotImplementedException();
        }
    }

    public static MockGrid Grid(string gridName)
    {
        Debug.WriteLine("new Grid(" + gridName + ")");
        return new MockGrid(gridName);
    }
    
    public static string strMmt;

    public static string GetVar(string key)
    {
        if (key == "mmtid") return strMmt;
        return null;
    }

    public static void SetVar(string questionlistId, string empty) { }

    public static void WriteLog(string initializequestionlist)
    {
        throw new NotImplementedException();
    }

    public static string ExecuteScalarSQL(string sSql)
    {
        throw new NotImplementedException();
    }
}


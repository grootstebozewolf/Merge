using System;
using System.Collections;
using System.Data;
using System.Diagnostics;

namespace FieldCore
{
    public enum FieldCoreActions
    {
        /// <summary>
        /// Start Action
        /// </summary>
        Start,
        /// <summary>
        /// Stop Action
        /// </summary>
        Stop,
        /// <summary>
        /// Signal Action
        /// </summary>
        Signal
    };
    public delegate void GlobalEventDelegate(object sender, FieldCoreActions Action, string Destination, string Message);
    public interface GlobalEvent
    {
        event GlobalEventDelegate GlobalNotifyEvent;
        GlobalEvent GetInstance();
    }
    public class DynamicForm
    {
        public FcNavBar fcNavBar1;
    }

    public class FcNavBar
    {
        public FCButton PrevButton;
    }

    public class FCButton
    {
        public event EventHandler<EventArgs> Click;
    }

    public class Core
    {
        public static Core GetInstance()
        {
            return new Core();
        }

        private Core()
        {
        }

        public static string ExecuteSelectSqlValue;

        public DataBase cnDB = new DataBase();

        public static IDataReader MockDataReader;
        public class GlobalsFlowObject
        {
            public void PutValue(string key, string  value)
            {
                Debug.WriteLine("public void PutValue(string key, string value)");
                Debug.WriteLine("key:" + key);
                Debug.WriteLine("value:" + value);
            }

            public void SetValue(string key, string value)
            {
                Debug.WriteLine("public void SetValue(string key, string value)");
                Debug.WriteLine("key:" + key);
                Debug.WriteLine("value:" + value);
            }
        }
        public GlobalsFlowObject GlobalsFlow;
        public FCXmlCollection GlobalsApp = new FCXmlCollection();

        public DynamicForm DynamicForm
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }

    public class FCXmlCollection
    {
        public static string GetValueString;
        public string GetValue(string key)
        {
            return GetValueString;
        }

        public void PutValue(string organisationid, string toString)
        {
            throw new NotImplementedException();
        }

        public void SetValue(string organisationid, string toString)
        {
            throw new NotImplementedException();
        }
    }

    public class DataBase
    {
        public static string ExecuteSelectSQLParamValue;
        public IDataReader ExecuteSelectSQL(string strSql)
        {
            Debug.WriteLine("ExecuteSelectSQL(string strSql)");
            Debug.WriteLine(strSql);
            ExecuteSelectSQLParamValue = strSql;
            return Core.MockDataReader;
        }
    }

// ReSharper disable InconsistentNaming
    public class FCOwnerDrawnList
// ReSharper restore InconsistentNaming
    {
        public ICollection Items;
    }
}
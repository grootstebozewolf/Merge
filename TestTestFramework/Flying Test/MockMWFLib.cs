using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using FieldCore;
using Flying_Test;
using Moq;

namespace Tensing.FieldVision.Scripting.GeneratedPlugin
{
    public partial class MWF
    {
        public enum MaterialMutationReason { MaterialAdvised = 6, MaterialUsed = 3, MaterialOrdered = 4, MaterialReturned = 5 };

        public static MWF Instance = new MWF();
        
        private MWF()
        {
        

        }

        public static decimal FakeDecimalDataValue = 0;

        public decimal GetDecimalDataValue(string sQuery)
        {
            return FakeDecimalDataValue;
        }

        private void LogCall(string message)
        {
            
        }

        public static int ShowLangAlertYesNoReturnValue;

        public int ShowLangAlertYesNo(string textId)
        {
            return ShowLangAlertYesNoReturnValue;
        }
        public static string ShowLangAlertOKParam;
        private void ShowLangAlertOK(string textId)
        {
            ShowLangAlertOKParam = textId;
        }
        public static int FakeGetIntDataValue;
        public int GetIntDataValue(string sQuery)
        {
            Debug.WriteLine("GetIntDataValue(string sQuery)");
            Debug.WriteLine(sQuery);
            return FakeGetIntDataValue;
        }


        public static string FakeGetStringDataValue;
        public string GetStringDataValue(string sQuery)
        {
            Debug.WriteLine("GetStringDataValue(string sQuery)");
            Debug.WriteLine(sQuery);
            return FakeGetStringDataValue;
        }

        public static string FakeGetActivityId;
        public string GetActivityId()
        {
            Debug.WriteLine("GetActivityId(string sQuery)");
            return FakeGetActivityId;
        } 

        //private void UpdateLabour(int labourstatusId, Hashtable ht) { }
        public static string FakeGetSingleValueFromSQLValue;
        private string GetSingleValueFromSQL(string s)
        {
            return FakeGetSingleValueFromSQLValue;
        }


        public class MwfParentTable
        {
            public MwfParentTable(string param0, string param1, string param2)
            {
                Param0 = param0;
                Param1 = param1;
                Param2 = param2;
            }

            public string Param0;
            public string Param1;
            public string Param2;
        }

        public static ICollection<MwfParentTable> ParentTables;
        public int? InsertParentTableReturnValue;

        private int InsertParentTable(string param0, string param1, string param2)
        {
            if(ParentTables!=null)
            {
                ParentTables.Add(new MwfParentTable(param0, param1, param2));
            }
            return InsertParentTableReturnValue??0;
        }

        public class MwfChildTable
        {
            public MwfChildTable(int param0, string param1, string param2)
            {
                Param0 = param0;
                Param1 = param1;
                Param2 = param2;
            }

            public int Param0;
            public string Param1;
            public string Param2;
        }

        public static ICollection<MwfChildTable> ChildTables;
        public int? InsertChildTableReturnValue;

        private int InsertChildTable(int param0, string param1, string param2)
        {
            if (ChildTables != null)
            {
                ChildTables.Add(new MwfChildTable(param0, param1, param2));
            }
            return InsertChildTableReturnValue ?? 0;
        }

        private void SendTables() { }

        private void LogCall(string getorganisationid, string toString) { }

        /// <summary>
        /// Check if a duration string is valid
        /// </summary>
        /// <param name="duration">Duration string in formar "HH:mm".</param>
        /// <returns>Returns true if duration string is valid.</returns>
        //public bool IsValidDuration(string duration)
        //{
        //    int hours = 0;
        //    int minutes = 0;
        //    return IsValidDuration(duration, ref hours, ref minutes);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="duration"></param>
        /// <returns></returns>
        //public int ConvertDurationToMinutes(string duration)
        //{
        //    int hours = 0;
        //    int minutes = 0;
        //    if (IsValidDuration(duration, ref hours, ref minutes))
        //    {
        //        return hours * 60 + minutes;
        //    }
        //    else
        //    {
        //        return -1;
        //    }
        //}

        //private int GetCurrentActivityStatus(string activity_id)
        //{
        //    int retval = -1;

        //    retval =
        //        Convert.ToInt32(
        //            GetSingleValueFromSQL(String.Format(@"SELECT activitystatustype_id FROM fv_activity WHERE id = '{0}'", activity_id)));
        //    LogCall(String.Format(@"GetCurrenctActivityStatus: status = {0}", retval));
        //    return retval;
        //}


        /// <summary>
        /// Get the duration of a single labour record
        /// </summary>
        /// <param name="labour_id">labour record id</param>
        /// <returns>returns the duration in minutes (int), 0 if an error occurs</returns>
        //private int GetLabourDuration(string labour_id)
        //{
        //    LogCall("GetLabourDuration");

        //    try
        //    {
        //        return System.Convert.ToInt32(GetSingleValueFromSQL(String.Format("SELECT DATEDIFF(mi, rostartdt, roenddt) as orgduur FROM fv_labour WHERE id = '{0}'", labour_id)));
        //    }
        //    catch
        //    {
        //        return 0;
        //    }
        //}

        /// <summary>
        /// Get the sequencenumber of a single labour record
        /// </summary>
        /// <param name="labour_id">labour record id</param>
        /// <returns>returns the sequence number (int), 0 if an error occurs</returns>
        //private int GetLabourSequencenumber(string labour_id)
        //{
        //    LogCall("GetLabourSequencenumber");

        //    try
        //    {
        //        return System.Convert.ToInt32(GetSingleValueFromSQL(String.Format("SELECT sequenceno AS mod_seq_no FROM fv_labour WHERE id = '{0}'", labour_id)));
        //    }
        //    catch
        //    {
        //        return 0;
        //    }
        //}

        /// <summary>
        /// Check if a string value can be converted to a integer
        /// </summary>
        /// <param name="value">string value</param>
        /// <returns>return true if string has a numeric value</returns>
        public bool IsValidInt(string value)
        {
            try
            {
                int iValue = System.Convert.ToInt32(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Check if a string value can be converted to a decimal/double
        /// </summary>
        /// <param name="value">string value to check</param>
        /// <returns>true if valid, false if not</returns>
        public bool IsValidDecimal(string value)
        {
            bool isValid = true;
            double testValue = 0.0;

            try
            {
                testValue = System.Convert.ToDouble(value);
            }
            catch
            {
                isValid = false;
            }
            return isValid;
        }

        /// <summary>
        /// Returns the hours from a duration string
        /// </summary>
        /// <param name="duration">Duration string (HH:mm)</param>
        /// <returns>Hours (int)</returns>
        public int HoursFromDuration(string duration)
        {
            int hrs = 0;

            if (duration.Trim().Length > 0)
            {
                int colPos = duration.IndexOf(':');
                if (colPos > 0)
                {
                    if (IsValidInt(duration.Substring(0, colPos)))
                        hrs = System.Convert.ToInt32(duration.Substring(0, colPos));
                }
            }
            return hrs;
        }

        /// <summary>
        /// Returns the minutes from a duration string
        /// </summary>
        /// <param name="duration">Duration string (HH:mm)</param>
        /// <returns>Minutes (int)</returns>
        public int MinutesFromDuration(string duration)
        {
            int min = 0;

            if (duration.Trim().Length > 0)
            {
                int colPos = duration.IndexOf(':');
                if (colPos > 0)
                {
                    if (IsValidInt(duration.Substring(0, colPos)))
                        min = System.Convert.ToInt32(duration.Substring(colPos + 1, duration.Trim().Length - colPos - 1));
                }
            }
            return min;
        }

        /// <summary>
        /// Check if a duration string is valid
        /// </summary>
        /// <param name="duration">Duration string in formar "HH:mm".</param>
        /// <returns>Returns true if duration string is valid.</returns>
        //public bool IsValidDuration(string duration, ref int hours, ref int minutes)
        //{
        //    bool res = false;

        //    if (duration.Trim().Length > 0)
        //    {
        //        int colPos = duration.IndexOf(':');
        //        if (colPos > 0)
        //        {
        //            string hrs = duration.Substring(0, colPos);
        //            string min = duration.Substring(colPos + 1, duration.Trim().Length - colPos - 1);
        //            if (IsValidInt(hrs) && IsValidInt(min))
        //            {
        //                if ((System.Convert.ToInt32(min) >= 0) && (System.Convert.ToInt32(min) <= 59))
        //                {
        //                    res = true;
        //                    hours = System.Convert.ToInt32(hrs);
        //                    minutes = System.Convert.ToInt32(min);
        //                }
        //            }
        //        }
        //    }
        //    LogCall(String.Format("IsValidDuration {0}: {1}", duration, res.ToString()));
        //    return res;
        //}

        /// <summary>
        /// [MWFLib]  Get the maximum squencenumber from fv_labour filtered on activity_id.
        /// </summary>
        /// <returns>Sequence number (int)</returns>
//        private int GetMaxSequenceNo()
//        {
//            LogCall("GetMaxSequenceNo");

//            return System.Convert.ToInt32(GetSingleValueFromSQL(String.Format(@"SELECT COALESCE(MAX(sequenceno), 0) + 1 as MaxSeq
//                FROM fv_labour
//                WHERE fv_labour.activity_id = '{0}'
//            	AND fv_labour.engineer_id = {1}", GetActivityId(), ScriptingSystem.GetAppVar("userid").ToString())));
//        }

        private void CreateLabour(int labourstatusId)
        {
            throw new NotImplementedException();
        }

        //private void UpdateLabour(int labourstatusId)
        //{
        //    throw new NotImplementedException();
        //}

        //private void UpdateLabourDuration(string changedLabourId, string newDuration)
        //{
        //    throw new NotImplementedException();
        //}

                /// <summary>
        /// [MWFLib]  StatusUpdate inserts a row in fv_activitystatus and updates fv_activity
        /// </summary>
        /// <param name="status_id">The status id to use</param>
        /// <param name="withExists">Use or not use the with not exists clause</param>
        /// <param name="CheckFromStatusId">check if the current from_status is different from the to_status if so update else quit</param>
        public void StatusUpdate(int status_id, bool withExists, bool CheckFromStatusId)
        {
            throw new NotImplementedException();
        }
                /// <summary>
        /// [MWFLib]  StatusUpdate inserts a row in fv_activitystatus and updates fv_activity with a where not exists clause
        /// </summary>
        /// <param name="status_id">The status id to use</param>
        /// <param name="withExists">Use or not use the with not exists clause</param>
        public void StatusUpdate(int status_id, bool withExists)
        {
            throw new NotImplementedException();
        }

        //private static UpdateTimeDelta CreateUpdateTimeDelta(UpdateTimeDeltaArgs args)
        //{
        //    return new UpdateTimeDelta(new Mock<GlobalEvent>().Object, args);
        //}

        //private static UpdateTimeDeltaArgs CreateUpdateTimeDeltaArgs(Guid id, string tableName, string[] columnNames)
        //{
        //    return new UpdateTimeDeltaArgs(tableName, id, columnNames);
        //}

        //private UpdateTimeDelta CreateUpdateTimeDelta(UpdateTimeDeltaArgs args, Timer timeSyncTimer)
        //{
        //    return new UpdateTimeDelta(timeSyncTimer, args);
        //}
    }
}
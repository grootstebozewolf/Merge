using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Xml;

namespace MSTest
{
    public abstract class BaseTest
    {
        protected BaseTest()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomainAssemblyResolve;
            Params = ParamInit.Instance.Create_global_params();
        }

        public static Hashtable Params;

        public abstract void SetupEnviroment();

        protected readonly XmlReader _xrScreen = XmlReader.Create(@"C:\My Test\DoNCSqlWithStringFormat\Desktop\Screen.xml");
        protected readonly Assembly _assemblyScripting = Assembly.LoadFrom(@"C:\My Test\DoNCSqlWithStringFormat\Desktop\DoNCSqlWithStringFormat_scripting.dll");
        protected ScreenLib.ScreenConfiguration _configuration;
        protected readonly string _cMyTestImportTxt = @"C:\My Test\DoNCSqlWithStringFormat\import.txt";
        protected const string ConnStr = @"Data Source=C:\My Test\DoNCSqlWithStringFormat\FieldService.sdf";
        protected const string _cMyTestFieldserviceSdf = @"C:\My Test\DoNCSqlWithStringFormat\FieldService.sdf";



        #region AssemblyMethods

        private static Assembly CurrentDomainAssemblyResolve(object sender, ResolveEventArgs args)
        {
            Assembly[] currentAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            for (int i = 0; i < currentAssemblies.Length; i++)
            {
                if (currentAssemblies[i].FullName == args.Name)
                {
                    return currentAssemblies[i];
                }
            }

            return FindAssembliesInDirectory(args.Name, @"C:\My Test\Planon\Desktop\");
        }

        private static Assembly FindAssembliesInDirectory(string assemblyName, string directory)
        {
            foreach (string file in Directory.GetFiles(directory))
            {
                Assembly assm;

                if (TryLoadAssemblyFromFile(file, assemblyName, out assm))
                    return assm;
            }

            return null;
        }

        private static bool TryLoadAssemblyFromFile(string file, string assemblyName, out Assembly assm)
        {
            try
            {
                // Convert the filename into an absolute file name for 
                // use with LoadFile. 
                file = new FileInfo(file).FullName;

                if (AssemblyName.GetAssemblyName(file).FullName == assemblyName)
                {
                    assm = Assembly.LoadFile(file);
                    return true;
                }
            }
            catch
            {
                /* Do Nothing */
            }
            assm = null;
            return false;
        }

        #endregion
    }

    public class ParamInit
    {
        Hashtable objParams;
        readonly static ParamInit _instance = new ParamInit();

        private ParamInit()
        {
            objParams = new Hashtable();
            objParams.Add("app.userid",4);
            objParams.Add("flow.screen", "");
            objParams.Add("flow.handtekeningoptie", 0);
            objParams.Add("flow.activity_id", Guid.Empty);
            objParams.Add("flow.flow_id", 0);
            objParams.Add("flow.questtype", 0);
            objParams.Add("flow.debqlist_done", 0);
            objParams.Add("form.cmbstep", 0);
            objParams.Add("app.day_labour_id", Guid.Empty);
            objParams.Add("form.txtmaxsequence", 1);
            objParams.Add("app.huurauto", 0);
            objParams.Add("sub.cbbreekreden", 0);
            objParams.Add("app.verifydt", new DateTime(2012, 02, 20, 18, 55, 00).ToString(CultureInfo.InvariantCulture));
            objParams.Add("flow.verifydt", new DateTime(2012, 02, 20, 18, 55, 00).ToString(CultureInfo.InvariantCulture));
            objParams.Add("flow.activitytype_id", 0);
            objParams.Add("flow.serviceobjecttype_id", 0);
            objParams.Add("flow.selectedstartdate", new DateTime(2012, 02, 22, 9, 00, 00).ToString(CultureInfo.InvariantCulture));
            objParams.Add("flow.selectedenddate", new DateTime(2012, 02, 22, 9, 01, 00).ToString(CultureInfo.InvariantCulture));
            objParams.Add("flow.upd_labour_id", Guid.Empty);
            objParams.Add("flow.statusid", Guid.Empty);
            objParams.Add("flow.eod", 0);
            objParams.Add("flow.matmutid", Guid.Empty);
            objParams.Add("flow.mmutid", Guid.Empty);
            objParams.Add("flow.artikelid", 0);
            objParams.Add("flow.hist_id", 0);
            objParams.Add("flow.questionlist_id", 0);
            objParams.Add("form.txtdebqlistid", Guid.Empty);
            objParams.Add("form.txtdebquestid", Guid.Empty);
            objParams.Add("flow.debqlist_id", Guid.Empty);
            objParams.Add("form.txtcountnotdone", 0);
            objParams.Add("flow.quest_id", 0);
            objParams.Add("flow.nextquestion_id", 0);
            objParams.Add("flow.questionnumber", 0);
            objParams.Add("flow.messageid", Guid.Empty);
            objParams.Add("flow.sel_activity_id", Guid.Empty);
            objParams.Add("flow.activitystatus_id", Guid.Empty);
            objParams.Add("flow.continue", "dummy");
            objParams.Add("app.volledig", -1);
            objParams.Add("flow.afsluiten", 0);
            objParams.Add("flow.customer_id", 0);
            objParams.Add("flow.contact_id", 0);
            objParams.Add("flow.serviceobject_id", 0);
            objParams.Add("flow.fin_id", 0);
            objParams.Add("flow.financial_id", 0);
            objParams.Add("flow.meter_id", 0);
            objParams.Add("flow.serviceorder_id", Guid.Empty);
            objParams.Add("flow.planstart", new DateTime(2012, 3, 9, 14, 18, 0).ToString(CultureInfo.InvariantCulture));
            objParams.Add("flow.planend", new DateTime(2012, 5, 10, 14, 18, 0).ToString(CultureInfo.InvariantCulture));
            objParams.Add("flow.actid", Guid.Empty);
            objParams.Add("flow.orderid", Guid.Empty);
            objParams.Add("app.carid", 0);
            objParams.Add("flow.end_status", 0);
            objParams.Add("flow.status", 0);
        }

        public static ParamInit Instance
        {
            get { return _instance; }
        }

        public Hashtable Create_global_params()
        {
            return objParams;
        }
    }
}

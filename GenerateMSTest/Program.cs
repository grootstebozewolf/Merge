using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.IO;
using UpdateUI;

namespace GenerateMSTest
{
    class Program
    {
        private static string CurrentDomainBasePath;

        static void Main(string[] args)
        {
            SDILReader.Globals.LoadOpCodes();
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomainAssemblyResolve;
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(MyUnhandledExceptionEventHandler);
            CurrentDomainBasePath = args[0].ToLower().Replace("screen.xml", "");
            var xrScreen = XmlReader.Create(args[0]);
            Assembly assemblyScripting = Assembly.LoadFrom(args[1]);
            var objScreenConfiguration = new ScreenLib.ScreenConfiguration(xrScreen, assemblyScripting);
            objScreenConfiguration.GenerateMSTest(args[2]+"\\");
        }
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

            return FindAssembliesInDirectory(args.Name, CurrentDomainBasePath);
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
        static void MyUnhandledExceptionEventHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            Exception innerException = e.InnerException;
            while (innerException != null)
            {
                innerException = innerException.InnerException;
            }
        }
    }
}

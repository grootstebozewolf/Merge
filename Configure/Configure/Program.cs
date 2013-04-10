using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Configure
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly myAssembly = Assembly.GetExecutingAssembly();
            StreamReader reader = new StreamReader(myAssembly.GetManifestResourceStream("Configure.Resources.file.txt"));
            Console.WriteLine(reader.ReadToEnd());
        }

    }
}

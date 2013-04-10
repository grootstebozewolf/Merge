using System.Runtime.InteropServices;
using System.Text;

namespace UpdateUI
{
    class FilesystemLib
    {
        [DllImport("kernel32", EntryPoint = "GetShortPathNameA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int GetShortPathName([MarshalAs(UnmanagedType.VBByRefStr)] ref string lpszLongPath, StringBuilder lpszShortPath, int cchBuffer);

        public static string GetShortPathName(ref string strNewDir)
        {
            StringBuilder objBuilder = new StringBuilder(0x400);
            int intLength = GetShortPathName(ref strNewDir, objBuilder, objBuilder.Capacity);
            return objBuilder.ToString(0, intLength);
        }

        public static string GetStrNewDir(bool blnIsForDesktop, bool blnIsForWindowsCE, bool blnIsForPocketPC, bool blnIsPPC2003Device, bool blnIsWM50Device, string text)
        {
            string strNewDir = text;
            if (blnIsForWindowsCE) strNewDir += @"\SD\Mobile\Client WindowsCE";
            if (blnIsForPocketPC && !blnIsPPC2003Device && !blnIsWM50Device) strNewDir += @"\SD\Mobile\Client PocketPC\Mobile 6.1 en hoger";
            if (blnIsForPocketPC && !blnIsPPC2003Device && blnIsWM50Device) strNewDir += @"\SD\Mobile\Client PocketPC\Mobile 5 and 6";
            if (blnIsForPocketPC && blnIsPPC2003Device) strNewDir += @"\SD\Mobile\Client PocketPC\PocketPC 2003";
            if (blnIsForDesktop) strNewDir += @"\SD\Mobile\Client Desktop";
            return strNewDir;
        }
    }
}

namespace UpdateStartUp
{
    internal class Program
    {
        private static void Main()
        {
            const string strOldStartUp = @"C:\Temp\Old\StartUp.xml";
            const string strNewStartUp = @"C:\Temp\New\StartUp.xml";
            const string strOutputFileStartUp = @"C:\Temp\StartUp.xml";
            UpdateLib.UpdateStartUpXML(strOldStartUp, strOutputFileStartUp, strNewStartUp, false, true);
            const string strOld = @"C:\Temp\Old\FVCommCE.xml";
            const string strNew = @"C:\Temp\New\FVcommCE.xml";
            const string strOutputFile = @"C:\Temp\FVcommCE.xml";
            UpdateLib.UPdateFVcommCEXML(strOld, strOutputFile, strNew);
        }
    }
}
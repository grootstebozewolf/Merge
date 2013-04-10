using System.Collections.ObjectModel;
using System.Text;
using System.Xml;

namespace UpdateUI
{
    public class Device
    {
        public string TypeName;
        public string StorageCardRootName;
        public string strFormSettings;
        public override string ToString()
        {
            return TypeName;
        }
    }

    public class DeviceSettings : Collection<Device>
    {

        public DeviceSettings(string strDeviceSettingsPath)
        {
           
            using (XmlTextReader xr = new XmlTextReader(strDeviceSettingsPath))
            {
                //Lees device settings
                xr.Read();
                while (xr.Read())
                {
                    if (xr.NodeType == XmlNodeType.Element)
                    {
                        // Check we do have a valid control. If so get the name and value.
                        if (xr.Name == "Device")
                        {
                            // Check we do have a valid control. If so get the name and value.
                            xr.MoveToNextAttribute();
                            Device objDevice = new Device();
                            objDevice.TypeName = xr.Value;
                            xr.ReadToFollowing("StorageCardRootName");
                            xr.Read();
                            objDevice.StorageCardRootName = xr.Value;
                            xr.ReadToFollowing("FormSettings");
                            if(!xr.IsEmptyElement)
                            {
                                StringBuilder objStringBuilder = new StringBuilder();
                                XmlWriter xw = XmlWriter.Create(objStringBuilder);
                                xw.WriteNode(xr, true);
                                xw.Flush();
                                objDevice.strFormSettings = objStringBuilder.ToString();
                            }
                            
                            Add(objDevice);
                        }
                        else
                            continue;
                    }
                }
            }
        }
    }
}

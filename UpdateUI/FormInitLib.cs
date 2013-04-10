using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace UpdateUI
{
    public class FormInitLib
    {
        private readonly Control.ControlCollection colControls;
        public FormInitLib(Control.ControlCollection controls)
        {
            colControls = controls;
        }

        public void Load(string strConfigPath)
        {
            using (XmlTextReader xr = new XmlTextReader(strConfigPath))
            {
                //Lees configuratie
                xr.Read();
                ReadCycleThroughControls(xr, colControls);
            }
        }

        private void WriteCycleThroughControls(XmlTextWriter xw,
                           Control.ControlCollection controls)
        {
            foreach (Control c in controls)
            {
                if (c is TextBox)
                {
                    xw.WriteStartElement(c.GetType().ToString());
                    xw.WriteAttributeString("TypeName", c.Name);
                    xw.WriteAttributeString("Text", c.Text);
                    xw.WriteEndElement();
                }
                else if (c is CheckBox)
                {
                    xw.WriteStartElement(c.GetType().ToString());
                    xw.WriteAttributeString("TypeName", c.Name);
                    xw.WriteAttributeString("Checked",
                               ((CheckBox)c).Checked.ToString());
                    xw.WriteEndElement();
                }
                else if (c is RadioButton)
                {
                    xw.WriteStartElement(c.GetType().ToString());
                    xw.WriteAttributeString("TypeName", c.Name);
                    xw.WriteAttributeString("Checked",
                               ((RadioButton)c).Checked.ToString());
                    xw.WriteEndElement();
                }
                else if (c is ComboBox)
                {
                    xw.WriteStartElement(c.GetType().ToString());
                    xw.WriteAttributeString("TypeName", c.Name);
                    xw.WriteAttributeString("SelectedIndex",
                               ((ComboBox)c).SelectedIndex.ToString());
                    xw.WriteEndElement();
                }
                else if ((c is GroupBox) || (c is Panel))
                {
                    WriteCycleThroughControls(xw, c.Controls);
                }

            }
        }

        private void ReadCycleThroughControls(XmlReader xr,
                                        Control.ControlCollection controls)
        {
            string controltype;
            Control c;
            while (xr.Read())
            {
                if (xr.NodeType == XmlNodeType.Element || xr.IsEmptyElement == false)
                {
                    controltype = xr.Name;
                    // Check we do have a valid control. If so get the name and value.
                    string controlname;
                    string controlvalue;
                    if (controltype.StartsWith("System.Windows.Forms"))
                    {
                        xr.MoveToNextAttribute();
                        controlname = xr.Value;
                        xr.MoveToNextAttribute();
                        controlvalue = xr.Value;
                    }
                    else
                        continue;

                    // set local control to null
                    c = null;

                    // check control type is valid string and then get the control
                    if (controltype.Length > 0)
                        c = GetControl(controlname, controls);

                    // move back to the element
                    xr.MoveToElement();

                    // if GetControl returns null then control was not found, so skip
                    if (c == null)
                        continue;

                    // Set the control according to type
                    if (c is TextBox)
                        c.Text = controlvalue;
                    else if (c is CheckBox)
                        ((CheckBox)c).Checked = Convert.ToBoolean(controlvalue);
                    else if (c is RadioButton)
                        ((RadioButton)c).Checked = Convert.ToBoolean(controlvalue);
                    else if (c is ComboBox)
                        ((ComboBox)c).SelectedIndex = Convert.ToInt32(controlvalue);
                }
            }
        }

        private Control GetControl(string controlname, Control.ControlCollection controls)
        {
            foreach (Control c in controls)
            {
                if ((c is GroupBox) || (c is Panel))
                {
                    Control controlInGroupBox = GetControl(controlname, c.Controls);
                    if (controlInGroupBox != null)
                        return controlInGroupBox;
                }
                else if (c.Name == controlname)
                {
                    return c;
                }
            }
            return null;
        }

        public void Save(string path)
        {
            using (XmlTextWriter xw = new XmlTextWriter(path, Encoding.UTF8))
            {
                xw.WriteStartDocument(true);
                xw.WriteStartElement("Configuration");
                WriteCycleThroughControls(xw, colControls);
                xw.WriteFullEndElement();
            }
        }

        public void Load(Device device)
        {
            MemoryStream memStream = new MemoryStream();
            byte[] data = Encoding.Unicode.GetBytes(device.strFormSettings);
            memStream.Write(data, 0, data.Length);
            memStream.Position = 0;
            using (XmlReader xr = XmlReader.Create(memStream))
            {
                //Lees configuratie
                xr.Read();
                ReadCycleThroughControls(xr, colControls);
            }
        }
    }
}

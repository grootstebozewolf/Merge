﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4200
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by xsd, Version=2.0.50727.3038.
// 
namespace UpdateLib {
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
    public partial class config {
        [XmlAttribute(AttributeName="noNamespaceSchemaLocation", Namespace="http://www.w3.org/2001/XMLSchema-instance")]
        public string noNamespaceSchemaLocation = "file:///C:/Projects/Development/Main/Source/Mobile/Tools/FVInstallerCE/TensingInstall.xsd";
        private configRun[] sequenceField;
        
        private configSection[] sectionField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("run", IsNullable=false)]
        public configRun[] sequence {
            get {
                return this.sequenceField;
            }
            set {
                this.sequenceField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("section")]
        public configSection[] section {
            get {
                return this.sectionField;
            }
            set {
                this.sectionField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class configRun {
        
        private configRunReboot rebootField;
        
        private bool rebootFieldSpecified;
        
        private int rebootdelayField;
        
        private bool rebootdelayFieldSpecified;
        
        private string sectionField;
        
        /// <remarks/>
        public configRunReboot reboot {
            get {
                return this.rebootField;
            }
            set {
                this.rebootField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool rebootSpecified {
            get {
                return this.rebootFieldSpecified;
            }
            set {
                this.rebootFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public int rebootdelay {
            get {
                return this.rebootdelayField;
            }
            set {
                this.rebootdelayField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool rebootdelaySpecified {
            get {
                return this.rebootdelayFieldSpecified;
            }
            set {
                this.rebootdelayFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public string section {
            get {
                return this.sectionField;
            }
            set {
                this.sectionField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum configRunReboot {
        
        /// <remarks/>
        yes,
        
        /// <remarks/>
        no,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class killType {
        
        private killTypeMode modeField;
        
        private bool modeFieldSpecified;
        
        private string valueField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public killTypeMode mode {
            get {
                return this.modeField;
            }
            set {
                this.modeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool modeSpecified {
            get {
                return this.modeFieldSpecified;
            }
            set {
                this.modeFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum killTypeMode {
        
        /// <remarks/>
        firstmatch,
        
        /// <remarks/>
        killemall,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class execType {
        
        private execTypeWaitforprocess waitforprocessField;
        
        private bool waitforprocessFieldSpecified;
        
        private int timeoutField;
        
        private bool timeoutFieldSpecified;
        
        private string argumentsField;
        
        private string valueField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public execTypeWaitforprocess waitforprocess {
            get {
                return this.waitforprocessField;
            }
            set {
                this.waitforprocessField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool waitforprocessSpecified {
            get {
                return this.waitforprocessFieldSpecified;
            }
            set {
                this.waitforprocessFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int timeout {
            get {
                return this.timeoutField;
            }
            set {
                this.timeoutField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool timeoutSpecified {
            get {
                return this.timeoutFieldSpecified;
            }
            set {
                this.timeoutFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string arguments {
            get {
                return this.argumentsField;
            }
            set {
                this.argumentsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum execTypeWaitforprocess {
        
        /// <remarks/>
        yes,
        
        /// <remarks/>
        no,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class rebootType {
        
        private rebootTypeInstallationfinished installationfinishedField;
        
        private bool installationfinishedFieldSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public rebootTypeInstallationfinished installationfinished {
            get {
                return this.installationfinishedField;
            }
            set {
                this.installationfinishedField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool installationfinishedSpecified {
            get {
                return this.installationfinishedFieldSpecified;
            }
            set {
                this.installationfinishedFieldSpecified = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum rebootTypeInstallationfinished {
        
        /// <remarks/>
        yes,
        
        /// <remarks/>
        no,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class exitType {
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class waitType {
        
        private waitTypeType typeField;
        
        private int timeoutField;
        
        private bool timeoutFieldSpecified;
        
        private string valueField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public waitTypeType type {
            get {
                return this.typeField;
            }
            set {
                this.typeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int timeout {
            get {
                return this.timeoutField;
            }
            set {
                this.timeoutField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool timeoutSpecified {
            get {
                return this.timeoutFieldSpecified;
            }
            set {
                this.timeoutFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum waitTypeType {
        
        /// <remarks/>
        seconds,
        
        /// <remarks/>
        cab,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class registryType {
        
        private registryTypeType typeField;
        
        private string valueField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public registryTypeType type {
            get {
                return this.typeField;
            }
            set {
                this.typeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum registryTypeType {
        
        /// <remarks/>
        create,
        
        /// <remarks/>
        delete,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ioType {
        
        private ioTypeType typeField;
        
        private ioTypeAttributes attributesField;
        
        private bool attributesFieldSpecified;
        
        private string valueField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ioTypeType type {
            get {
                return this.typeField;
            }
            set {
                this.typeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ioTypeAttributes attributes {
            get {
                return this.attributesField;
            }
            set {
                this.attributesField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool attributesSpecified {
            get {
                return this.attributesFieldSpecified;
            }
            set {
                this.attributesFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum ioTypeType {
        
        /// <remarks/>
        copy,
        
        /// <remarks/>
        delete,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum ioTypeAttributes {
        
        /// <remarks/>
        clear,
        
        /// <remarks/>
        preserve,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class sqlType {
        
        private sqlTypeType typeField;
        
        private string databaseField;
        
        private string valueField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public sqlTypeType type {
            get {
                return this.typeField;
            }
            set {
                this.typeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string database {
            get {
                return this.databaseField;
            }
            set {
                this.databaseField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum sqlTypeType {
        
        /// <remarks/>
        script,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class installType {
        
        private installTypeType typeField;
        
        private installTypeRun runField;
        
        private bool runFieldSpecified;
        
        private installTypeWaitforprocess waitforprocessField;
        
        private bool waitforprocessFieldSpecified;
        
        private int timeoutField;
        
        private bool timeoutFieldSpecified;
        
        private installTypeMode modeField;
        
        private bool modeFieldSpecified;
        
        private string valueField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public installTypeType type {
            get {
                return this.typeField;
            }
            set {
                this.typeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public installTypeRun run {
            get {
                return this.runField;
            }
            set {
                this.runField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool runSpecified {
            get {
                return this.runFieldSpecified;
            }
            set {
                this.runFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public installTypeWaitforprocess waitforprocess {
            get {
                return this.waitforprocessField;
            }
            set {
                this.waitforprocessField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool waitforprocessSpecified {
            get {
                return this.waitforprocessFieldSpecified;
            }
            set {
                this.waitforprocessFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int timeout {
            get {
                return this.timeoutField;
            }
            set {
                this.timeoutField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool timeoutSpecified {
            get {
                return this.timeoutFieldSpecified;
            }
            set {
                this.timeoutFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public installTypeMode mode {
            get {
                return this.modeField;
            }
            set {
                this.modeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool modeSpecified {
            get {
                return this.modeFieldSpecified;
            }
            set {
                this.modeFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum installTypeType {
        
        /// <remarks/>
        cab,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum installTypeRun {
        
        /// <remarks/>
        now,
        
        /// <remarks/>
        nextreboot,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum installTypeWaitforprocess {
        
        /// <remarks/>
        yes,
        
        /// <remarks/>
        no,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum installTypeMode {
        
        /// <remarks/>
        normal,
        
        /// <remarks/>
        silent,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class configSection {
        
        private object[] itemsField;
        
        private string nameField;
        
        private int idletimeoutField;
        
        private bool idletimeoutFieldSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("exec", typeof(execType))]
        [System.Xml.Serialization.XmlElementAttribute("exit", typeof(exitType))]
        [System.Xml.Serialization.XmlElementAttribute("install", typeof(installType))]
        [System.Xml.Serialization.XmlElementAttribute("io", typeof(ioType))]
        [System.Xml.Serialization.XmlElementAttribute("kill", typeof(killType))]
        [System.Xml.Serialization.XmlElementAttribute("reboot", typeof(rebootType))]
        [System.Xml.Serialization.XmlElementAttribute("registry", typeof(registryType))]
        [System.Xml.Serialization.XmlElementAttribute("sql", typeof(sqlType))]
        [System.Xml.Serialization.XmlElementAttribute("wait", typeof(waitType))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int idletimeout {
            get {
                return this.idletimeoutField;
            }
            set {
                this.idletimeoutField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool idletimeoutSpecified {
            get {
                return this.idletimeoutFieldSpecified;
            }
            set {
                this.idletimeoutFieldSpecified = value;
            }
        }
    }
}

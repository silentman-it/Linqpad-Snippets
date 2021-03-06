<Query Kind="Program">
  <Output>DataGrids</Output>
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceModel.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceModel.Internals.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Serialization.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Configuration.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\SMDiagnostics.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.IdentityModel.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.DirectoryServices.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\Microsoft.Transactions.Bridge.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.Services.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.EnterpriseServices.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.IdentityModel.Selectors.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.ApplicationServices.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Messaging.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.DurableInstancing.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceProcess.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceModel.Activation.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Security.dll</Reference>
  <Namespace>System.ServiceModel</Namespace>
</Query>

void Main()
{
	string[] unitList = { "UP_BUSSENTO_1" };
	//string[] unitList = { "UP_MONCALRPW_2" };

	//string[] unitList = { "UP_NAPOLIL_4", "UP_TORREVALD_4", "UP_TRRVLDLIGA_5", "UP_TRRVLDLIGA_6", "UP_VADOTERM_5", "UP_VADO_TERM_3", "UP_VADO_TERM_4" };
	//string[] unitList = { "UP_CNTRALETEL_11", "UP_BRESSANON_1", "UP_S.PANCRAZ_1", "UP_S.VALBURG_1", "UP_LAPPAGO_1", "UP_M.DI_TURE_1", "UP_PONTE_GAR_1", "UP_SARENTINO_1", "UP_S.FLORI.A_1", "UP_SFLORIANO_2", "UP_CNTRLNTRNO_11", "UP_FONTANA_B_1", "UP_LANA_1", "UP_PRACOMUNE_1", "UP_CARDANO_1" };
	//string[] unitList = { "UP_BUSSENTO_1", "UP_MONCALIERI_3", "UP_MONCALRPW_2", "UP_PNTVENTOUX_3", "UP_ROSONE_1", "UP_TELESSIO_1", "UP_TORINONORD_1", "UP_TURBIGO_4", "UP_VILLA_1" };
	
	DateTime flowDate = DateTime.Parse("30/11/2017");
	//DateTime flowDate = DateTime.Today;
	
	string endpointAddress = "net.tcp://localhost:8734/CalculatePlans/"; // LOCAL
	//string endpointAddress = "net.tcp://win2008r2test1:8734/CalculatePlans/"; // TEST1
	//string endpointAddress = "net.tcp://srvegt01.master.local:8734/CalculatePlans/"; // IREN TEST
	//string endpointAddress = "net.tcp://srvegt02.master.local:8734/CalculatePlans/"; // IREN PROD
	
	NetTcpBinding myNetTcpBinding = new NetTcpBinding() {
		MaxReceivedMessageSize = 1024 * 64 * 100,
		ReceiveTimeout = TimeSpan.FromMinutes(5),
		SendTimeout = TimeSpan.FromMinutes(5),
		Security = new NetTcpSecurity { Mode = SecurityMode.None }
	};

	ChannelFactory<ICalculatePlansService> myNetTcpChannelFactory = new ChannelFactory<ICalculatePlansService>(myNetTcpBinding, new EndpointAddress(endpointAddress));

	ICalculatePlansService px = myNetTcpChannelFactory.CreateChannel();
	
	px.GetServiceInfo().Dump("Service Version");

	var o = 
		px.GetCalculatePlansDataExtended(unitList, flowDate, GetCalculatePlansDataOptions.Defaults)
			.Dump("GetCalculatePlansData() over NetTcp");
			
	px.GetCacheStatus()
		.GroupBy(x => x.Key2)
		.OrderBy(x => x.Key)
		.Dump("CacheStatus");
		
	
		
//	o.Select(x => new { x.UnitName, x.LatestMeasureTime, Metering = x.Metering.Where(m => m.Value.HasValue).Max(m => m.Key) }).Dump();
		
	((IClientChannel)px).Close();

}


// ///////////////////////////////////////////////
// Copied from Service Reference's code
// ///////////////////////////////////////////////

    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CalculatePlansDataItem", Namespace="http://schemas.datacontract.org/2004/07/CalculatePlans")]
    [System.SerializableAttribute()]
    public partial class CalculatePlansDataItem : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.TimeSpan DataGenerationDurationField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTimeOffset DataGenerationTimestampField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ErrorField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.Dictionary<string, CalculatePlansFasciaItem[]> FasceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTimeOffset LatestMeasureTimeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int MessageCountField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private CalculatePlansMessageItem[] MessagesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.Dictionary<System.DateTimeOffset, System.Nullable<decimal>> MeteringField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.Dictionary<System.DateTimeOffset, System.Nullable<decimal>> OBField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.Dictionary<System.DateTimeOffset, System.Nullable<decimal>> PVField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.Dictionary<System.DateTimeOffset, System.Nullable<decimal>> PVMCField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.Dictionary<System.DateTimeOffset, System.Nullable<decimal>> SbilField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.Dictionary<System.DateTimeOffset, System.Nullable<decimal>> SecondariaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.Dictionary<System.DateTimeOffset, System.Nullable<decimal>> SemibandaTernaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool SuccessfulField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<System.DateTimeOffset, System.Nullable<decimal>>> USField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UnitNameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.TimeSpan DataGenerationDuration {
            get {
                return this.DataGenerationDurationField;
            }
            set {
                if ((this.DataGenerationDurationField.Equals(value) != true)) {
                    this.DataGenerationDurationField = value;
                    this.RaisePropertyChanged("DataGenerationDuration");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTimeOffset DataGenerationTimestamp {
            get {
                return this.DataGenerationTimestampField;
            }
            set {
                if ((this.DataGenerationTimestampField.Equals(value) != true)) {
                    this.DataGenerationTimestampField = value;
                    this.RaisePropertyChanged("DataGenerationTimestamp");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Error {
            get {
                return this.ErrorField;
            }
            set {
                if ((object.ReferenceEquals(this.ErrorField, value) != true)) {
                    this.ErrorField = value;
                    this.RaisePropertyChanged("Error");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.Dictionary<string, CalculatePlansFasciaItem[]> Fasce {
            get {
                return this.FasceField;
            }
            set {
                if ((object.ReferenceEquals(this.FasceField, value) != true)) {
                    this.FasceField = value;
                    this.RaisePropertyChanged("Fasce");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTimeOffset LatestMeasureTime {
            get {
                return this.LatestMeasureTimeField;
            }
            set {
                if ((this.LatestMeasureTimeField.Equals(value) != true)) {
                    this.LatestMeasureTimeField = value;
                    this.RaisePropertyChanged("LatestMeasureTime");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int MessageCount {
            get {
                return this.MessageCountField;
            }
            set {
                if ((this.MessageCountField.Equals(value) != true)) {
                    this.MessageCountField = value;
                    this.RaisePropertyChanged("MessageCount");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public CalculatePlansMessageItem[] Messages {
            get {
                return this.MessagesField;
            }
            set {
                if ((object.ReferenceEquals(this.MessagesField, value) != true)) {
                    this.MessagesField = value;
                    this.RaisePropertyChanged("Messages");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.Dictionary<System.DateTimeOffset, System.Nullable<decimal>> Metering {
            get {
                return this.MeteringField;
            }
            set {
                if ((object.ReferenceEquals(this.MeteringField, value) != true)) {
                    this.MeteringField = value;
                    this.RaisePropertyChanged("Metering");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.Dictionary<System.DateTimeOffset, System.Nullable<decimal>> OB {
            get {
                return this.OBField;
            }
            set {
                if ((object.ReferenceEquals(this.OBField, value) != true)) {
                    this.OBField = value;
                    this.RaisePropertyChanged("OB");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.Dictionary<System.DateTimeOffset, System.Nullable<decimal>> PV {
            get {
                return this.PVField;
            }
            set {
                if ((object.ReferenceEquals(this.PVField, value) != true)) {
                    this.PVField = value;
                    this.RaisePropertyChanged("PV");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.Dictionary<System.DateTimeOffset, System.Nullable<decimal>> PVMC {
            get {
                return this.PVMCField;
            }
            set {
                if ((object.ReferenceEquals(this.PVMCField, value) != true)) {
                    this.PVMCField = value;
                    this.RaisePropertyChanged("PVMC");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.Dictionary<System.DateTimeOffset, System.Nullable<decimal>> Sbil {
            get {
                return this.SbilField;
            }
            set {
                if ((object.ReferenceEquals(this.SbilField, value) != true)) {
                    this.SbilField = value;
                    this.RaisePropertyChanged("Sbil");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.Dictionary<System.DateTimeOffset, System.Nullable<decimal>> Secondaria {
            get {
                return this.SecondariaField;
            }
            set {
                if ((object.ReferenceEquals(this.SecondariaField, value) != true)) {
                    this.SecondariaField = value;
                    this.RaisePropertyChanged("Secondaria");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.Dictionary<System.DateTimeOffset, System.Nullable<decimal>> SemibandaTerna {
            get {
                return this.SemibandaTernaField;
            }
            set {
                if ((object.ReferenceEquals(this.SemibandaTernaField, value) != true)) {
                    this.SemibandaTernaField = value;
                    this.RaisePropertyChanged("SemibandaTerna");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Successful {
            get {
                return this.SuccessfulField;
            }
            set {
                if ((this.SuccessfulField.Equals(value) != true)) {
                    this.SuccessfulField = value;
                    this.RaisePropertyChanged("Successful");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<System.DateTimeOffset, System.Nullable<decimal>>> US {
            get {
                return this.USField;
            }
            set {
                if ((object.ReferenceEquals(this.USField, value) != true)) {
                    this.USField = value;
                    this.RaisePropertyChanged("US");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UnitName {
            get {
                return this.UnitNameField;
            }
            set {
                if ((object.ReferenceEquals(this.UnitNameField, value) != true)) {
                    this.UnitNameField = value;
                    this.RaisePropertyChanged("UnitName");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CalculatePlansFasciaItem", Namespace="http://schemas.datacontract.org/2004/07/CalculatePlans")]
    [System.SerializableAttribute()]
    public partial class CalculatePlansFasciaItem : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private decimal PMaxField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private decimal PMinField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private decimal SBField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTimeOffset TSField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Id {
            get {
                return this.IdField;
            }
            set {
                if ((object.ReferenceEquals(this.IdField, value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal PMax {
            get {
                return this.PMaxField;
            }
            set {
                if ((this.PMaxField.Equals(value) != true)) {
                    this.PMaxField = value;
                    this.RaisePropertyChanged("PMax");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal PMin {
            get {
                return this.PMinField;
            }
            set {
                if ((this.PMinField.Equals(value) != true)) {
                    this.PMinField = value;
                    this.RaisePropertyChanged("PMin");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal SB {
            get {
                return this.SBField;
            }
            set {
                if ((this.SBField.Equals(value) != true)) {
                    this.SBField = value;
                    this.RaisePropertyChanged("SB");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTimeOffset TS {
            get {
                return this.TSField;
            }
            set {
                if ((this.TSField.Equals(value) != true)) {
                    this.TSField = value;
                    this.RaisePropertyChanged("TS");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CalculatePlansMessageItem", Namespace="http://schemas.datacontract.org/2004/07/CalculatePlans")]
    [System.SerializableAttribute()]
    public partial class CalculatePlansMessageItem : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTimeOffset EndField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FileNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageTypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string RawField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTimeOffset StartField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UnitField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTimeOffset End {
            get {
                return this.EndField;
            }
            set {
                if ((this.EndField.Equals(value) != true)) {
                    this.EndField = value;
                    this.RaisePropertyChanged("End");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FileName {
            get {
                return this.FileNameField;
            }
            set {
                if ((object.ReferenceEquals(this.FileNameField, value) != true)) {
                    this.FileNameField = value;
                    this.RaisePropertyChanged("FileName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Id {
            get {
                return this.IdField;
            }
            set {
                if ((object.ReferenceEquals(this.IdField, value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string MessageType {
            get {
                return this.MessageTypeField;
            }
            set {
                if ((object.ReferenceEquals(this.MessageTypeField, value) != true)) {
                    this.MessageTypeField = value;
                    this.RaisePropertyChanged("MessageType");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Raw {
            get {
                return this.RawField;
            }
            set {
                if ((object.ReferenceEquals(this.RawField, value) != true)) {
                    this.RawField = value;
                    this.RaisePropertyChanged("Raw");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTimeOffset Start {
            get {
                return this.StartField;
            }
            set {
                if ((this.StartField.Equals(value) != true)) {
                    this.StartField = value;
                    this.RaisePropertyChanged("Start");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Unit {
            get {
                return this.UnitField;
            }
            set {
                if ((object.ReferenceEquals(this.UnitField, value) != true)) {
                    this.UnitField = value;
                    this.RaisePropertyChanged("Unit");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CalculatePlansServiceFault", Namespace="http://schemas.datacontract.org/2004/07/CalculatePlans")]
    [System.SerializableAttribute()]
    public partial class CalculatePlansServiceFault : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private CalculatePlansUnitFault[] ExceptionsField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public CalculatePlansUnitFault[] Exceptions {
            get {
                return this.ExceptionsField;
            }
            set {
                if ((object.ReferenceEquals(this.ExceptionsField, value) != true)) {
                    this.ExceptionsField = value;
                    this.RaisePropertyChanged("Exceptions");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CalculatePlansUnitFault", Namespace="http://schemas.datacontract.org/2004/07/CalculatePlans")]
    [System.SerializableAttribute()]
    public partial class CalculatePlansUnitFault : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ErrCodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ErrMessageField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ErrCode {
            get {
                return this.ErrCodeField;
            }
            set {
                if ((this.ErrCodeField.Equals(value) != true)) {
                    this.ErrCodeField = value;
                    this.RaisePropertyChanged("ErrCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ErrMessage {
            get {
                return this.ErrMessageField;
            }
            set {
                if ((object.ReferenceEquals(this.ErrMessageField, value) != true)) {
                    this.ErrMessageField = value;
                    this.RaisePropertyChanged("ErrMessage");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.FlagsAttribute()]
    [System.Runtime.Serialization.DataContractAttribute(Name="GetCalculatePlansDataOptions", Namespace="http://schemas.datacontract.org/2004/07/CalculatePlans")]
    public enum GetCalculatePlansDataOptions : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        None = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        ResampleToFifteenMinutes = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        OnlyLatestUS = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Defaults = 3,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CacheStatusItem", Namespace="http://schemas.datacontract.org/2004/07/CalculatePlans")]
    [System.SerializableAttribute()]
    public partial class CacheStatusItem : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string Key1Field;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string Key2Field;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string RegionField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Key1 {
            get {
                return this.Key1Field;
            }
            set {
                if ((object.ReferenceEquals(this.Key1Field, value) != true)) {
                    this.Key1Field = value;
                    this.RaisePropertyChanged("Key1");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Key2 {
            get {
                return this.Key2Field;
            }
            set {
                if ((object.ReferenceEquals(this.Key2Field, value) != true)) {
                    this.Key2Field = value;
                    this.RaisePropertyChanged("Key2");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Region {
            get {
                return this.RegionField;
            }
            set {
                if ((object.ReferenceEquals(this.RegionField, value) != true)) {
                    this.RegionField = value;
                    this.RaisePropertyChanged("Region");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="InfoConsolle", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Types")]
    [System.SerializableAttribute()]
    public partial class InfoConsolle : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private decimal BDEField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ErrorField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<DateTimeUtilityIntervalType, System.Nullable<decimal>>> FasciaPmaxField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<DateTimeUtilityIntervalType, System.Nullable<decimal>>> FasciaPminField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.Dictionary<DateTimeUtilityIntervalType, System.Nullable<decimal>> MeteringField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.Dictionary<DateTimeUtilityIntervalType, System.Nullable<decimal>> OBField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.Dictionary<DateTimeUtilityIntervalType, System.Nullable<decimal>> PVField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.Dictionary<DateTimeUtilityIntervalType, System.Nullable<decimal>> PVMCField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.Dictionary<DateTimeUtilityIntervalType, System.Nullable<decimal>> SbilField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.Dictionary<DateTimeUtilityIntervalType, System.Nullable<decimal>> SecondariaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool SuccessfulField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime TimeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UnitNameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal BDE {
            get {
                return this.BDEField;
            }
            set {
                if ((this.BDEField.Equals(value) != true)) {
                    this.BDEField = value;
                    this.RaisePropertyChanged("BDE");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Error {
            get {
                return this.ErrorField;
            }
            set {
                if ((object.ReferenceEquals(this.ErrorField, value) != true)) {
                    this.ErrorField = value;
                    this.RaisePropertyChanged("Error");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<DateTimeUtilityIntervalType, System.Nullable<decimal>>> FasciaPmax {
            get {
                return this.FasciaPmaxField;
            }
            set {
                if ((object.ReferenceEquals(this.FasciaPmaxField, value) != true)) {
                    this.FasciaPmaxField = value;
                    this.RaisePropertyChanged("FasciaPmax");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<DateTimeUtilityIntervalType, System.Nullable<decimal>>> FasciaPmin {
            get {
                return this.FasciaPminField;
            }
            set {
                if ((object.ReferenceEquals(this.FasciaPminField, value) != true)) {
                    this.FasciaPminField = value;
                    this.RaisePropertyChanged("FasciaPmin");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.Dictionary<DateTimeUtilityIntervalType, System.Nullable<decimal>> Metering {
            get {
                return this.MeteringField;
            }
            set {
                if ((object.ReferenceEquals(this.MeteringField, value) != true)) {
                    this.MeteringField = value;
                    this.RaisePropertyChanged("Metering");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.Dictionary<DateTimeUtilityIntervalType, System.Nullable<decimal>> OB {
            get {
                return this.OBField;
            }
            set {
                if ((object.ReferenceEquals(this.OBField, value) != true)) {
                    this.OBField = value;
                    this.RaisePropertyChanged("OB");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.Dictionary<DateTimeUtilityIntervalType, System.Nullable<decimal>> PV {
            get {
                return this.PVField;
            }
            set {
                if ((object.ReferenceEquals(this.PVField, value) != true)) {
                    this.PVField = value;
                    this.RaisePropertyChanged("PV");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.Dictionary<DateTimeUtilityIntervalType, System.Nullable<decimal>> PVMC {
            get {
                return this.PVMCField;
            }
            set {
                if ((object.ReferenceEquals(this.PVMCField, value) != true)) {
                    this.PVMCField = value;
                    this.RaisePropertyChanged("PVMC");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.Dictionary<DateTimeUtilityIntervalType, System.Nullable<decimal>> Sbil {
            get {
                return this.SbilField;
            }
            set {
                if ((object.ReferenceEquals(this.SbilField, value) != true)) {
                    this.SbilField = value;
                    this.RaisePropertyChanged("Sbil");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.Dictionary<DateTimeUtilityIntervalType, System.Nullable<decimal>> Secondaria {
            get {
                return this.SecondariaField;
            }
            set {
                if ((object.ReferenceEquals(this.SecondariaField, value) != true)) {
                    this.SecondariaField = value;
                    this.RaisePropertyChanged("Secondaria");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Successful {
            get {
                return this.SuccessfulField;
            }
            set {
                if ((this.SuccessfulField.Equals(value) != true)) {
                    this.SuccessfulField = value;
                    this.RaisePropertyChanged("Successful");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime Time {
            get {
                return this.TimeField;
            }
            set {
                if ((this.TimeField.Equals(value) != true)) {
                    this.TimeField = value;
                    this.RaisePropertyChanged("Time");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UnitName {
            get {
                return this.UnitNameField;
            }
            set {
                if ((object.ReferenceEquals(this.UnitNameField, value) != true)) {
                    this.UnitNameField = value;
                    this.RaisePropertyChanged("UnitName");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DateTimeUtility.IntervalType", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Utils")]
    [System.SerializableAttribute()]
    public partial class DateTimeUtilityIntervalType : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime ValidityEndDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ValidityEndDateOffsetField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime ValidityStartDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ValidityStartDateOffsetField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime ValidityEndDate {
            get {
                return this.ValidityEndDateField;
            }
            set {
                if ((this.ValidityEndDateField.Equals(value) != true)) {
                    this.ValidityEndDateField = value;
                    this.RaisePropertyChanged("ValidityEndDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ValidityEndDateOffset {
            get {
                return this.ValidityEndDateOffsetField;
            }
            set {
                if ((this.ValidityEndDateOffsetField.Equals(value) != true)) {
                    this.ValidityEndDateOffsetField = value;
                    this.RaisePropertyChanged("ValidityEndDateOffset");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime ValidityStartDate {
            get {
                return this.ValidityStartDateField;
            }
            set {
                if ((this.ValidityStartDateField.Equals(value) != true)) {
                    this.ValidityStartDateField = value;
                    this.RaisePropertyChanged("ValidityStartDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ValidityStartDateOffset {
            get {
                return this.ValidityStartDateOffsetField;
            }
            set {
                if ((this.ValidityStartDateOffsetField.Equals(value) != true)) {
                    this.ValidityStartDateOffsetField = value;
                    this.RaisePropertyChanged("ValidityStartDateOffset");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="CPServiceReferenceNetTcp.ICalculatePlansService")]
    public interface ICalculatePlansService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICalculatePlansService/GetCalculatePlansData", ReplyAction="http://tempuri.org/ICalculatePlansService/GetCalculatePlansDataResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(CalculatePlansServiceFault), Action="http://tempuri.org/ICalculatePlansService/GetCalculatePlansDataCalculatePlansServ" +
            "iceFaultFault", Name="CalculatePlansServiceFault", Namespace="http://schemas.datacontract.org/2004/07/CalculatePlans")]
        CalculatePlansDataItem[] GetCalculatePlansData(string[] lsUnits, System.DateTime flowDate);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICalculatePlansService/GetCalculatePlansData", ReplyAction="http://tempuri.org/ICalculatePlansService/GetCalculatePlansDataResponse")]
        System.Threading.Tasks.Task<CalculatePlansDataItem[]> GetCalculatePlansDataAsync(string[] lsUnits, System.DateTime flowDate);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICalculatePlansService/GetCalculatePlansDataExtended", ReplyAction="http://tempuri.org/ICalculatePlansService/GetCalculatePlansDataExtendedResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(CalculatePlansServiceFault), Action="http://tempuri.org/ICalculatePlansService/GetCalculatePlansDataExtendedCalculateP" +
            "lansServiceFaultFault", Name="CalculatePlansServiceFault", Namespace="http://schemas.datacontract.org/2004/07/CalculatePlans")]
        CalculatePlansDataItem[] GetCalculatePlansDataExtended(string[] lsUnits, System.DateTime flowDate, GetCalculatePlansDataOptions options);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICalculatePlansService/GetCalculatePlansDataExtended", ReplyAction="http://tempuri.org/ICalculatePlansService/GetCalculatePlansDataExtendedResponse")]
        System.Threading.Tasks.Task<CalculatePlansDataItem[]> GetCalculatePlansDataExtendedAsync(string[] lsUnits, System.DateTime flowDate, GetCalculatePlansDataOptions options);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICalculatePlansService/GetCacheStatus", ReplyAction="http://tempuri.org/ICalculatePlansService/GetCacheStatusResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(CalculatePlansServiceFault), Action="http://tempuri.org/ICalculatePlansService/GetCacheStatusCalculatePlansServiceFaul" +
            "tFault", Name="CalculatePlansServiceFault", Namespace="http://schemas.datacontract.org/2004/07/CalculatePlans")]
        CacheStatusItem[] GetCacheStatus();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICalculatePlansService/GetCacheStatus", ReplyAction="http://tempuri.org/ICalculatePlansService/GetCacheStatusResponse")]
        System.Threading.Tasks.Task<CacheStatusItem[]> GetCacheStatusAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICalculatePlansService/GetServiceInfo", ReplyAction="http://tempuri.org/ICalculatePlansService/GetServiceInfoResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(CalculatePlansServiceFault), Action="http://tempuri.org/ICalculatePlansService/GetServiceInfoCalculatePlansServiceFaul" +
            "tFault", Name="CalculatePlansServiceFault", Namespace="http://schemas.datacontract.org/2004/07/CalculatePlans")]
        string GetServiceInfo();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICalculatePlansService/GetServiceInfo", ReplyAction="http://tempuri.org/ICalculatePlansService/GetServiceInfoResponse")]
        System.Threading.Tasks.Task<string> GetServiceInfoAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICalculatePlansService/GetPlanCalculationDump", ReplyAction="http://tempuri.org/ICalculatePlansService/GetPlanCalculationDumpResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(CalculatePlansServiceFault), Action="http://tempuri.org/ICalculatePlansService/GetPlanCalculationDumpCalculatePlansSer" +
            "viceFaultFault", Name="CalculatePlansServiceFault", Namespace="http://schemas.datacontract.org/2004/07/CalculatePlans")]
        byte[] GetPlanCalculationDump(string unitName, System.DateTime flowDate, string planType);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICalculatePlansService/GetPlanCalculationDump", ReplyAction="http://tempuri.org/ICalculatePlansService/GetPlanCalculationDumpResponse")]
        System.Threading.Tasks.Task<byte[]> GetPlanCalculationDumpAsync(string unitName, System.DateTime flowDate, string planType);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICalculatePlansService/GetInfoConsolles", ReplyAction="http://tempuri.org/ICalculatePlansService/GetInfoConsollesResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(CalculatePlansServiceFault), Action="http://tempuri.org/ICalculatePlansService/GetInfoConsollesCalculatePlansServiceFa" +
            "ultFault", Name="CalculatePlansServiceFault", Namespace="http://schemas.datacontract.org/2004/07/CalculatePlans")]
        InfoConsolle[] GetInfoConsolles(string[] lsUnits, System.DateTime flowDate);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICalculatePlansService/GetInfoConsolles", ReplyAction="http://tempuri.org/ICalculatePlansService/GetInfoConsollesResponse")]
        System.Threading.Tasks.Task<InfoConsolle[]> GetInfoConsollesAsync(string[] lsUnits, System.DateTime flowDate);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICalculatePlansServiceChannel : ICalculatePlansService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CalculatePlansServiceClient : System.ServiceModel.ClientBase<ICalculatePlansService>, ICalculatePlansService {
        
        public CalculatePlansServiceClient() {
        }
        
        public CalculatePlansServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CalculatePlansServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CalculatePlansServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CalculatePlansServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public CalculatePlansDataItem[] GetCalculatePlansData(string[] lsUnits, System.DateTime flowDate) {
            return base.Channel.GetCalculatePlansData(lsUnits, flowDate);
        }
        
        public System.Threading.Tasks.Task<CalculatePlansDataItem[]> GetCalculatePlansDataAsync(string[] lsUnits, System.DateTime flowDate) {
            return base.Channel.GetCalculatePlansDataAsync(lsUnits, flowDate);
        }
        
        public CalculatePlansDataItem[] GetCalculatePlansDataExtended(string[] lsUnits, System.DateTime flowDate, GetCalculatePlansDataOptions options) {
            return base.Channel.GetCalculatePlansDataExtended(lsUnits, flowDate, options);
        }
        
        public System.Threading.Tasks.Task<CalculatePlansDataItem[]> GetCalculatePlansDataExtendedAsync(string[] lsUnits, System.DateTime flowDate, GetCalculatePlansDataOptions options) {
            return base.Channel.GetCalculatePlansDataExtendedAsync(lsUnits, flowDate, options);
        }
        
        public CacheStatusItem[] GetCacheStatus() {
            return base.Channel.GetCacheStatus();
        }
        
        public System.Threading.Tasks.Task<CacheStatusItem[]> GetCacheStatusAsync() {
            return base.Channel.GetCacheStatusAsync();
        }
        
        public string GetServiceInfo() {
            return base.Channel.GetServiceInfo();
        }
        
        public System.Threading.Tasks.Task<string> GetServiceInfoAsync() {
            return base.Channel.GetServiceInfoAsync();
        }
        
        public byte[] GetPlanCalculationDump(string unitName, System.DateTime flowDate, string planType) {
            return base.Channel.GetPlanCalculationDump(unitName, flowDate, planType);
        }
        
        public System.Threading.Tasks.Task<byte[]> GetPlanCalculationDumpAsync(string unitName, System.DateTime flowDate, string planType) {
            return base.Channel.GetPlanCalculationDumpAsync(unitName, flowDate, planType);
        }
        
        public InfoConsolle[] GetInfoConsolles(string[] lsUnits, System.DateTime flowDate) {
            return base.Channel.GetInfoConsolles(lsUnits, flowDate);
        }
        
        public System.Threading.Tasks.Task<InfoConsolle[]> GetInfoConsollesAsync(string[] lsUnits, System.DateTime flowDate) {
            return base.Channel.GetInfoConsollesAsync(lsUnits, flowDate);
        }
    }
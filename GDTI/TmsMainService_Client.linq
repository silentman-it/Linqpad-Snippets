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
	//string endpointAddress = @"http://localhost:11229/TmsMainService.svc"; //LOCAL
	string endpointAddress = @"http://srvegt01.master.local/TmsMainService.svc"; //IREN_TEST
	string dnsIdentity = "srvegt01";

	WSHttpBinding myBinding = new WSHttpBinding() {
		MaxReceivedMessageSize = 1024 * 1024 * 5,
		ReceiveTimeout = TimeSpan.FromMinutes(2),
		Security = new WSHttpSecurity { Mode = SecurityMode.Message, Message = new NonDualMessageSecurityOverHttp { ClientCredentialType = MessageCredentialType.UserName } }
	};

	ChannelFactory<ITmsMainService> channelFactory = new ChannelFactory<ITmsMainService>(myBinding, new EndpointAddress(new Uri(endpointAddress), new DnsEndpointIdentity(dnsIdentity)));
	
	channelFactory.Credentials.UserName.UserName = "System";
	channelFactory.Credentials.UserName.Password = "pippo";
	channelFactory.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.None;
	channelFactory.Credentials.ServiceCertificate.Authentication.RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck;
	
	
	ITmsMainService px = channelFactory.CreateChannel();

	//px.GetDBServerDate().Dump("GetDBServerDate()");
	
	//string[] unitList = { "UP_NAPOLIL_4", "UP_TORREVALD_4", "UP_TRRVLDLIGA_5", "UP_TRRVLDLIGA_6", "UP_VADOTERM_5", "UP_VADO_TERM_3", "UP_VADO_TERM_4" };
	string[] unitList = { "UP_VADOTERM_5" };
	
	px.GetCalculatePlansDataExtended(unitList, DateTime.Parse("08/06/2017"), GetCalculatePlansDataOptions.Defaults).Dump("GetCalculatePlansData()");
		
	((IClientChannel)px).Close();
	
	///////////////////////////////////////
	

	
}


    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="BaseEntity", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Entities.Series")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ContextEntity))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(SymbolEntity))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(FormulaEntity))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(SerieEntity))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ChannelEntity))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ClassEntity))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(FrequencyEntity))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(SourceEntity))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(SerieDetailGroupEntity))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(SerieDetailEntity))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(StreamEntity))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ActivityEntity))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(MarketEntity))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(UnitEntity))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ActivitySerieGroupEntity))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ActivityGroupDetailEntity))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(FlowEntity))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(UserEntity))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(SignalEntity))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(MessageEntity))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(MessageOutEntity))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(LogEntity))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(FmkActivityEntity))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ActivityAssociationEntity))]
    public partial class BaseEntity : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private EntityDBStatus DBStatusField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool PopulatedField;
        
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
        public EntityDBStatus DBStatus {
            get {
                return this.DBStatusField;
            }
            set {
                if ((this.DBStatusField.Equals(value) != true)) {
                    this.DBStatusField = value;
                    this.RaisePropertyChanged("DBStatus");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Populated {
            get {
                return this.PopulatedField;
            }
            set {
                if ((this.PopulatedField.Equals(value) != true)) {
                    this.PopulatedField = value;
                    this.RaisePropertyChanged("Populated");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="ContextEntity", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Entities.Series")]
    [System.SerializableAttribute()]
    public partial class ContextEntity : BaseEntity {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SymbolEntity", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Entities.Series")]
    [System.SerializableAttribute()]
    public partial class SymbolEntity : BaseEntity {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ParentNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private SymbolEntity ParentSymbolField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ParentName {
            get {
                return this.ParentNameField;
            }
            set {
                if ((object.ReferenceEquals(this.ParentNameField, value) != true)) {
                    this.ParentNameField = value;
                    this.RaisePropertyChanged("ParentName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public SymbolEntity ParentSymbol {
            get {
                return this.ParentSymbolField;
            }
            set {
                if ((object.ReferenceEquals(this.ParentSymbolField, value) != true)) {
                    this.ParentSymbolField = value;
                    this.RaisePropertyChanged("ParentSymbol");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FormulaEntity", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Entities.Series")]
    [System.SerializableAttribute()]
    public partial class FormulaEntity : BaseEntity {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private byte[] FormulaBinaryField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FormulaTextField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime InsertDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> InsertUserIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime UpdateDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> UpdateUserIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string XmlDataField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte[] FormulaBinary {
            get {
                return this.FormulaBinaryField;
            }
            set {
                if ((object.ReferenceEquals(this.FormulaBinaryField, value) != true)) {
                    this.FormulaBinaryField = value;
                    this.RaisePropertyChanged("FormulaBinary");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FormulaText {
            get {
                return this.FormulaTextField;
            }
            set {
                if ((object.ReferenceEquals(this.FormulaTextField, value) != true)) {
                    this.FormulaTextField = value;
                    this.RaisePropertyChanged("FormulaText");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime InsertDate {
            get {
                return this.InsertDateField;
            }
            set {
                if ((this.InsertDateField.Equals(value) != true)) {
                    this.InsertDateField = value;
                    this.RaisePropertyChanged("InsertDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> InsertUserId {
            get {
                return this.InsertUserIdField;
            }
            set {
                if ((this.InsertUserIdField.Equals(value) != true)) {
                    this.InsertUserIdField = value;
                    this.RaisePropertyChanged("InsertUserId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Type {
            get {
                return this.TypeField;
            }
            set {
                if ((object.ReferenceEquals(this.TypeField, value) != true)) {
                    this.TypeField = value;
                    this.RaisePropertyChanged("Type");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime UpdateDate {
            get {
                return this.UpdateDateField;
            }
            set {
                if ((this.UpdateDateField.Equals(value) != true)) {
                    this.UpdateDateField = value;
                    this.RaisePropertyChanged("UpdateDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> UpdateUserId {
            get {
                return this.UpdateUserIdField;
            }
            set {
                if ((this.UpdateUserIdField.Equals(value) != true)) {
                    this.UpdateUserIdField = value;
                    this.RaisePropertyChanged("UpdateUserId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string XmlData {
            get {
                return this.XmlDataField;
            }
            set {
                if ((object.ReferenceEquals(this.XmlDataField, value) != true)) {
                    this.XmlDataField = value;
                    this.RaisePropertyChanged("XmlData");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SerieEntity", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Entities.Series")]
    [System.SerializableAttribute()]
    public partial class SerieEntity : BaseEntity {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private ChannelEntity ChannelField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private ClassEntity ClassField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private ContextEntity ContextField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private FrequencyEntity FrequencyField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<bool> HistoricizedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool IsVirtualField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<decimal> PrimaryKeyField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<decimal> SecondaryKeyField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private SourceEntity SourceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private SymbolEntity SymbolField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UomExpressionField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ChannelEntity Channel {
            get {
                return this.ChannelField;
            }
            set {
                if ((object.ReferenceEquals(this.ChannelField, value) != true)) {
                    this.ChannelField = value;
                    this.RaisePropertyChanged("Channel");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ClassEntity Class {
            get {
                return this.ClassField;
            }
            set {
                if ((object.ReferenceEquals(this.ClassField, value) != true)) {
                    this.ClassField = value;
                    this.RaisePropertyChanged("Class");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ContextEntity Context {
            get {
                return this.ContextField;
            }
            set {
                if ((object.ReferenceEquals(this.ContextField, value) != true)) {
                    this.ContextField = value;
                    this.RaisePropertyChanged("Context");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public FrequencyEntity Frequency {
            get {
                return this.FrequencyField;
            }
            set {
                if ((object.ReferenceEquals(this.FrequencyField, value) != true)) {
                    this.FrequencyField = value;
                    this.RaisePropertyChanged("Frequency");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<bool> Historicized {
            get {
                return this.HistoricizedField;
            }
            set {
                if ((this.HistoricizedField.Equals(value) != true)) {
                    this.HistoricizedField = value;
                    this.RaisePropertyChanged("Historicized");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsVirtual {
            get {
                return this.IsVirtualField;
            }
            set {
                if ((this.IsVirtualField.Equals(value) != true)) {
                    this.IsVirtualField = value;
                    this.RaisePropertyChanged("IsVirtual");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<decimal> PrimaryKey {
            get {
                return this.PrimaryKeyField;
            }
            set {
                if ((this.PrimaryKeyField.Equals(value) != true)) {
                    this.PrimaryKeyField = value;
                    this.RaisePropertyChanged("PrimaryKey");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<decimal> SecondaryKey {
            get {
                return this.SecondaryKeyField;
            }
            set {
                if ((this.SecondaryKeyField.Equals(value) != true)) {
                    this.SecondaryKeyField = value;
                    this.RaisePropertyChanged("SecondaryKey");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public SourceEntity Source {
            get {
                return this.SourceField;
            }
            set {
                if ((object.ReferenceEquals(this.SourceField, value) != true)) {
                    this.SourceField = value;
                    this.RaisePropertyChanged("Source");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public SymbolEntity Symbol {
            get {
                return this.SymbolField;
            }
            set {
                if ((object.ReferenceEquals(this.SymbolField, value) != true)) {
                    this.SymbolField = value;
                    this.RaisePropertyChanged("Symbol");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UomExpression {
            get {
                return this.UomExpressionField;
            }
            set {
                if ((object.ReferenceEquals(this.UomExpressionField, value) != true)) {
                    this.UomExpressionField = value;
                    this.RaisePropertyChanged("UomExpression");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ChannelEntity", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Entities.Series")]
    [System.SerializableAttribute()]
    public partial class ChannelEntity : BaseEntity {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ClassEntity", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Entities.Series")]
    [System.SerializableAttribute()]
    public partial class ClassEntity : BaseEntity {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FrequencyEntity", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Entities.Series")]
    [System.SerializableAttribute()]
    public partial class FrequencyEntity : BaseEntity {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SourceEntity", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Entities.Series")]
    [System.SerializableAttribute()]
    public partial class SourceEntity : BaseEntity {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SerieDetailGroupEntity", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Entities.Series")]
    [System.SerializableAttribute()]
    public partial class SerieDetailGroupEntity : BaseEntity {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private ColumnType ColumnsTypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool EditableField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int PrecisionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private SerieEntity SerieField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private SerieDetailEntity[] SerieDetailsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int StreamIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> TransactionTsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> ValidityDateField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ColumnType ColumnsType {
            get {
                return this.ColumnsTypeField;
            }
            set {
                if ((this.ColumnsTypeField.Equals(value) != true)) {
                    this.ColumnsTypeField = value;
                    this.RaisePropertyChanged("ColumnsType");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Editable {
            get {
                return this.EditableField;
            }
            set {
                if ((this.EditableField.Equals(value) != true)) {
                    this.EditableField = value;
                    this.RaisePropertyChanged("Editable");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Precision {
            get {
                return this.PrecisionField;
            }
            set {
                if ((this.PrecisionField.Equals(value) != true)) {
                    this.PrecisionField = value;
                    this.RaisePropertyChanged("Precision");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public SerieEntity Serie {
            get {
                return this.SerieField;
            }
            set {
                if ((object.ReferenceEquals(this.SerieField, value) != true)) {
                    this.SerieField = value;
                    this.RaisePropertyChanged("Serie");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public SerieDetailEntity[] SerieDetails {
            get {
                return this.SerieDetailsField;
            }
            set {
                if ((object.ReferenceEquals(this.SerieDetailsField, value) != true)) {
                    this.SerieDetailsField = value;
                    this.RaisePropertyChanged("SerieDetails");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int StreamId {
            get {
                return this.StreamIdField;
            }
            set {
                if ((this.StreamIdField.Equals(value) != true)) {
                    this.StreamIdField = value;
                    this.RaisePropertyChanged("StreamId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> TransactionTs {
            get {
                return this.TransactionTsField;
            }
            set {
                if ((this.TransactionTsField.Equals(value) != true)) {
                    this.TransactionTsField = value;
                    this.RaisePropertyChanged("TransactionTs");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> ValidityDate {
            get {
                return this.ValidityDateField;
            }
            set {
                if ((this.ValidityDateField.Equals(value) != true)) {
                    this.ValidityDateField = value;
                    this.RaisePropertyChanged("ValidityDate");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SerieDetailEntity", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Entities.Series")]
    [System.SerializableAttribute()]
    public partial class SerieDetailEntity : BaseEntity {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string GenerationCdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LabelField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<decimal> PriorityField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PriorityCdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int SerieDetailGroupIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int SerieIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int StreamIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime TransactionEndTsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime TransactionStartTsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime ValidityEndDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ValidityEndDateOffsetField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime ValidityStartDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ValidityStartDateOffsetField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<decimal> ValueField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string GenerationCd {
            get {
                return this.GenerationCdField;
            }
            set {
                if ((object.ReferenceEquals(this.GenerationCdField, value) != true)) {
                    this.GenerationCdField = value;
                    this.RaisePropertyChanged("GenerationCd");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Label {
            get {
                return this.LabelField;
            }
            set {
                if ((object.ReferenceEquals(this.LabelField, value) != true)) {
                    this.LabelField = value;
                    this.RaisePropertyChanged("Label");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<decimal> Priority {
            get {
                return this.PriorityField;
            }
            set {
                if ((this.PriorityField.Equals(value) != true)) {
                    this.PriorityField = value;
                    this.RaisePropertyChanged("Priority");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PriorityCd {
            get {
                return this.PriorityCdField;
            }
            set {
                if ((object.ReferenceEquals(this.PriorityCdField, value) != true)) {
                    this.PriorityCdField = value;
                    this.RaisePropertyChanged("PriorityCd");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int SerieDetailGroupId {
            get {
                return this.SerieDetailGroupIdField;
            }
            set {
                if ((this.SerieDetailGroupIdField.Equals(value) != true)) {
                    this.SerieDetailGroupIdField = value;
                    this.RaisePropertyChanged("SerieDetailGroupId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int SerieId {
            get {
                return this.SerieIdField;
            }
            set {
                if ((this.SerieIdField.Equals(value) != true)) {
                    this.SerieIdField = value;
                    this.RaisePropertyChanged("SerieId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int StreamId {
            get {
                return this.StreamIdField;
            }
            set {
                if ((this.StreamIdField.Equals(value) != true)) {
                    this.StreamIdField = value;
                    this.RaisePropertyChanged("StreamId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime TransactionEndTs {
            get {
                return this.TransactionEndTsField;
            }
            set {
                if ((this.TransactionEndTsField.Equals(value) != true)) {
                    this.TransactionEndTsField = value;
                    this.RaisePropertyChanged("TransactionEndTs");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime TransactionStartTs {
            get {
                return this.TransactionStartTsField;
            }
            set {
                if ((this.TransactionStartTsField.Equals(value) != true)) {
                    this.TransactionStartTsField = value;
                    this.RaisePropertyChanged("TransactionStartTs");
                }
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
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<decimal> Value {
            get {
                return this.ValueField;
            }
            set {
                if ((this.ValueField.Equals(value) != true)) {
                    this.ValueField = value;
                    this.RaisePropertyChanged("Value");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="StreamEntity", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Entities.Series")]
    [System.SerializableAttribute()]
    public partial class StreamEntity : BaseEntity {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime CreationDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string GenerationCdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string InformationField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime InsertDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int InsertUserIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string OriginCdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StreamNameField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime CreationDate {
            get {
                return this.CreationDateField;
            }
            set {
                if ((this.CreationDateField.Equals(value) != true)) {
                    this.CreationDateField = value;
                    this.RaisePropertyChanged("CreationDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string GenerationCd {
            get {
                return this.GenerationCdField;
            }
            set {
                if ((object.ReferenceEquals(this.GenerationCdField, value) != true)) {
                    this.GenerationCdField = value;
                    this.RaisePropertyChanged("GenerationCd");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Information {
            get {
                return this.InformationField;
            }
            set {
                if ((object.ReferenceEquals(this.InformationField, value) != true)) {
                    this.InformationField = value;
                    this.RaisePropertyChanged("Information");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime InsertDate {
            get {
                return this.InsertDateField;
            }
            set {
                if ((this.InsertDateField.Equals(value) != true)) {
                    this.InsertDateField = value;
                    this.RaisePropertyChanged("InsertDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int InsertUserId {
            get {
                return this.InsertUserIdField;
            }
            set {
                if ((this.InsertUserIdField.Equals(value) != true)) {
                    this.InsertUserIdField = value;
                    this.RaisePropertyChanged("InsertUserId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string OriginCd {
            get {
                return this.OriginCdField;
            }
            set {
                if ((object.ReferenceEquals(this.OriginCdField, value) != true)) {
                    this.OriginCdField = value;
                    this.RaisePropertyChanged("OriginCd");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StreamName {
            get {
                return this.StreamNameField;
            }
            set {
                if ((object.ReferenceEquals(this.StreamNameField, value) != true)) {
                    this.StreamNameField = value;
                    this.RaisePropertyChanged("StreamName");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ActivityEntity", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Entities")]
    [System.SerializableAttribute()]
    public partial class ActivityEntity : BaseEntity {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ActivityCdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ActivityCd {
            get {
                return this.ActivityCdField;
            }
            set {
                if ((object.ReferenceEquals(this.ActivityCdField, value) != true)) {
                    this.ActivityCdField = value;
                    this.RaisePropertyChanged("ActivityCd");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MarketEntity", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Entities")]
    [System.SerializableAttribute()]
    public partial class MarketEntity : BaseEntity {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> ClosingDayOffsetField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime ClosingHourField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private ContextEntity ContextField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MarketCdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> OpeningDayOffsetField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime OpeningHourField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> ClosingDayOffset {
            get {
                return this.ClosingDayOffsetField;
            }
            set {
                if ((this.ClosingDayOffsetField.Equals(value) != true)) {
                    this.ClosingDayOffsetField = value;
                    this.RaisePropertyChanged("ClosingDayOffset");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime ClosingHour {
            get {
                return this.ClosingHourField;
            }
            set {
                if ((this.ClosingHourField.Equals(value) != true)) {
                    this.ClosingHourField = value;
                    this.RaisePropertyChanged("ClosingHour");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ContextEntity Context {
            get {
                return this.ContextField;
            }
            set {
                if ((object.ReferenceEquals(this.ContextField, value) != true)) {
                    this.ContextField = value;
                    this.RaisePropertyChanged("Context");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string MarketCd {
            get {
                return this.MarketCdField;
            }
            set {
                if ((object.ReferenceEquals(this.MarketCdField, value) != true)) {
                    this.MarketCdField = value;
                    this.RaisePropertyChanged("MarketCd");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> OpeningDayOffset {
            get {
                return this.OpeningDayOffsetField;
            }
            set {
                if ((this.OpeningDayOffsetField.Equals(value) != true)) {
                    this.OpeningDayOffsetField = value;
                    this.RaisePropertyChanged("OpeningDayOffset");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime OpeningHour {
            get {
                return this.OpeningHourField;
            }
            set {
                if ((this.OpeningHourField.Equals(value) != true)) {
                    this.OpeningHourField = value;
                    this.RaisePropertyChanged("OpeningHour");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="UnitEntity", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Entities")]
    [System.SerializableAttribute()]
    public partial class UnitEntity : BaseEntity {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ExternalCdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private SecurityObject SecurityField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private SymbolEntity SymbolField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TypeField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Code {
            get {
                return this.CodeField;
            }
            set {
                if ((object.ReferenceEquals(this.CodeField, value) != true)) {
                    this.CodeField = value;
                    this.RaisePropertyChanged("Code");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ExternalCd {
            get {
                return this.ExternalCdField;
            }
            set {
                if ((object.ReferenceEquals(this.ExternalCdField, value) != true)) {
                    this.ExternalCdField = value;
                    this.RaisePropertyChanged("ExternalCd");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public SecurityObject Security {
            get {
                return this.SecurityField;
            }
            set {
                if ((object.ReferenceEquals(this.SecurityField, value) != true)) {
                    this.SecurityField = value;
                    this.RaisePropertyChanged("Security");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public SymbolEntity Symbol {
            get {
                return this.SymbolField;
            }
            set {
                if ((object.ReferenceEquals(this.SymbolField, value) != true)) {
                    this.SymbolField = value;
                    this.RaisePropertyChanged("Symbol");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Type {
            get {
                return this.TypeField;
            }
            set {
                if ((object.ReferenceEquals(this.TypeField, value) != true)) {
                    this.TypeField = value;
                    this.RaisePropertyChanged("Type");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ActivitySerieGroupEntity", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Entities")]
    [System.SerializableAttribute()]
    public partial class ActivitySerieGroupEntity : BaseEntity {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private ActivityEntity ActivityField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> ActivitySerieGroupIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MarketEntity MarketField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> PositionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private UnitEntity UnitField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ActivityEntity Activity {
            get {
                return this.ActivityField;
            }
            set {
                if ((object.ReferenceEquals(this.ActivityField, value) != true)) {
                    this.ActivityField = value;
                    this.RaisePropertyChanged("Activity");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> ActivitySerieGroupId {
            get {
                return this.ActivitySerieGroupIdField;
            }
            set {
                if ((this.ActivitySerieGroupIdField.Equals(value) != true)) {
                    this.ActivitySerieGroupIdField = value;
                    this.RaisePropertyChanged("ActivitySerieGroupId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public MarketEntity Market {
            get {
                return this.MarketField;
            }
            set {
                if ((object.ReferenceEquals(this.MarketField, value) != true)) {
                    this.MarketField = value;
                    this.RaisePropertyChanged("Market");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> Position {
            get {
                return this.PositionField;
            }
            set {
                if ((this.PositionField.Equals(value) != true)) {
                    this.PositionField = value;
                    this.RaisePropertyChanged("Position");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public UnitEntity Unit {
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
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ActivityGroupDetailEntity", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Entities")]
    [System.SerializableAttribute()]
    public partial class ActivityGroupDetailEntity : BaseEntity {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private ActivitySerieGroupEntity ActivitySerieGroupField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<ColumnType> ColumnsTypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<bool> EditableField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private FormulaEntity FormulaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> PositionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private SerieEntity SerieField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ActivitySerieGroupEntity ActivitySerieGroup {
            get {
                return this.ActivitySerieGroupField;
            }
            set {
                if ((object.ReferenceEquals(this.ActivitySerieGroupField, value) != true)) {
                    this.ActivitySerieGroupField = value;
                    this.RaisePropertyChanged("ActivitySerieGroup");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<ColumnType> ColumnsType {
            get {
                return this.ColumnsTypeField;
            }
            set {
                if ((this.ColumnsTypeField.Equals(value) != true)) {
                    this.ColumnsTypeField = value;
                    this.RaisePropertyChanged("ColumnsType");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<bool> Editable {
            get {
                return this.EditableField;
            }
            set {
                if ((this.EditableField.Equals(value) != true)) {
                    this.EditableField = value;
                    this.RaisePropertyChanged("Editable");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public FormulaEntity Formula {
            get {
                return this.FormulaField;
            }
            set {
                if ((object.ReferenceEquals(this.FormulaField, value) != true)) {
                    this.FormulaField = value;
                    this.RaisePropertyChanged("Formula");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> Position {
            get {
                return this.PositionField;
            }
            set {
                if ((this.PositionField.Equals(value) != true)) {
                    this.PositionField = value;
                    this.RaisePropertyChanged("Position");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public SerieEntity Serie {
            get {
                return this.SerieField;
            }
            set {
                if ((object.ReferenceEquals(this.SerieField, value) != true)) {
                    this.SerieField = value;
                    this.RaisePropertyChanged("Serie");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FlowEntity", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Entities")]
    [System.SerializableAttribute()]
    public partial class FlowEntity : BaseEntity {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> PositionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private SerieDetailGroupEntity[] SerieDetailGroupsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private StreamEntity StreamField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> Position {
            get {
                return this.PositionField;
            }
            set {
                if ((this.PositionField.Equals(value) != true)) {
                    this.PositionField = value;
                    this.RaisePropertyChanged("Position");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public SerieDetailGroupEntity[] SerieDetailGroups {
            get {
                return this.SerieDetailGroupsField;
            }
            set {
                if ((object.ReferenceEquals(this.SerieDetailGroupsField, value) != true)) {
                    this.SerieDetailGroupsField = value;
                    this.RaisePropertyChanged("SerieDetailGroups");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public StreamEntity Stream {
            get {
                return this.StreamField;
            }
            set {
                if ((object.ReferenceEquals(this.StreamField, value) != true)) {
                    this.StreamField = value;
                    this.RaisePropertyChanged("Stream");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="UserEntity", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Entities")]
    [System.SerializableAttribute()]
    public partial class UserEntity : BaseEntity {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AuthenticationMatrixField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<bool> IsFederatedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LanguageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> LastAccessField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> LastPasswordChangeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LastPasswordsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PasswordField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PasswordStatusField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string RoleField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private SecurityObject SecurityField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int UseAuthenticationMatrixField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserNameField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string AuthenticationMatrix {
            get {
                return this.AuthenticationMatrixField;
            }
            set {
                if ((object.ReferenceEquals(this.AuthenticationMatrixField, value) != true)) {
                    this.AuthenticationMatrixField = value;
                    this.RaisePropertyChanged("AuthenticationMatrix");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<bool> IsFederated {
            get {
                return this.IsFederatedField;
            }
            set {
                if ((this.IsFederatedField.Equals(value) != true)) {
                    this.IsFederatedField = value;
                    this.RaisePropertyChanged("IsFederated");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Language {
            get {
                return this.LanguageField;
            }
            set {
                if ((object.ReferenceEquals(this.LanguageField, value) != true)) {
                    this.LanguageField = value;
                    this.RaisePropertyChanged("Language");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> LastAccess {
            get {
                return this.LastAccessField;
            }
            set {
                if ((this.LastAccessField.Equals(value) != true)) {
                    this.LastAccessField = value;
                    this.RaisePropertyChanged("LastAccess");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> LastPasswordChange {
            get {
                return this.LastPasswordChangeField;
            }
            set {
                if ((this.LastPasswordChangeField.Equals(value) != true)) {
                    this.LastPasswordChangeField = value;
                    this.RaisePropertyChanged("LastPasswordChange");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LastPasswords {
            get {
                return this.LastPasswordsField;
            }
            set {
                if ((object.ReferenceEquals(this.LastPasswordsField, value) != true)) {
                    this.LastPasswordsField = value;
                    this.RaisePropertyChanged("LastPasswords");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Password {
            get {
                return this.PasswordField;
            }
            set {
                if ((object.ReferenceEquals(this.PasswordField, value) != true)) {
                    this.PasswordField = value;
                    this.RaisePropertyChanged("Password");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PasswordStatus {
            get {
                return this.PasswordStatusField;
            }
            set {
                if ((object.ReferenceEquals(this.PasswordStatusField, value) != true)) {
                    this.PasswordStatusField = value;
                    this.RaisePropertyChanged("PasswordStatus");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Role {
            get {
                return this.RoleField;
            }
            set {
                if ((object.ReferenceEquals(this.RoleField, value) != true)) {
                    this.RoleField = value;
                    this.RaisePropertyChanged("Role");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public SecurityObject Security {
            get {
                return this.SecurityField;
            }
            set {
                if ((object.ReferenceEquals(this.SecurityField, value) != true)) {
                    this.SecurityField = value;
                    this.RaisePropertyChanged("Security");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int UseAuthenticationMatrix {
            get {
                return this.UseAuthenticationMatrixField;
            }
            set {
                if ((this.UseAuthenticationMatrixField.Equals(value) != true)) {
                    this.UseAuthenticationMatrixField = value;
                    this.RaisePropertyChanged("UseAuthenticationMatrix");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserName {
            get {
                return this.UserNameField;
            }
            set {
                if ((object.ReferenceEquals(this.UserNameField, value) != true)) {
                    this.UserNameField = value;
                    this.RaisePropertyChanged("UserName");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SignalEntity", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Entities")]
    [System.SerializableAttribute()]
    public partial class SignalEntity : BaseEntity {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private SerieDetailGroupEntity SerieDetailGroupField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private StreamEntity StreamField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private UserEntity UpdateUserField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public SerieDetailGroupEntity SerieDetailGroup {
            get {
                return this.SerieDetailGroupField;
            }
            set {
                if ((object.ReferenceEquals(this.SerieDetailGroupField, value) != true)) {
                    this.SerieDetailGroupField = value;
                    this.RaisePropertyChanged("SerieDetailGroup");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public StreamEntity Stream {
            get {
                return this.StreamField;
            }
            set {
                if ((object.ReferenceEquals(this.StreamField, value) != true)) {
                    this.StreamField = value;
                    this.RaisePropertyChanged("Stream");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public UserEntity UpdateUser {
            get {
                return this.UpdateUserField;
            }
            set {
                if ((object.ReferenceEquals(this.UpdateUserField, value) != true)) {
                    this.UpdateUserField = value;
                    this.RaisePropertyChanged("UpdateUser");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MessageEntity", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Entities")]
    [System.SerializableAttribute()]
    public partial class MessageEntity : BaseEntity {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<decimal> ActivitiescountField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<decimal> ActivitiesfailedcountField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CreatedbyoperatoridField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CreatedbyuseridField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> CreationdateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FilenameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool IsGrantedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageTypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<decimal> MessageidField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> PriorityField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> RecipientDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> RecipientNumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string RecipientTextField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string RecipientapplicationnameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string RecipientidentifierField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ReferencenumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<decimal> ResultcodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ResultdetailField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<decimal> RunneridField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<decimal> RunnerqueueidField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> ScheduleForField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<decimal> ScheduleidField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SenderapplicationnameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SenderidentifierField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StatusField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TransactionisolationrequiredField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<decimal> Activitiescount {
            get {
                return this.ActivitiescountField;
            }
            set {
                if ((this.ActivitiescountField.Equals(value) != true)) {
                    this.ActivitiescountField = value;
                    this.RaisePropertyChanged("Activitiescount");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<decimal> Activitiesfailedcount {
            get {
                return this.ActivitiesfailedcountField;
            }
            set {
                if ((this.ActivitiesfailedcountField.Equals(value) != true)) {
                    this.ActivitiesfailedcountField = value;
                    this.RaisePropertyChanged("Activitiesfailedcount");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Createdbyoperatorid {
            get {
                return this.CreatedbyoperatoridField;
            }
            set {
                if ((object.ReferenceEquals(this.CreatedbyoperatoridField, value) != true)) {
                    this.CreatedbyoperatoridField = value;
                    this.RaisePropertyChanged("Createdbyoperatorid");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Createdbyuserid {
            get {
                return this.CreatedbyuseridField;
            }
            set {
                if ((object.ReferenceEquals(this.CreatedbyuseridField, value) != true)) {
                    this.CreatedbyuseridField = value;
                    this.RaisePropertyChanged("Createdbyuserid");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> Creationdate {
            get {
                return this.CreationdateField;
            }
            set {
                if ((this.CreationdateField.Equals(value) != true)) {
                    this.CreationdateField = value;
                    this.RaisePropertyChanged("Creationdate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Filename {
            get {
                return this.FilenameField;
            }
            set {
                if ((object.ReferenceEquals(this.FilenameField, value) != true)) {
                    this.FilenameField = value;
                    this.RaisePropertyChanged("Filename");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsGranted {
            get {
                return this.IsGrantedField;
            }
            set {
                if ((this.IsGrantedField.Equals(value) != true)) {
                    this.IsGrantedField = value;
                    this.RaisePropertyChanged("IsGranted");
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
        public System.Nullable<decimal> Messageid {
            get {
                return this.MessageidField;
            }
            set {
                if ((this.MessageidField.Equals(value) != true)) {
                    this.MessageidField = value;
                    this.RaisePropertyChanged("Messageid");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> Priority {
            get {
                return this.PriorityField;
            }
            set {
                if ((this.PriorityField.Equals(value) != true)) {
                    this.PriorityField = value;
                    this.RaisePropertyChanged("Priority");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> RecipientDate {
            get {
                return this.RecipientDateField;
            }
            set {
                if ((this.RecipientDateField.Equals(value) != true)) {
                    this.RecipientDateField = value;
                    this.RaisePropertyChanged("RecipientDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> RecipientNumber {
            get {
                return this.RecipientNumberField;
            }
            set {
                if ((this.RecipientNumberField.Equals(value) != true)) {
                    this.RecipientNumberField = value;
                    this.RaisePropertyChanged("RecipientNumber");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string RecipientText {
            get {
                return this.RecipientTextField;
            }
            set {
                if ((object.ReferenceEquals(this.RecipientTextField, value) != true)) {
                    this.RecipientTextField = value;
                    this.RaisePropertyChanged("RecipientText");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Recipientapplicationname {
            get {
                return this.RecipientapplicationnameField;
            }
            set {
                if ((object.ReferenceEquals(this.RecipientapplicationnameField, value) != true)) {
                    this.RecipientapplicationnameField = value;
                    this.RaisePropertyChanged("Recipientapplicationname");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Recipientidentifier {
            get {
                return this.RecipientidentifierField;
            }
            set {
                if ((object.ReferenceEquals(this.RecipientidentifierField, value) != true)) {
                    this.RecipientidentifierField = value;
                    this.RaisePropertyChanged("Recipientidentifier");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Referencenumber {
            get {
                return this.ReferencenumberField;
            }
            set {
                if ((object.ReferenceEquals(this.ReferencenumberField, value) != true)) {
                    this.ReferencenumberField = value;
                    this.RaisePropertyChanged("Referencenumber");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<decimal> Resultcode {
            get {
                return this.ResultcodeField;
            }
            set {
                if ((this.ResultcodeField.Equals(value) != true)) {
                    this.ResultcodeField = value;
                    this.RaisePropertyChanged("Resultcode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Resultdetail {
            get {
                return this.ResultdetailField;
            }
            set {
                if ((object.ReferenceEquals(this.ResultdetailField, value) != true)) {
                    this.ResultdetailField = value;
                    this.RaisePropertyChanged("Resultdetail");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<decimal> Runnerid {
            get {
                return this.RunneridField;
            }
            set {
                if ((this.RunneridField.Equals(value) != true)) {
                    this.RunneridField = value;
                    this.RaisePropertyChanged("Runnerid");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<decimal> Runnerqueueid {
            get {
                return this.RunnerqueueidField;
            }
            set {
                if ((this.RunnerqueueidField.Equals(value) != true)) {
                    this.RunnerqueueidField = value;
                    this.RaisePropertyChanged("Runnerqueueid");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> ScheduleFor {
            get {
                return this.ScheduleForField;
            }
            set {
                if ((this.ScheduleForField.Equals(value) != true)) {
                    this.ScheduleForField = value;
                    this.RaisePropertyChanged("ScheduleFor");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<decimal> Scheduleid {
            get {
                return this.ScheduleidField;
            }
            set {
                if ((this.ScheduleidField.Equals(value) != true)) {
                    this.ScheduleidField = value;
                    this.RaisePropertyChanged("Scheduleid");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Senderapplicationname {
            get {
                return this.SenderapplicationnameField;
            }
            set {
                if ((object.ReferenceEquals(this.SenderapplicationnameField, value) != true)) {
                    this.SenderapplicationnameField = value;
                    this.RaisePropertyChanged("Senderapplicationname");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Senderidentifier {
            get {
                return this.SenderidentifierField;
            }
            set {
                if ((object.ReferenceEquals(this.SenderidentifierField, value) != true)) {
                    this.SenderidentifierField = value;
                    this.RaisePropertyChanged("Senderidentifier");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Status {
            get {
                return this.StatusField;
            }
            set {
                if ((object.ReferenceEquals(this.StatusField, value) != true)) {
                    this.StatusField = value;
                    this.RaisePropertyChanged("Status");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Transactionisolationrequired {
            get {
                return this.TransactionisolationrequiredField;
            }
            set {
                if ((object.ReferenceEquals(this.TransactionisolationrequiredField, value) != true)) {
                    this.TransactionisolationrequiredField = value;
                    this.RaisePropertyChanged("Transactionisolationrequired");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MessageOutEntity", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Entities")]
    [System.SerializableAttribute()]
    public partial class MessageOutEntity : BaseEntity {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> CreationdateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> DownloaddateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FilenameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool IsGrantedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> MessageOutidField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageOuttypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<long> MessageidField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string RecipientApplicationNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> RecipientDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> RecipientNumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string RecipientTextField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string RecipientoperatoridField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ReferencenumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SenderoperatoridField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SenderuseridField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StatusField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> Creationdate {
            get {
                return this.CreationdateField;
            }
            set {
                if ((this.CreationdateField.Equals(value) != true)) {
                    this.CreationdateField = value;
                    this.RaisePropertyChanged("Creationdate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> Downloaddate {
            get {
                return this.DownloaddateField;
            }
            set {
                if ((this.DownloaddateField.Equals(value) != true)) {
                    this.DownloaddateField = value;
                    this.RaisePropertyChanged("Downloaddate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Filename {
            get {
                return this.FilenameField;
            }
            set {
                if ((object.ReferenceEquals(this.FilenameField, value) != true)) {
                    this.FilenameField = value;
                    this.RaisePropertyChanged("Filename");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsGranted {
            get {
                return this.IsGrantedField;
            }
            set {
                if ((this.IsGrantedField.Equals(value) != true)) {
                    this.IsGrantedField = value;
                    this.RaisePropertyChanged("IsGranted");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> MessageOutid {
            get {
                return this.MessageOutidField;
            }
            set {
                if ((this.MessageOutidField.Equals(value) != true)) {
                    this.MessageOutidField = value;
                    this.RaisePropertyChanged("MessageOutid");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string MessageOuttype {
            get {
                return this.MessageOuttypeField;
            }
            set {
                if ((object.ReferenceEquals(this.MessageOuttypeField, value) != true)) {
                    this.MessageOuttypeField = value;
                    this.RaisePropertyChanged("MessageOuttype");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<long> Messageid {
            get {
                return this.MessageidField;
            }
            set {
                if ((this.MessageidField.Equals(value) != true)) {
                    this.MessageidField = value;
                    this.RaisePropertyChanged("Messageid");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string RecipientApplicationName {
            get {
                return this.RecipientApplicationNameField;
            }
            set {
                if ((object.ReferenceEquals(this.RecipientApplicationNameField, value) != true)) {
                    this.RecipientApplicationNameField = value;
                    this.RaisePropertyChanged("RecipientApplicationName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> RecipientDate {
            get {
                return this.RecipientDateField;
            }
            set {
                if ((this.RecipientDateField.Equals(value) != true)) {
                    this.RecipientDateField = value;
                    this.RaisePropertyChanged("RecipientDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> RecipientNumber {
            get {
                return this.RecipientNumberField;
            }
            set {
                if ((this.RecipientNumberField.Equals(value) != true)) {
                    this.RecipientNumberField = value;
                    this.RaisePropertyChanged("RecipientNumber");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string RecipientText {
            get {
                return this.RecipientTextField;
            }
            set {
                if ((object.ReferenceEquals(this.RecipientTextField, value) != true)) {
                    this.RecipientTextField = value;
                    this.RaisePropertyChanged("RecipientText");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Recipientoperatorid {
            get {
                return this.RecipientoperatoridField;
            }
            set {
                if ((object.ReferenceEquals(this.RecipientoperatoridField, value) != true)) {
                    this.RecipientoperatoridField = value;
                    this.RaisePropertyChanged("Recipientoperatorid");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Referencenumber {
            get {
                return this.ReferencenumberField;
            }
            set {
                if ((object.ReferenceEquals(this.ReferencenumberField, value) != true)) {
                    this.ReferencenumberField = value;
                    this.RaisePropertyChanged("Referencenumber");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Senderoperatorid {
            get {
                return this.SenderoperatoridField;
            }
            set {
                if ((object.ReferenceEquals(this.SenderoperatoridField, value) != true)) {
                    this.SenderoperatoridField = value;
                    this.RaisePropertyChanged("Senderoperatorid");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Senderuserid {
            get {
                return this.SenderuseridField;
            }
            set {
                if ((object.ReferenceEquals(this.SenderuseridField, value) != true)) {
                    this.SenderuseridField = value;
                    this.RaisePropertyChanged("Senderuserid");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Status {
            get {
                return this.StatusField;
            }
            set {
                if ((object.ReferenceEquals(this.StatusField, value) != true)) {
                    this.StatusField = value;
                    this.RaisePropertyChanged("Status");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="LogEntity", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Entities")]
    [System.SerializableAttribute()]
    public partial class LogEntity : BaseEntity {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private ActivityEntity ActivityField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime CreationdateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> EndDtField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> IndentcountField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> IsmultilanguageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long LogsessionidField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MarketEntity MarketField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private byte[] MultilanguageparameterField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long SequencenumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private SerieEntity SerieField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> SeverityField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> StartDtField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private StreamEntity StreamField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private UnitEntity UnitField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private UserEntity UserField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ActivityEntity Activity {
            get {
                return this.ActivityField;
            }
            set {
                if ((object.ReferenceEquals(this.ActivityField, value) != true)) {
                    this.ActivityField = value;
                    this.RaisePropertyChanged("Activity");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime Creationdate {
            get {
                return this.CreationdateField;
            }
            set {
                if ((this.CreationdateField.Equals(value) != true)) {
                    this.CreationdateField = value;
                    this.RaisePropertyChanged("Creationdate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> EndDt {
            get {
                return this.EndDtField;
            }
            set {
                if ((this.EndDtField.Equals(value) != true)) {
                    this.EndDtField = value;
                    this.RaisePropertyChanged("EndDt");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> Indentcount {
            get {
                return this.IndentcountField;
            }
            set {
                if ((this.IndentcountField.Equals(value) != true)) {
                    this.IndentcountField = value;
                    this.RaisePropertyChanged("Indentcount");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> Ismultilanguage {
            get {
                return this.IsmultilanguageField;
            }
            set {
                if ((this.IsmultilanguageField.Equals(value) != true)) {
                    this.IsmultilanguageField = value;
                    this.RaisePropertyChanged("Ismultilanguage");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long Logsessionid {
            get {
                return this.LogsessionidField;
            }
            set {
                if ((this.LogsessionidField.Equals(value) != true)) {
                    this.LogsessionidField = value;
                    this.RaisePropertyChanged("Logsessionid");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public MarketEntity Market {
            get {
                return this.MarketField;
            }
            set {
                if ((object.ReferenceEquals(this.MarketField, value) != true)) {
                    this.MarketField = value;
                    this.RaisePropertyChanged("Market");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Message {
            get {
                return this.MessageField;
            }
            set {
                if ((object.ReferenceEquals(this.MessageField, value) != true)) {
                    this.MessageField = value;
                    this.RaisePropertyChanged("Message");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte[] Multilanguageparameter {
            get {
                return this.MultilanguageparameterField;
            }
            set {
                if ((object.ReferenceEquals(this.MultilanguageparameterField, value) != true)) {
                    this.MultilanguageparameterField = value;
                    this.RaisePropertyChanged("Multilanguageparameter");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long Sequencenumber {
            get {
                return this.SequencenumberField;
            }
            set {
                if ((this.SequencenumberField.Equals(value) != true)) {
                    this.SequencenumberField = value;
                    this.RaisePropertyChanged("Sequencenumber");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public SerieEntity Serie {
            get {
                return this.SerieField;
            }
            set {
                if ((object.ReferenceEquals(this.SerieField, value) != true)) {
                    this.SerieField = value;
                    this.RaisePropertyChanged("Serie");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> Severity {
            get {
                return this.SeverityField;
            }
            set {
                if ((this.SeverityField.Equals(value) != true)) {
                    this.SeverityField = value;
                    this.RaisePropertyChanged("Severity");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> StartDt {
            get {
                return this.StartDtField;
            }
            set {
                if ((this.StartDtField.Equals(value) != true)) {
                    this.StartDtField = value;
                    this.RaisePropertyChanged("StartDt");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public StreamEntity Stream {
            get {
                return this.StreamField;
            }
            set {
                if ((object.ReferenceEquals(this.StreamField, value) != true)) {
                    this.StreamField = value;
                    this.RaisePropertyChanged("Stream");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public UnitEntity Unit {
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
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public UserEntity User {
            get {
                return this.UserField;
            }
            set {
                if ((object.ReferenceEquals(this.UserField, value) != true)) {
                    this.UserField = value;
                    this.RaisePropertyChanged("User");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FmkActivityEntity", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Entities")]
    [System.SerializableAttribute()]
    public partial class FmkActivityEntity : BaseEntity {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> ActivityidField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DirectionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime ExecutionEndField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime ExecutionStartField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> MessageidField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> ResultcodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ResultdetailField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StatusField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> TransactiongroupField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> Activityid {
            get {
                return this.ActivityidField;
            }
            set {
                if ((this.ActivityidField.Equals(value) != true)) {
                    this.ActivityidField = value;
                    this.RaisePropertyChanged("Activityid");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Direction {
            get {
                return this.DirectionField;
            }
            set {
                if ((object.ReferenceEquals(this.DirectionField, value) != true)) {
                    this.DirectionField = value;
                    this.RaisePropertyChanged("Direction");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime ExecutionEnd {
            get {
                return this.ExecutionEndField;
            }
            set {
                if ((this.ExecutionEndField.Equals(value) != true)) {
                    this.ExecutionEndField = value;
                    this.RaisePropertyChanged("ExecutionEnd");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime ExecutionStart {
            get {
                return this.ExecutionStartField;
            }
            set {
                if ((this.ExecutionStartField.Equals(value) != true)) {
                    this.ExecutionStartField = value;
                    this.RaisePropertyChanged("ExecutionStart");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> Messageid {
            get {
                return this.MessageidField;
            }
            set {
                if ((this.MessageidField.Equals(value) != true)) {
                    this.MessageidField = value;
                    this.RaisePropertyChanged("Messageid");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> Resultcode {
            get {
                return this.ResultcodeField;
            }
            set {
                if ((this.ResultcodeField.Equals(value) != true)) {
                    this.ResultcodeField = value;
                    this.RaisePropertyChanged("Resultcode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Resultdetail {
            get {
                return this.ResultdetailField;
            }
            set {
                if ((object.ReferenceEquals(this.ResultdetailField, value) != true)) {
                    this.ResultdetailField = value;
                    this.RaisePropertyChanged("Resultdetail");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Status {
            get {
                return this.StatusField;
            }
            set {
                if ((object.ReferenceEquals(this.StatusField, value) != true)) {
                    this.StatusField = value;
                    this.RaisePropertyChanged("Status");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> Transactiongroup {
            get {
                return this.TransactiongroupField;
            }
            set {
                if ((this.TransactiongroupField.Equals(value) != true)) {
                    this.TransactiongroupField = value;
                    this.RaisePropertyChanged("Transactiongroup");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ActivityAssociationEntity", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Entities")]
    [System.SerializableAttribute()]
    public partial class ActivityAssociationEntity : BaseEntity {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private ActivityEntity ActivityField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MarketEntity MarketField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private UnitEntity UnitField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ActivityEntity Activity {
            get {
                return this.ActivityField;
            }
            set {
                if ((object.ReferenceEquals(this.ActivityField, value) != true)) {
                    this.ActivityField = value;
                    this.RaisePropertyChanged("Activity");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public MarketEntity Market {
            get {
                return this.MarketField;
            }
            set {
                if ((object.ReferenceEquals(this.MarketField, value) != true)) {
                    this.MarketField = value;
                    this.RaisePropertyChanged("Market");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public UnitEntity Unit {
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
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="EntityDBStatus", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Types.Enums")]
    public enum EntityDBStatus : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Exist = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Create = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Update = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Delete = 3,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ColumnType", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Types.Enums")]
    public enum ColumnType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Text = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Number = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        CheckBox = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        ComboBox = 3,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SecurityObject", Namespace="http://schemas.datacontract.org/2004/07/EnergyCore.Security.Model")]
    [System.SerializableAttribute()]
    public partial class SecurityObject : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private SecurityObject ParentField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private SecurityRule[] RulesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TypeField;
        
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
        public SecurityObject Parent {
            get {
                return this.ParentField;
            }
            set {
                if ((object.ReferenceEquals(this.ParentField, value) != true)) {
                    this.ParentField = value;
                    this.RaisePropertyChanged("Parent");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public SecurityRule[] Rules {
            get {
                return this.RulesField;
            }
            set {
                if ((object.ReferenceEquals(this.RulesField, value) != true)) {
                    this.RulesField = value;
                    this.RaisePropertyChanged("Rules");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Type {
            get {
                return this.TypeField;
            }
            set {
                if ((object.ReferenceEquals(this.TypeField, value) != true)) {
                    this.TypeField = value;
                    this.RaisePropertyChanged("Type");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="SecurityRule", Namespace="http://schemas.datacontract.org/2004/07/EnergyCore.Security.Model")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(TmsBaseRule))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(MarketEnablingRule))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(MessageTypeRule))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ViewRoleRule))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(TmsFileServiceAccessRule))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(UnitEnablingRule))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ViewPageRule))]
    public partial class SecurityRule : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private AccessTypes AccessTypeField;
        
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
        public AccessTypes AccessType {
            get {
                return this.AccessTypeField;
            }
            set {
                if ((this.AccessTypeField.Equals(value) != true)) {
                    this.AccessTypeField = value;
                    this.RaisePropertyChanged("AccessType");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="TmsBaseRule", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Security.Rules")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(MarketEnablingRule))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(MessageTypeRule))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ViewRoleRule))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(TmsFileServiceAccessRule))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(UnitEnablingRule))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ViewPageRule))]
    public partial class TmsBaseRule : SecurityRule {
        
        private System.Nullable<int> _RuleIdField;
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public System.Nullable<int> _RuleId {
            get {
                return this._RuleIdField;
            }
            set {
                if ((this._RuleIdField.Equals(value) != true)) {
                    this._RuleIdField = value;
                    this.RaisePropertyChanged("_RuleId");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MarketEnablingRule", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Security.Rules")]
    [System.SerializableAttribute()]
    public partial class MarketEnablingRule : TmsBaseRule {
        
        private string MarketNamek__BackingFieldField;
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<MarketName>k__BackingField", IsRequired=true)]
        public string MarketNamek__BackingField {
            get {
                return this.MarketNamek__BackingFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.MarketNamek__BackingFieldField, value) != true)) {
                    this.MarketNamek__BackingFieldField = value;
                    this.RaisePropertyChanged("MarketNamek__BackingField");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MessageTypeRule", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Security.Rules")]
    [System.SerializableAttribute()]
    public partial class MessageTypeRule : TmsBaseRule {
        
        private string MessageTypek__BackingFieldField;
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<MessageType>k__BackingField", IsRequired=true)]
        public string MessageTypek__BackingField {
            get {
                return this.MessageTypek__BackingFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.MessageTypek__BackingFieldField, value) != true)) {
                    this.MessageTypek__BackingFieldField = value;
                    this.RaisePropertyChanged("MessageTypek__BackingField");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ViewRoleRule", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Security.Rules")]
    [System.SerializableAttribute()]
    public partial class ViewRoleRule : TmsBaseRule {
        
        private string RoleNamek__BackingFieldField;
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<RoleName>k__BackingField", IsRequired=true)]
        public string RoleNamek__BackingField {
            get {
                return this.RoleNamek__BackingFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.RoleNamek__BackingFieldField, value) != true)) {
                    this.RoleNamek__BackingFieldField = value;
                    this.RaisePropertyChanged("RoleNamek__BackingField");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TmsFileServiceAccessRule", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Security.Rules")]
    [System.SerializableAttribute()]
    public partial class TmsFileServiceAccessRule : TmsBaseRule {
        
        private string Methodk__BackingFieldField;
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<Method>k__BackingField", IsRequired=true)]
        public string Methodk__BackingField {
            get {
                return this.Methodk__BackingFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.Methodk__BackingFieldField, value) != true)) {
                    this.Methodk__BackingFieldField = value;
                    this.RaisePropertyChanged("Methodk__BackingField");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="UnitEnablingRule", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Security.Rules")]
    [System.SerializableAttribute()]
    public partial class UnitEnablingRule : TmsBaseRule {
        
        private string UnitNamek__BackingFieldField;
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<UnitName>k__BackingField", IsRequired=true)]
        public string UnitNamek__BackingField {
            get {
                return this.UnitNamek__BackingFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.UnitNamek__BackingFieldField, value) != true)) {
                    this.UnitNamek__BackingFieldField = value;
                    this.RaisePropertyChanged("UnitNamek__BackingField");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ViewPageRule", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Security.Rules")]
    [System.SerializableAttribute()]
    public partial class ViewPageRule : TmsBaseRule {
        
        private bool EnableModifyk__BackingFieldField;
        
        private bool EnableOperativek__BackingFieldField;
        
        private string PathNamek__BackingFieldField;
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<EnableModify>k__BackingField", IsRequired=true)]
        public bool EnableModifyk__BackingField {
            get {
                return this.EnableModifyk__BackingFieldField;
            }
            set {
                if ((this.EnableModifyk__BackingFieldField.Equals(value) != true)) {
                    this.EnableModifyk__BackingFieldField = value;
                    this.RaisePropertyChanged("EnableModifyk__BackingField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<EnableOperative>k__BackingField", IsRequired=true)]
        public bool EnableOperativek__BackingField {
            get {
                return this.EnableOperativek__BackingFieldField;
            }
            set {
                if ((this.EnableOperativek__BackingFieldField.Equals(value) != true)) {
                    this.EnableOperativek__BackingFieldField = value;
                    this.RaisePropertyChanged("EnableOperativek__BackingField");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Name="<PathName>k__BackingField", IsRequired=true)]
        public string PathNamek__BackingField {
            get {
                return this.PathNamek__BackingFieldField;
            }
            set {
                if ((object.ReferenceEquals(this.PathNamek__BackingFieldField, value) != true)) {
                    this.PathNamek__BackingFieldField = value;
                    this.RaisePropertyChanged("PathNamek__BackingField");
                }
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AccessTypes", Namespace="http://schemas.datacontract.org/2004/07/EnergyCore.Security.Model")]
    public enum AccessTypes : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        NotSpecified = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Allow = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Deny = 2,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MatchType", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Types.Enums")]
    public enum MatchType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Exact = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Exclusive = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        ExactAndNull = 2,
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TmsBlob", Namespace="http://schemas.datacontract.org/2004/07/TMS.Core.Data")]
    [System.SerializableAttribute()]
    public partial class TmsBlob : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CompressorNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private byte[] ContentField;
        
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
        public string CompressorName {
            get {
                return this.CompressorNameField;
            }
            set {
                if ((object.ReferenceEquals(this.CompressorNameField, value) != true)) {
                    this.CompressorNameField = value;
                    this.RaisePropertyChanged("CompressorName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte[] Content {
            get {
                return this.ContentField;
            }
            set {
                if ((object.ReferenceEquals(this.ContentField, value) != true)) {
                    this.ContentField = value;
                    this.RaisePropertyChanged("Content");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="MessageType", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Types.Enums")]
    public enum MessageType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Input = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Output = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Published = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Calculation = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Log = 4,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Message.Priorities", Namespace="http://schemas.datacontract.org/2004/07/TMS.Core.Data")]
    public enum MessagePriorities : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Default = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        High = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Higher = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Highest = 3,
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
    [System.Runtime.Serialization.DataContractAttribute(Name="SerieDataItem", Namespace="http://schemas.datacontract.org/2004/07/TMS.Core.Data.Simple")]
    [System.SerializableAttribute()]
    public partial class SerieDataItem : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.Dictionary<System.DateTime, object> DataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private DataItem[] ExtendedDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private SerieDescription SerieField;
        
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
        public System.Collections.Generic.Dictionary<System.DateTime, object> Data {
            get {
                return this.DataField;
            }
            set {
                if ((object.ReferenceEquals(this.DataField, value) != true)) {
                    this.DataField = value;
                    this.RaisePropertyChanged("Data");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public DataItem[] ExtendedData {
            get {
                return this.ExtendedDataField;
            }
            set {
                if ((object.ReferenceEquals(this.ExtendedDataField, value) != true)) {
                    this.ExtendedDataField = value;
                    this.RaisePropertyChanged("ExtendedData");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public SerieDescription Serie {
            get {
                return this.SerieField;
            }
            set {
                if ((object.ReferenceEquals(this.SerieField, value) != true)) {
                    this.SerieField = value;
                    this.RaisePropertyChanged("Serie");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="SerieDescription", Namespace="http://schemas.datacontract.org/2004/07/TMS.Core.Data.Simple")]
    [System.SerializableAttribute()]
    public partial class SerieDescription : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ChannelField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ClassField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ContextField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FrequencyField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool HistoricizedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SourceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SymbolField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UoMField;
        
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
        public string Channel {
            get {
                return this.ChannelField;
            }
            set {
                if ((object.ReferenceEquals(this.ChannelField, value) != true)) {
                    this.ChannelField = value;
                    this.RaisePropertyChanged("Channel");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Class {
            get {
                return this.ClassField;
            }
            set {
                if ((object.ReferenceEquals(this.ClassField, value) != true)) {
                    this.ClassField = value;
                    this.RaisePropertyChanged("Class");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Context {
            get {
                return this.ContextField;
            }
            set {
                if ((object.ReferenceEquals(this.ContextField, value) != true)) {
                    this.ContextField = value;
                    this.RaisePropertyChanged("Context");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Frequency {
            get {
                return this.FrequencyField;
            }
            set {
                if ((object.ReferenceEquals(this.FrequencyField, value) != true)) {
                    this.FrequencyField = value;
                    this.RaisePropertyChanged("Frequency");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Historicized {
            get {
                return this.HistoricizedField;
            }
            set {
                if ((this.HistoricizedField.Equals(value) != true)) {
                    this.HistoricizedField = value;
                    this.RaisePropertyChanged("Historicized");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Source {
            get {
                return this.SourceField;
            }
            set {
                if ((object.ReferenceEquals(this.SourceField, value) != true)) {
                    this.SourceField = value;
                    this.RaisePropertyChanged("Source");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Symbol {
            get {
                return this.SymbolField;
            }
            set {
                if ((object.ReferenceEquals(this.SymbolField, value) != true)) {
                    this.SymbolField = value;
                    this.RaisePropertyChanged("Symbol");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UoM {
            get {
                return this.UoMField;
            }
            set {
                if ((object.ReferenceEquals(this.UoMField, value) != true)) {
                    this.UoMField = value;
                    this.RaisePropertyChanged("UoM");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="DataItem", Namespace="http://schemas.datacontract.org/2004/07/TMS.Core.Data.Simple")]
    [System.SerializableAttribute()]
    public partial class DataItem : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<double> DoubleValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTimeOffset EndDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTimeOffset StartDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int StreamIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StringValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime TimestampField;
        
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
        public System.Nullable<double> DoubleValue {
            get {
                return this.DoubleValueField;
            }
            set {
                if ((this.DoubleValueField.Equals(value) != true)) {
                    this.DoubleValueField = value;
                    this.RaisePropertyChanged("DoubleValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTimeOffset EndDate {
            get {
                return this.EndDateField;
            }
            set {
                if ((this.EndDateField.Equals(value) != true)) {
                    this.EndDateField = value;
                    this.RaisePropertyChanged("EndDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTimeOffset StartDate {
            get {
                return this.StartDateField;
            }
            set {
                if ((this.StartDateField.Equals(value) != true)) {
                    this.StartDateField = value;
                    this.RaisePropertyChanged("StartDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int StreamId {
            get {
                return this.StreamIdField;
            }
            set {
                if ((this.StreamIdField.Equals(value) != true)) {
                    this.StreamIdField = value;
                    this.RaisePropertyChanged("StreamId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StringValue {
            get {
                return this.StringValueField;
            }
            set {
                if ((object.ReferenceEquals(this.StringValueField, value) != true)) {
                    this.StringValueField = value;
                    this.RaisePropertyChanged("StringValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime Timestamp {
            get {
                return this.TimestampField;
            }
            set {
                if ((this.TimestampField.Equals(value) != true)) {
                    this.TimestampField = value;
                    this.RaisePropertyChanged("Timestamp");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="ApplicationType", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Types.Enums")]
    public enum ApplicationType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        TmsWa = 0,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="BalanceOrderMessageType", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Types.Enums")]
    public enum BalanceOrderMessageType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        BalanceOrderMessage = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        BalanceOrderCancellationMessage = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        GenericMessage = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        BalanceExclusionMessage = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        BalanceLimitationMessage = 4,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        SecondaryAdjustmentMessage = 5,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        ReactivePowerMessage = 6,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        UnavailabilityRejectionMessage = 7,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        ReservedQuantityMessage = 8,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ChannelType", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Types.Enums")]
    public enum ChannelType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Pmin = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Pmax = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Pass = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Setup = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Signal = 4,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        EnergyPrice = 5,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        BidQuantity = 6,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        ContractID = 7,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        SourceOffer = 8,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        PresentedOffer = 9,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        PredefinedOffer = 10,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        TransactionTs = 11,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        MessageType = 12,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        StartDate = 13,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        EndDate = 14,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        StartDateOffset = 15,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        EndDateOffset = 16,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        DispatchMessageId = 17,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        DispatchMessageFileName = 18,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        PVPowerDelta_TINI = 19,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        PVPowerDelta_TFIN = 20,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        ContinuationType = 21,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        PV_TINI = 22,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        PV_TFIN = 23,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        CustomStartupTime = 24,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        CustomGradient = 25,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        IsFinalPV_TINI = 26,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        IsFinalPV_TFIN = 27,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        IsLinkOrder = 28,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        PNR = 29,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        RevokedMessageID = 30,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        CreatedBy = 31,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Readmission = 32,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        MinPowerLimit = 33,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        MaxPowerLimit = 34,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Restore = 35,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Reason = 36,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        SecondaryAdjustment = 37,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Excitement = 38,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        ReserveType = 39,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        ReservedQuantityMW = 40,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        ReservedQuantity = 41,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Operation = 42,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Imm = 43,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Pre = 44,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Ecc = 45,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        PV = 46,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        PVM = 47,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        RTDelta = 48,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        FromPlant = 49,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        ToPlant = 50,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        FromPlantPV = 51,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        ToPlantPV = 52,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Cost = 53,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        MinEnImm = 54,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        MaxEnImm = 55,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        MaxEnPrel = 56,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FormulaType", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Types.Enums")]
    public enum FormulaType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Series = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Signals = 1,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="LanguageType", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Types.Enums")]
    public enum LanguageType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Italian = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        English = 1,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="LogType", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Types.Enums")]
    public enum LogType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Info = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        InfoCustom = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        InfoCustomGlobalized = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        InfoGlobalized = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Error = 4,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        ErrorCustom = 5,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        ErrorCustomGlobalized = 6,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        ErrorGlobalized = 7,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Warning = 8,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MarketType", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Types.Enums")]
    public enum MarketType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        MSD1 = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        MSD2 = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        MSD3 = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        MSD4 = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        MSD5 = 4,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        MSD6 = 5,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="OtherRUPType", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Types.Enums")]
    public enum OtherRUPType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        MIB = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        MIRR = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        MIRP = 2,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="PasswordStatusType", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Types.Enums")]
    public enum PasswordStatusType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Init = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Valid = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Block = 2,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="PlanType", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Types.Enums")]
    public enum PlanType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        PVtc = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        PVMnf = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        PVM = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        PVMC = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        PVMCS = 4,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="RoleType", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Types.Enums")]
    public enum RoleType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Customer = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Admin = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Operator = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Toller = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Viewer = 4,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="RuleType", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Types.Enums")]
    public enum RuleType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Page = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Unit = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        User = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Role = 3,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SCWOperationType", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Types.Enums")]
    public enum SCWOperationType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Insert = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Update = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Delete = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        DeleteInsert = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Calcola = 4,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="StatusType", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Types.Enums")]
    public enum StatusType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        RUN = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        COM = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        ERR = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        SUB = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        DEL = 4,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        SCH = 5,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TmsCommandTypes", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Types.Enums")]
    public enum TmsCommandTypes : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        ForecastedHRPCalculation = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        ActualHRPCalculation = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        FlowPublish = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        FlowPublishRUP = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        FlowPublishSCW = 4,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        SendToTerna = 5,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        GetFileListFromTerna = 6,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        GetFromTerna = 7,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        UploadChecker = 8,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        LogCleaner = 9,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        ExportOBRSToEnBO = 10,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        GetVDTFromTerna = 11,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        GetMIBFromTerna = 12,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="UpdateType", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Types.Enums")]
    public enum UpdateType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Overwrite = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Append = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Delete = 2,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="VirtualClassDescriptionType", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Types.Enums")]
    public enum VirtualClassDescriptionType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        HrpFPublish = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        HrpCPublish = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Selection = 2,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DirectionType", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Types.Enums")]
    public enum DirectionType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Input = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Output = 1,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="RelevantActivityType", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Types.Enums")]
    public enum RelevantActivityType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        ForecastedHRP = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        ActualHRP = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        UnitsConfiguration = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        RupManamgement = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        MSDOffers = 4,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        GasConsumption = 5,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        RealTime = 6,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        RealTimeStrategy = 7,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="RollupType", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Types.Enums")]
    public enum RollupType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        All = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Detail = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        PartialSum = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        TotalSum = 3,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="RUPType", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Types.Enums")]
    public enum RUPType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Static = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Dynamic = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Virtual = 2,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SeverityType", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Types.Enums")]
    public enum SeverityType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Error = 32,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Warning = 64,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Info = 96,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Debug = 128,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        vDebug = 160,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        All = 255,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SignalColorType", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Types.Enums")]
    public enum SignalColorType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Grey = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Green = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Yellow = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Red = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        GreenRed = 4,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        YellowRed = 5,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        White = 6,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SignalType", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Types.Enums")]
    public enum SignalType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Input = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Output = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Published = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        General = 3,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SourceType", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Types.Enums")]
    public enum SourceType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        ExcelSender = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Plant = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        PlantCurves = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Configuration = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Calculation = 4,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Terna = 5,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        GME = 6,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        DCS = 7,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        EES = 8,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FrequencyType", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Types.Enums")]
    public enum FrequencyType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Minute = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        FifteenMinutes = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Hour = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Day = 4,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Month = 5,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Year = 6,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Irregular = 7,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Muddled = 8,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Overlapped = 9,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        None = 10,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="OriginType", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Types.Enums")]
    public enum OriginType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        API = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Xml = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Interpolation = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        UserEdited = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Calculated = 4,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Uploading = 5,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Uploaded = 6,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        UploadKo = 7,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Modified = 8,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Confirmed = 9,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="UoMType", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Types.Enums")]
    public enum UoMType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        None = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        MWh = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        MW = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        CelsiusDegree = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        KJKg = 4,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Bar = 5,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Percentage = 6,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Hz = 7,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        KJSmc = 8,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        KgSmc = 9,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        MS = 10,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        MWm = 11,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        GJ = 12,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Min = 13,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Euro = 14,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CalculatePlansDataItem", Namespace="http://schemas.datacontract.org/2004/07/TMS.Svc.MainService.Types")]
    [System.SerializableAttribute()]
    public partial class CalculatePlansDataItem : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private decimal BDEField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.TimeSpan DataGenerationDurationField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTimeOffset DataGenerationTimestampField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ErrorField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<System.DateTimeOffset, System.Nullable<decimal>>> FMSField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.Dictionary<string, CalculatePlansFasciaItem[]> FasceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime LatestMeasureTimeField;
        
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
        public System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<System.DateTimeOffset, System.Nullable<decimal>>> FMS {
            get {
                return this.FMSField;
            }
            set {
                if ((object.ReferenceEquals(this.FMSField, value) != true)) {
                    this.FMSField = value;
                    this.RaisePropertyChanged("FMS");
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
        public System.DateTime LatestMeasureTime {
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
    [System.Runtime.Serialization.DataContractAttribute(Name="CalculatePlansFasciaItem", Namespace="http://schemas.datacontract.org/2004/07/TMS.Svc.MainService.Types")]
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
    [System.Runtime.Serialization.DataContractAttribute(Name="TmsParameterCollection", Namespace="http://schemas.datacontract.org/2004/07/TMS.Common.Utils.DynamicCode")]
    [System.SerializableAttribute()]
    public partial class TmsParameterCollection : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
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
    [System.Runtime.Serialization.DataContractAttribute(Name="GetCalculatePlansDataOptions", Namespace="http://schemas.datacontract.org/2004/07/TMS.Svc.MainService")]
    public enum GetCalculatePlansDataOptions : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        None = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        ResampleToFifteenMinutes = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        OnlyLatestFMS = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Defaults = 3,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Unit", Namespace="http://schemas.datacontract.org/2004/07/TMS.Core.Data.EntityFramework", IsReference=true)]
    [System.SerializableAttribute()]
    public partial class Unit : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private UnitAsset[] AssetsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ETSOCodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ExternalIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private UnitProperty[] PropertiesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SymbolField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Tag[] TagsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UnitNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UnitTypeField;
        
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
        public UnitAsset[] Assets {
            get {
                return this.AssetsField;
            }
            set {
                if ((object.ReferenceEquals(this.AssetsField, value) != true)) {
                    this.AssetsField = value;
                    this.RaisePropertyChanged("Assets");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ETSOCode {
            get {
                return this.ETSOCodeField;
            }
            set {
                if ((object.ReferenceEquals(this.ETSOCodeField, value) != true)) {
                    this.ETSOCodeField = value;
                    this.RaisePropertyChanged("ETSOCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ExternalId {
            get {
                return this.ExternalIdField;
            }
            set {
                if ((object.ReferenceEquals(this.ExternalIdField, value) != true)) {
                    this.ExternalIdField = value;
                    this.RaisePropertyChanged("ExternalId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public UnitProperty[] Properties {
            get {
                return this.PropertiesField;
            }
            set {
                if ((object.ReferenceEquals(this.PropertiesField, value) != true)) {
                    this.PropertiesField = value;
                    this.RaisePropertyChanged("Properties");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Symbol {
            get {
                return this.SymbolField;
            }
            set {
                if ((object.ReferenceEquals(this.SymbolField, value) != true)) {
                    this.SymbolField = value;
                    this.RaisePropertyChanged("Symbol");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Tag[] Tags {
            get {
                return this.TagsField;
            }
            set {
                if ((object.ReferenceEquals(this.TagsField, value) != true)) {
                    this.TagsField = value;
                    this.RaisePropertyChanged("Tags");
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
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UnitType {
            get {
                return this.UnitTypeField;
            }
            set {
                if ((object.ReferenceEquals(this.UnitTypeField, value) != true)) {
                    this.UnitTypeField = value;
                    this.RaisePropertyChanged("UnitType");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="UnitAsset", Namespace="http://schemas.datacontract.org/2004/07/TMS.Core.Data.EntityFramework", IsReference=true)]
    [System.SerializableAttribute()]
    public partial class UnitAsset : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AssetIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private UnitAssetBand[] BandsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ExternalIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SymbolField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Unit UnitField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UnitReferenceField;
        
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
        public string AssetId {
            get {
                return this.AssetIdField;
            }
            set {
                if ((object.ReferenceEquals(this.AssetIdField, value) != true)) {
                    this.AssetIdField = value;
                    this.RaisePropertyChanged("AssetId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public UnitAssetBand[] Bands {
            get {
                return this.BandsField;
            }
            set {
                if ((object.ReferenceEquals(this.BandsField, value) != true)) {
                    this.BandsField = value;
                    this.RaisePropertyChanged("Bands");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ExternalId {
            get {
                return this.ExternalIdField;
            }
            set {
                if ((object.ReferenceEquals(this.ExternalIdField, value) != true)) {
                    this.ExternalIdField = value;
                    this.RaisePropertyChanged("ExternalId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Symbol {
            get {
                return this.SymbolField;
            }
            set {
                if ((object.ReferenceEquals(this.SymbolField, value) != true)) {
                    this.SymbolField = value;
                    this.RaisePropertyChanged("Symbol");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Unit Unit {
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
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UnitReference {
            get {
                return this.UnitReferenceField;
            }
            set {
                if ((object.ReferenceEquals(this.UnitReferenceField, value) != true)) {
                    this.UnitReferenceField = value;
                    this.RaisePropertyChanged("UnitReference");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="UnitProperty", Namespace="http://schemas.datacontract.org/2004/07/TMS.Core.Data.EntityFramework", IsReference=true)]
    [System.SerializableAttribute()]
    public partial class UnitProperty : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<bool> BooleanValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> DateTimeValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<double> DoubleValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StringValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Unit UnitField;
        
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
        public System.Nullable<bool> BooleanValue {
            get {
                return this.BooleanValueField;
            }
            set {
                if ((this.BooleanValueField.Equals(value) != true)) {
                    this.BooleanValueField = value;
                    this.RaisePropertyChanged("BooleanValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> DateTimeValue {
            get {
                return this.DateTimeValueField;
            }
            set {
                if ((this.DateTimeValueField.Equals(value) != true)) {
                    this.DateTimeValueField = value;
                    this.RaisePropertyChanged("DateTimeValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<double> DoubleValue {
            get {
                return this.DoubleValueField;
            }
            set {
                if ((this.DoubleValueField.Equals(value) != true)) {
                    this.DoubleValueField = value;
                    this.RaisePropertyChanged("DoubleValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StringValue {
            get {
                return this.StringValueField;
            }
            set {
                if ((object.ReferenceEquals(this.StringValueField, value) != true)) {
                    this.StringValueField = value;
                    this.RaisePropertyChanged("StringValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Unit Unit {
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
    [System.Runtime.Serialization.DataContractAttribute(Name="Tag", Namespace="http://schemas.datacontract.org/2004/07/TMS.Core.Data.EntityFramework", IsReference=true)]
    [System.SerializableAttribute()]
    public partial class Tag : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<bool> IsDefaultField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Unit[] UnitsField;
        
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
        public System.Nullable<bool> IsDefault {
            get {
                return this.IsDefaultField;
            }
            set {
                if ((this.IsDefaultField.Equals(value) != true)) {
                    this.IsDefaultField = value;
                    this.RaisePropertyChanged("IsDefault");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Unit[] Units {
            get {
                return this.UnitsField;
            }
            set {
                if ((object.ReferenceEquals(this.UnitsField, value) != true)) {
                    this.UnitsField = value;
                    this.RaisePropertyChanged("Units");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="UnitAssetBand", Namespace="http://schemas.datacontract.org/2004/07/TMS.Core.Data.EntityFramework", IsReference=true)]
    [System.SerializableAttribute()]
    public partial class UnitAssetBand : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private UnitAsset AssetField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AssetIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string BandIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ExternalIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SymbolField;
        
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
        public UnitAsset Asset {
            get {
                return this.AssetField;
            }
            set {
                if ((object.ReferenceEquals(this.AssetField, value) != true)) {
                    this.AssetField = value;
                    this.RaisePropertyChanged("Asset");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string AssetId {
            get {
                return this.AssetIdField;
            }
            set {
                if ((object.ReferenceEquals(this.AssetIdField, value) != true)) {
                    this.AssetIdField = value;
                    this.RaisePropertyChanged("AssetId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string BandId {
            get {
                return this.BandIdField;
            }
            set {
                if ((object.ReferenceEquals(this.BandIdField, value) != true)) {
                    this.BandIdField = value;
                    this.RaisePropertyChanged("BandId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ExternalId {
            get {
                return this.ExternalIdField;
            }
            set {
                if ((object.ReferenceEquals(this.ExternalIdField, value) != true)) {
                    this.ExternalIdField = value;
                    this.RaisePropertyChanged("ExternalId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Symbol {
            get {
                return this.SymbolField;
            }
            set {
                if ((object.ReferenceEquals(this.SymbolField, value) != true)) {
                    this.SymbolField = value;
                    this.RaisePropertyChanged("Symbol");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="Activity", Namespace="http://schemas.datacontract.org/2004/07/TMS.Core.Data.EntityFramework", IsReference=true)]
    [System.SerializableAttribute()]
    public partial class Activity : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ActivityNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
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
        public string ActivityName {
            get {
                return this.ActivityNameField;
            }
            set {
                if ((object.ReferenceEquals(this.ActivityNameField, value) != true)) {
                    this.ActivityNameField = value;
                    this.RaisePropertyChanged("ActivityName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="ActivityAssociation", Namespace="http://schemas.datacontract.org/2004/07/TMS.Core.Data.EntityFramework")]
    [System.SerializableAttribute()]
    public partial class ActivityAssociation : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ActivityField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MarketField;
        
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
        public string Activity {
            get {
                return this.ActivityField;
            }
            set {
                if ((object.ReferenceEquals(this.ActivityField, value) != true)) {
                    this.ActivityField = value;
                    this.RaisePropertyChanged("Activity");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Market {
            get {
                return this.MarketField;
            }
            set {
                if ((object.ReferenceEquals(this.MarketField, value) != true)) {
                    this.MarketField = value;
                    this.RaisePropertyChanged("Market");
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice", ConfigurationName="TmsMainServiceServiceReference.ITmsMainService")]
    public interface ITmsMainService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/Ping", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/PingRespon" +
            "se")]
        string Ping(string p);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetActivit" +
            "yGroup", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetActivit" +
            "yGroupResponse")]
        ActivitySerieGroupEntity[] GetActivityGroup(ActivityAssociationEntity key);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetActivit" +
            "yGroupSerie", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetActivit" +
            "yGroupSerieResponse")]
        FlowEntity[] GetActivityGroupSerie(ActivityGroupDetailEntity key, int FrequencyMode, MatchType marketMode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetFlowByD" +
            "ate", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetFlowByD" +
            "ateResponse")]
        FlowEntity GetFlowByDate(FlowEntity key, System.DateTime date);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetFlowByS" +
            "erieAndDate", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetFlowByS" +
            "erieAndDateResponse")]
        FlowEntity GetFlowBySerieAndDate(string serieSource, string serieSymbol, string serieClass, string serieChannel, string serieContext, System.DateTime date);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetFlowByD" +
            "ateTimeInterval", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetFlowByD" +
            "ateTimeIntervalResponse")]
        FlowEntity GetFlowByDateTimeInterval(FlowEntity key, DateTimeUtilityIntervalType dateTimeInterval);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetFlowByS" +
            "erieAndDateTimeInterval", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetFlowByS" +
            "erieAndDateTimeIntervalResponse")]
        FlowEntity GetFlowBySerieAndDateTimeInterval(string serieSource, string serieSymbol, string serieClass, string serieChannel, string serieContext, DateTimeUtilityIntervalType dateTimeInterval);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetRecents" +
            "SeriesDetails", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetRecents" +
            "SeriesDetailsResponse")]
        SerieDetailEntity[] GetRecentsSeriesDetails(SerieEntity serie, int sinceMinutes);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetMessage" +
            "Status", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetMessage" +
            "StatusResponse")]
        string GetMessageStatus(int messageId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetMessage" +
            "Statuses", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetMessage" +
            "StatusesResponse")]
        System.Collections.Generic.Dictionary<int, string> GetMessageStatuses(int[] messageIds);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/PushMessag" +
            "e", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/PushMessag" +
            "eResponse")]
        int PushMessage(UserEntity user, string sFileName, string sOriginCd, TmsBlob oContent, MessageType type);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/PushMessag" +
            "eWithPriority", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/PushMessag" +
            "eWithPriorityResponse")]
        int PushMessageWithPriority(UserEntity user, string sFileName, string sOriginCd, TmsBlob oContent, MessageType type, MessagePriorities priority, System.Nullable<System.DateTime> scheduledFor);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/SaveFlowEn" +
            "tities", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/SaveFlowEn" +
            "titiesResponse")]
        int[] SaveFlowEntities(string userName, string userRole, string WorkingPlant, System.DateTime flowDate, FlowEntity[] flows, System.Collections.Generic.Dictionary<string, object> argsD);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/PushComman" +
            "d", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/PushComman" +
            "dResponse")]
        int PushCommand(string commandType, TmsParameterCollection extraParameters, MessageType type);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/PushGeneri" +
            "cCommand", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/PushGeneri" +
            "cCommandResponse")]
        int PushGenericCommand(string commandType, System.Collections.Generic.Dictionary<string, object> extraParams, MessageType type, MessagePriorities priority, System.Nullable<System.DateTime> scheduledFor);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/CompileMBX" +
            "", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/CompileMBX" +
            "Response")]
        int CompileMBX(UserEntity user, string activityCd, string unitName, DateTimeUtilityIntervalType[] lsDateTimeIntervals, string market);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/PublishFlo" +
            "wEntities", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/PublishFlo" +
            "wEntitiesResponse")]
        int PublishFlowEntities(string commandType, System.DateTime[] lsDates, FrequencyType frequency, int[] lsFlowInts, int[] lsSerieId, System.Collections.Generic.Dictionary<string, object> argsD);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/IsMarketCl" +
            "ose", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/IsMarketCl" +
            "oseResponse")]
        bool IsMarketClose(string sMarket, System.DateTime flowDate);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetSLActiv" +
            "ityGroupSerie", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetSLActiv" +
            "ityGroupSerieResponse")]
        System.Collections.Generic.Dictionary<string, object>[] GetSLActivityGroupSerie(int activityGroupId, DateTimeUtilityIntervalType interval, System.DateTime flowDate);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetAlarmEv" +
            "entSerie", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetAlarmEv" +
            "entSerieResponse")]
        System.Collections.Generic.Dictionary<string, object>[] GetAlarmEventSerie(string activity, string unitCode, DateTimeUtilityIntervalType dateTimeInterval, System.Nullable<int> fromRow, System.Nullable<int> maxRow, string Culture);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetAlarmEv" +
            "entSerieCount", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetAlarmEv" +
            "entSerieCountResponse")]
        int GetAlarmEventSerieCount(string activity, string unitCode, DateTimeUtilityIntervalType dateTimeInterval);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/CalculateP" +
            "V", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/CalculateP" +
            "VResponse")]
        int CalculatePV(UserEntity user, string activityCd, string unitName, DateTimeUtilityIntervalType[] lsDateTimeIntervals, string[] planTypes, bool debugMode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/SaveComman" +
            "dMessage", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/SaveComman" +
            "dMessageResponse")]
        int SaveCommandMessage(UserEntity user, string activityCd, System.Collections.Generic.Dictionary<string, object> argsD);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/Authentica" +
            "teUser", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/Authentica" +
            "teUserResponse")]
        UserEntity AuthenticateUser(string UserName, string Password, string guaranteedUser);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/UpdateUser" +
            "", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/UpdateUser" +
            "Response")]
        bool UpdateUser(UserEntity key);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetUserUni" +
            "ts", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetUserUni" +
            "tsResponse")]
        UnitEntity[] GetUserUnits(UserEntity user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetEnabled" +
            "UserUnits", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetEnabled" +
            "UserUnitsResponse")]
        UnitEntity[] GetEnabledUserUnits(UserEntity user, string enabledForActivity);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetEnabled" +
            "Units", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetEnabled" +
            "UnitsResponse")]
        UnitEntity[] GetEnabledUnits(string enabledForActivity);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetAllSign" +
            "al", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetAllSign" +
            "alResponse")]
        SignalEntity[] GetAllSignal(string activityCD, System.DateTime FlowDate, UnitEntity[] Units);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetSingle", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetSingleR" +
            "esponse")]
        UserEntity GetSingle(UserEntity key);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetMessage" +
            "sIn", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetMessage" +
            "sInResponse")]
        MessageEntity[] GetMessagesIn(UserEntity user, string operatorName, System.Nullable<StatusType> status, System.Nullable<MessageType> type, int fromRow, int toRow, System.Nullable<System.DateTime> dateFrom, System.Nullable<System.DateTime> dateTo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/CountMessa" +
            "geIn", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/CountMessa" +
            "geInResponse")]
        decimal CountMessageIn(string operatorName, System.Nullable<StatusType> status, System.Nullable<MessageType> type, System.Nullable<System.DateTime> creationStDt, System.Nullable<System.DateTime> creationEdDt);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetMessage" +
            "Outs", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetMessage" +
            "OutsResponse")]
        MessageOutEntity[] GetMessageOuts(UserEntity user, MessageOutEntity key, int fromRow, int toRow, System.Nullable<System.DateTime> dateFrom, System.Nullable<System.DateTime> dateTo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/CountMessa" +
            "geOuts", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/CountMessa" +
            "geOutsResponse")]
        decimal CountMessageOuts(MessageOutEntity key, System.Nullable<System.DateTime> creationStDt, System.Nullable<System.DateTime> creationEdDt);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetBlob", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetBlobRes" +
            "ponse")]
        TmsBlob GetBlob(decimal blobId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/UpdateMess" +
            "ageOutDownloadDate", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/UpdateMess" +
            "ageOutDownloadDateResponse")]
        void UpdateMessageOutDownloadDate(int msgId, System.DateTime downloadDate, UserEntity user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/InsertLog", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/InsertLogR" +
            "esponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(InfoConsolle[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(InfoConsolle))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SerieDataItem[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SerieDataItem))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DataItem[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DataItem))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SerieDescription))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SecurityRule))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AccessTypes))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SecurityObject))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SecurityRule[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(BaseEntity))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ContextEntity))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SymbolEntity))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(FormulaEntity))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SerieEntity))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ChannelEntity))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ClassEntity))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(FrequencyEntity))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SourceEntity))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SerieDetailGroupEntity[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SerieDetailGroupEntity))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SerieDetailEntity[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SerieDetailEntity))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StreamEntity))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SerieEntity[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ApplicationType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(BalanceOrderMessageType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ChannelType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(FormulaType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(LanguageType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(LogType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(MarketType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(MessageType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(OtherRUPType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(PasswordStatusType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(PlanType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RoleType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RuleType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SCWOperationType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(StatusType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(TmsCommandTypes))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UpdateType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(VirtualClassDescriptionType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ColumnType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DirectionType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(MatchType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RelevantActivityType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RollupType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(RUPType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SeverityType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SignalColorType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SignalType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SourceType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(EntityDBStatus))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(FrequencyType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(OriginType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UoMType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CalculatePlansDataItem[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CalculatePlansDataItem))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CalculatePlansFasciaItem[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CalculatePlansFasciaItem))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DateTimeUtilityIntervalType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(DateTimeUtilityIntervalType[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(TmsParameterCollection))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GetCalculatePlansDataOptions))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(int[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.Collections.Generic.Dictionary<int, string>))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.Collections.Generic.Dictionary<string, object>))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.DateTime[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.Collections.Generic.Dictionary<string, object>[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(string[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(object[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.Collections.Generic.Dictionary<string, string>))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.Collections.Generic.Dictionary<string, System.Version>))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<DateTimeUtilityIntervalType, System.Nullable<decimal>>>))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.Collections.Generic.Dictionary<DateTimeUtilityIntervalType, System.Nullable<decimal>>))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<System.DateTimeOffset, System.Nullable<decimal>>>))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.Collections.Generic.Dictionary<System.DateTimeOffset, System.Nullable<decimal>>))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.Collections.Generic.Dictionary<string, CalculatePlansFasciaItem[]>))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.Collections.Generic.Dictionary<System.DateTime, object>))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Unit[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Unit))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UnitAsset[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UnitAsset))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UnitAssetBand[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UnitAssetBand))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UnitProperty[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UnitProperty))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Tag[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Tag))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Activity[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Activity))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ActivityAssociation[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ActivityAssociation))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.Version))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.DateTimeOffset))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(TmsBaseRule))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(MarketEnablingRule))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(MessageTypeRule))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ViewRoleRule))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(TmsFileServiceAccessRule))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UnitEnablingRule))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ViewPageRule))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ActivityAssociationEntity))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ActivityEntity))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(MarketEntity))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UnitEntity))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ActivitySerieGroupEntity[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ActivitySerieGroupEntity))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ActivityGroupDetailEntity))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(FlowEntity[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(FlowEntity))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UserEntity))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UnitEntity[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SignalEntity[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SignalEntity))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(MessageEntity[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(MessageEntity))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(MessageOutEntity))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(MessageOutEntity[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(LogEntity))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(UserEntity[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(LogEntity[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(ActivityEntity[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(FmkActivityEntity))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(FmkActivityEntity[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(TmsBlob))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(MessagePriorities))]
        void InsertLog(LogType type, string description, string message, object[] args, LogEntity log);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetUsers", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetUsersRe" +
            "sponse")]
        UserEntity[] GetUsers(UserEntity key);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetLogs", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetLogsRes" +
            "ponse")]
        LogEntity[] GetLogs(LogEntity key, System.Nullable<System.DateTime> creationStDt, System.Nullable<System.DateTime> creationEdDt, System.Nullable<int> fromRow, System.Nullable<int> toRow, bool localizeLog, string cultureString);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetLogMess" +
            "ages", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetLogMess" +
            "agesResponse")]
        LogEntity[] GetLogMessages(System.Nullable<int> severity, System.Nullable<System.DateTime> creationStDt, System.Nullable<System.DateTime> creationEdDt, System.Nullable<int> fromRow, System.Nullable<int> toRow, string containingText, string unitName, string activityName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/CountLogs", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/CountLogsR" +
            "esponse")]
        decimal CountLogs(LogEntity key, System.Nullable<System.DateTime> creationStDt, System.Nullable<System.DateTime> creationEdDt);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetActivit" +
            "ies", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetActivit" +
            "iesResponse")]
        ActivityEntity[] GetActivities();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetFmkActi" +
            "vities", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetFmkActi" +
            "vitiesResponse")]
        FmkActivityEntity[] GetFmkActivities(FmkActivityEntity key, System.Nullable<int> rownum);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetFmkPend" +
            "ingActivities", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetFmkPend" +
            "ingActivitiesResponse")]
        FmkActivityEntity[] GetFmkPendingActivities(FmkActivityEntity key, System.Nullable<int> rownum);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetDefault" +
            "ColumnType", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetDefault" +
            "ColumnTypeResponse")]
        ColumnType GetDefaultColumnType(SerieEntity key);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetFlowsAv" +
            "ailability", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetFlowsAv" +
            "ailabilityResponse")]
        FlowEntity[] GetFlowsAvailability(FlowEntity key, System.DateTime flowDate, RollupType rollupType);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetDBServe" +
            "rDate", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetDBServe" +
            "rDateResponse")]
        System.DateTime GetDBServerDate();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetSymbols" +
            "NameDescription", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetSymbols" +
            "NameDescriptionResponse")]
        System.Collections.Generic.Dictionary<string, string> GetSymbolsNameDescription(UnitEntity ue);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/SelectVers" +
            "ionedFlows", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/SelectVers" +
            "ionedFlowsResponse")]
        FlowEntity[] SelectVersionedFlows(FlowEntity[] lsFlows, System.Nullable<bool> versionGreaterThan, System.DateTime flowDate);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/PushActivi" +
            "tyFormulaCommands", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/PushActivi" +
            "tyFormulaCommandsResponse")]
        int PushActivityFormulaCommands(UserEntity user, string[] lsActivityCd, string unitName, DateTimeUtilityIntervalType[] lsDateTimeIntervals, string commandType, System.Collections.Generic.Dictionary<string, object> extraParameters);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/PushFormul" +
            "aCommands", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/PushFormul" +
            "aCommandsResponse")]
        int PushFormulaCommands(UserEntity user, string activityCd, string unitName, DateTimeUtilityIntervalType[] lsDateTimeIntervals, TmsParameterCollection extraParameters, string optionalCommandType);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/LoadCurveE" +
            "ntity", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/LoadCurveE" +
            "ntityResponse")]
        SerieDetailGroupEntity[] LoadCurveEntity(SerieEntity[] lsSerie, System.DateTime FlowDate);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/SaveCurveE" +
            "ntity", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/SaveCurveE" +
            "ntityResponse")]
        int SaveCurveEntity(SerieDetailGroupEntity[] lsSerieDetailGroupEntity, string userName, string userRole, string WorkingPlant, System.DateTime flowDate);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetVersion" +
            "s", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetVersion" +
            "sResponse")]
        System.Collections.Generic.Dictionary<string, System.Version> GetVersions();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetInfoCon" +
            "solles", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetInfoCon" +
            "sollesResponse")]
        InfoConsolle[] GetInfoConsolles(string[] lsUnits, System.DateTime flowDate);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetCalcula" +
            "tePlansData", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetCalcula" +
            "tePlansDataResponse")]
        CalculatePlansDataItem[] GetCalculatePlansData(string[] lsUnits, System.DateTime flowDate);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetCalcula" +
            "tePlansDataExtended", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetCalcula" +
            "tePlansDataExtendedResponse")]
        CalculatePlansDataItem[] GetCalculatePlansDataExtended(string[] lsUnits, System.DateTime flowDate, GetCalculatePlansDataOptions options);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetSeries", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetSeriesR" +
            "esponse")]
        SerieEntity[] GetSeries(string serieSource, string serieSymbol, string serieClass, string serieChannel, string serieContext);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetSerieDa" +
            "ta", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetSerieDa" +
            "taResponse")]
        SerieDataItem[] GetSerieData(string serieSource, string serieSymbol, string serieClass, string serieChannel, string serieContext, System.DateTime dateFrom, System.DateTime dateTo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/SetSerieDa" +
            "ta", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/SetSerieDa" +
            "taResponse")]
        void SetSerieData(SerieDataItem[] serieDataItems);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/DeleteSeri" +
            "eData", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/DeleteSeri" +
            "eDataResponse")]
        void DeleteSerieData(string serieSource, string serieSymbol, string serieClass, string serieChannel, string serieContext, System.DateTime dateFrom, System.DateTime dateTo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/SetSemapho" +
            "re", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/SetSemapho" +
            "reResponse")]
        void SetSemaphore(string serieSource, string serieSymbol, string serieClass, string serieChannel, string serieContext, System.DateTime date, string semaphoreValue, string semaphoreNotes);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetUnits", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetUnitsRe" +
            "sponse")]
        Unit[] GetUnits(string filter);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetUnit", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetUnitRes" +
            "ponse")]
        Unit GetUnit(string unitName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/AddUnit", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/AddUnitRes" +
            "ponse")]
        bool AddUnit(out string[] messages, string unitName, string unitType, string description, string externalId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/EditUnit", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/EditUnitRe" +
            "sponse")]
        bool EditUnit(out string[] messages, string unitName, string unitType, string description, string externalId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/DeleteUnit" +
            "", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/DeleteUnit" +
            "Response")]
        bool DeleteUnit(out string[] messages, string unitName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/AddAsset", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/AddAssetRe" +
            "sponse")]
        bool AddAsset(out string[] messages, string unitName, string assetId, string description, string externalId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/EditAsset", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/EditAssetR" +
            "esponse")]
        bool EditAsset(out string[] messages, string unitName, string assetId, string description, string externalId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/DeleteAsse" +
            "t", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/DeleteAsse" +
            "tResponse")]
        bool DeleteAsset(out string[] messages, string unitName, string assetId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/AddBand", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/AddBandRes" +
            "ponse")]
        bool AddBand(out string[] messages, string unitName, string assetId, string bandId, string description, string externalId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/EditBand", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/EditBandRe" +
            "sponse")]
        bool EditBand(out string[] messages, string unitName, string assetId, string bandId, string description, string externalId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/DeleteBand" +
            "", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/DeleteBand" +
            "Response")]
        bool DeleteBand(out string[] messages, string unitName, string assetId, string bandId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/SetUnitTag" +
            "s", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/SetUnitTag" +
            "sResponse")]
        bool SetUnitTags(string unitName, string[] tags);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetTags", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetTagsRes" +
            "ponse")]
        Tag[] GetTags(string filter);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/AddNewTag", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/AddNewTagR" +
            "esponse")]
        Tag AddNewTag(string tagName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/AddTagUnit" +
            "s", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/AddTagUnit" +
            "sResponse")]
        Tag AddTagUnits(string tagName, string[] units);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/RemoveTagU" +
            "nits", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/RemoveTagU" +
            "nitsResponse")]
        Tag RemoveTagUnits(string tagName, string[] units);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/DeleteTag", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/DeleteTagR" +
            "esponse")]
        bool DeleteTag(string tagName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/SetDefault" +
            "Tag", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/SetDefault" +
            "TagResponse")]
        bool SetDefaultTag(string tagName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/SetUnitPro" +
            "perties", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/SetUnitPro" +
            "pertiesResponse")]
        bool SetUnitProperties(string unitName, System.Collections.Generic.Dictionary<string, object> properties);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetAllActi" +
            "vities", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetAllActi" +
            "vitiesResponse")]
        Activity[] GetAllActivities();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/SetActivit" +
            "yAssociation", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/SetActivit" +
            "yAssociationResponse")]
        bool SetActivityAssociation(string activityName, string unitName, bool enable);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetActivit" +
            "yAssociations", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/GetActivit" +
            "yAssociationsResponse")]
        ActivityAssociation[] GetActivityAssociations(string unitName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/PushUnitSe" +
            "riesReconfiguration", ReplyAction="http://schemas.elsagdatamat.com/xml/tms/tmsmainservice/ITmsMainService/PushUnitSe" +
            "riesReconfigurationResponse")]
        int PushUnitSeriesReconfiguration(UserEntity user, string unitName);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITmsMainServiceChannel : ITmsMainService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TmsMainServiceClient : System.ServiceModel.ClientBase<ITmsMainService>, ITmsMainService {
        
        public TmsMainServiceClient() {
        }
        
        public TmsMainServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TmsMainServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TmsMainServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TmsMainServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string Ping(string p) {
            return base.Channel.Ping(p);
        }
        
        public ActivitySerieGroupEntity[] GetActivityGroup(ActivityAssociationEntity key) {
            return base.Channel.GetActivityGroup(key);
        }
        
        public FlowEntity[] GetActivityGroupSerie(ActivityGroupDetailEntity key, int FrequencyMode, MatchType marketMode) {
            return base.Channel.GetActivityGroupSerie(key, FrequencyMode, marketMode);
        }
        
        public FlowEntity GetFlowByDate(FlowEntity key, System.DateTime date) {
            return base.Channel.GetFlowByDate(key, date);
        }
        
        public FlowEntity GetFlowBySerieAndDate(string serieSource, string serieSymbol, string serieClass, string serieChannel, string serieContext, System.DateTime date) {
            return base.Channel.GetFlowBySerieAndDate(serieSource, serieSymbol, serieClass, serieChannel, serieContext, date);
        }
        
        public FlowEntity GetFlowByDateTimeInterval(FlowEntity key, DateTimeUtilityIntervalType dateTimeInterval) {
            return base.Channel.GetFlowByDateTimeInterval(key, dateTimeInterval);
        }
        
        public FlowEntity GetFlowBySerieAndDateTimeInterval(string serieSource, string serieSymbol, string serieClass, string serieChannel, string serieContext, DateTimeUtilityIntervalType dateTimeInterval) {
            return base.Channel.GetFlowBySerieAndDateTimeInterval(serieSource, serieSymbol, serieClass, serieChannel, serieContext, dateTimeInterval);
        }
        
        public SerieDetailEntity[] GetRecentsSeriesDetails(SerieEntity serie, int sinceMinutes) {
            return base.Channel.GetRecentsSeriesDetails(serie, sinceMinutes);
        }
        
        public string GetMessageStatus(int messageId) {
            return base.Channel.GetMessageStatus(messageId);
        }
        
        public System.Collections.Generic.Dictionary<int, string> GetMessageStatuses(int[] messageIds) {
            return base.Channel.GetMessageStatuses(messageIds);
        }
        
        public int PushMessage(UserEntity user, string sFileName, string sOriginCd, TmsBlob oContent, MessageType type) {
            return base.Channel.PushMessage(user, sFileName, sOriginCd, oContent, type);
        }
        
        public int PushMessageWithPriority(UserEntity user, string sFileName, string sOriginCd, TmsBlob oContent, MessageType type, MessagePriorities priority, System.Nullable<System.DateTime> scheduledFor) {
            return base.Channel.PushMessageWithPriority(user, sFileName, sOriginCd, oContent, type, priority, scheduledFor);
        }
        
        public int[] SaveFlowEntities(string userName, string userRole, string WorkingPlant, System.DateTime flowDate, FlowEntity[] flows, System.Collections.Generic.Dictionary<string, object> argsD) {
            return base.Channel.SaveFlowEntities(userName, userRole, WorkingPlant, flowDate, flows, argsD);
        }
        
        public int PushCommand(string commandType, TmsParameterCollection extraParameters, MessageType type) {
            return base.Channel.PushCommand(commandType, extraParameters, type);
        }
        
        public int PushGenericCommand(string commandType, System.Collections.Generic.Dictionary<string, object> extraParams, MessageType type, MessagePriorities priority, System.Nullable<System.DateTime> scheduledFor) {
            return base.Channel.PushGenericCommand(commandType, extraParams, type, priority, scheduledFor);
        }
        
        public int CompileMBX(UserEntity user, string activityCd, string unitName, DateTimeUtilityIntervalType[] lsDateTimeIntervals, string market) {
            return base.Channel.CompileMBX(user, activityCd, unitName, lsDateTimeIntervals, market);
        }
        
        public int PublishFlowEntities(string commandType, System.DateTime[] lsDates, FrequencyType frequency, int[] lsFlowInts, int[] lsSerieId, System.Collections.Generic.Dictionary<string, object> argsD) {
            return base.Channel.PublishFlowEntities(commandType, lsDates, frequency, lsFlowInts, lsSerieId, argsD);
        }
        
        public bool IsMarketClose(string sMarket, System.DateTime flowDate) {
            return base.Channel.IsMarketClose(sMarket, flowDate);
        }
        
        public System.Collections.Generic.Dictionary<string, object>[] GetSLActivityGroupSerie(int activityGroupId, DateTimeUtilityIntervalType interval, System.DateTime flowDate) {
            return base.Channel.GetSLActivityGroupSerie(activityGroupId, interval, flowDate);
        }
        
        public System.Collections.Generic.Dictionary<string, object>[] GetAlarmEventSerie(string activity, string unitCode, DateTimeUtilityIntervalType dateTimeInterval, System.Nullable<int> fromRow, System.Nullable<int> maxRow, string Culture) {
            return base.Channel.GetAlarmEventSerie(activity, unitCode, dateTimeInterval, fromRow, maxRow, Culture);
        }
        
        public int GetAlarmEventSerieCount(string activity, string unitCode, DateTimeUtilityIntervalType dateTimeInterval) {
            return base.Channel.GetAlarmEventSerieCount(activity, unitCode, dateTimeInterval);
        }
        
        public int CalculatePV(UserEntity user, string activityCd, string unitName, DateTimeUtilityIntervalType[] lsDateTimeIntervals, string[] planTypes, bool debugMode) {
            return base.Channel.CalculatePV(user, activityCd, unitName, lsDateTimeIntervals, planTypes, debugMode);
        }
        
        public int SaveCommandMessage(UserEntity user, string activityCd, System.Collections.Generic.Dictionary<string, object> argsD) {
            return base.Channel.SaveCommandMessage(user, activityCd, argsD);
        }
        
        public UserEntity AuthenticateUser(string UserName, string Password, string guaranteedUser) {
            return base.Channel.AuthenticateUser(UserName, Password, guaranteedUser);
        }
        
        public bool UpdateUser(UserEntity key) {
            return base.Channel.UpdateUser(key);
        }
        
        public UnitEntity[] GetUserUnits(UserEntity user) {
            return base.Channel.GetUserUnits(user);
        }
        
        public UnitEntity[] GetEnabledUserUnits(UserEntity user, string enabledForActivity) {
            return base.Channel.GetEnabledUserUnits(user, enabledForActivity);
        }
        
        public UnitEntity[] GetEnabledUnits(string enabledForActivity) {
            return base.Channel.GetEnabledUnits(enabledForActivity);
        }
        
        public SignalEntity[] GetAllSignal(string activityCD, System.DateTime FlowDate, UnitEntity[] Units) {
            return base.Channel.GetAllSignal(activityCD, FlowDate, Units);
        }
        
        public UserEntity GetSingle(UserEntity key) {
            return base.Channel.GetSingle(key);
        }
        
        public MessageEntity[] GetMessagesIn(UserEntity user, string operatorName, System.Nullable<StatusType> status, System.Nullable<MessageType> type, int fromRow, int toRow, System.Nullable<System.DateTime> dateFrom, System.Nullable<System.DateTime> dateTo) {
            return base.Channel.GetMessagesIn(user, operatorName, status, type, fromRow, toRow, dateFrom, dateTo);
        }
        
        public decimal CountMessageIn(string operatorName, System.Nullable<StatusType> status, System.Nullable<MessageType> type, System.Nullable<System.DateTime> creationStDt, System.Nullable<System.DateTime> creationEdDt) {
            return base.Channel.CountMessageIn(operatorName, status, type, creationStDt, creationEdDt);
        }
        
        public MessageOutEntity[] GetMessageOuts(UserEntity user, MessageOutEntity key, int fromRow, int toRow, System.Nullable<System.DateTime> dateFrom, System.Nullable<System.DateTime> dateTo) {
            return base.Channel.GetMessageOuts(user, key, fromRow, toRow, dateFrom, dateTo);
        }
        
        public decimal CountMessageOuts(MessageOutEntity key, System.Nullable<System.DateTime> creationStDt, System.Nullable<System.DateTime> creationEdDt) {
            return base.Channel.CountMessageOuts(key, creationStDt, creationEdDt);
        }
        
        public TmsBlob GetBlob(decimal blobId) {
            return base.Channel.GetBlob(blobId);
        }
        
        public void UpdateMessageOutDownloadDate(int msgId, System.DateTime downloadDate, UserEntity user) {
            base.Channel.UpdateMessageOutDownloadDate(msgId, downloadDate, user);
        }
        
        public void InsertLog(LogType type, string description, string message, object[] args, LogEntity log) {
            base.Channel.InsertLog(type, description, message, args, log);
        }
        
        public UserEntity[] GetUsers(UserEntity key) {
            return base.Channel.GetUsers(key);
        }
        
        public LogEntity[] GetLogs(LogEntity key, System.Nullable<System.DateTime> creationStDt, System.Nullable<System.DateTime> creationEdDt, System.Nullable<int> fromRow, System.Nullable<int> toRow, bool localizeLog, string cultureString) {
            return base.Channel.GetLogs(key, creationStDt, creationEdDt, fromRow, toRow, localizeLog, cultureString);
        }
        
        public LogEntity[] GetLogMessages(System.Nullable<int> severity, System.Nullable<System.DateTime> creationStDt, System.Nullable<System.DateTime> creationEdDt, System.Nullable<int> fromRow, System.Nullable<int> toRow, string containingText, string unitName, string activityName) {
            return base.Channel.GetLogMessages(severity, creationStDt, creationEdDt, fromRow, toRow, containingText, unitName, activityName);
        }
        
        public decimal CountLogs(LogEntity key, System.Nullable<System.DateTime> creationStDt, System.Nullable<System.DateTime> creationEdDt) {
            return base.Channel.CountLogs(key, creationStDt, creationEdDt);
        }
        
        public ActivityEntity[] GetActivities() {
            return base.Channel.GetActivities();
        }
        
        public FmkActivityEntity[] GetFmkActivities(FmkActivityEntity key, System.Nullable<int> rownum) {
            return base.Channel.GetFmkActivities(key, rownum);
        }
        
        public FmkActivityEntity[] GetFmkPendingActivities(FmkActivityEntity key, System.Nullable<int> rownum) {
            return base.Channel.GetFmkPendingActivities(key, rownum);
        }
        
        public ColumnType GetDefaultColumnType(SerieEntity key) {
            return base.Channel.GetDefaultColumnType(key);
        }
        
        public FlowEntity[] GetFlowsAvailability(FlowEntity key, System.DateTime flowDate, RollupType rollupType) {
            return base.Channel.GetFlowsAvailability(key, flowDate, rollupType);
        }
        
        public System.DateTime GetDBServerDate() {
            return base.Channel.GetDBServerDate();
        }
        
        public System.Collections.Generic.Dictionary<string, string> GetSymbolsNameDescription(UnitEntity ue) {
            return base.Channel.GetSymbolsNameDescription(ue);
        }
        
        public FlowEntity[] SelectVersionedFlows(FlowEntity[] lsFlows, System.Nullable<bool> versionGreaterThan, System.DateTime flowDate) {
            return base.Channel.SelectVersionedFlows(lsFlows, versionGreaterThan, flowDate);
        }
        
        public int PushActivityFormulaCommands(UserEntity user, string[] lsActivityCd, string unitName, DateTimeUtilityIntervalType[] lsDateTimeIntervals, string commandType, System.Collections.Generic.Dictionary<string, object> extraParameters) {
            return base.Channel.PushActivityFormulaCommands(user, lsActivityCd, unitName, lsDateTimeIntervals, commandType, extraParameters);
        }
        
        public int PushFormulaCommands(UserEntity user, string activityCd, string unitName, DateTimeUtilityIntervalType[] lsDateTimeIntervals, TmsParameterCollection extraParameters, string optionalCommandType) {
            return base.Channel.PushFormulaCommands(user, activityCd, unitName, lsDateTimeIntervals, extraParameters, optionalCommandType);
        }
        
        public SerieDetailGroupEntity[] LoadCurveEntity(SerieEntity[] lsSerie, System.DateTime FlowDate) {
            return base.Channel.LoadCurveEntity(lsSerie, FlowDate);
        }
        
        public int SaveCurveEntity(SerieDetailGroupEntity[] lsSerieDetailGroupEntity, string userName, string userRole, string WorkingPlant, System.DateTime flowDate) {
            return base.Channel.SaveCurveEntity(lsSerieDetailGroupEntity, userName, userRole, WorkingPlant, flowDate);
        }
        
        public System.Collections.Generic.Dictionary<string, System.Version> GetVersions() {
            return base.Channel.GetVersions();
        }
        
        public InfoConsolle[] GetInfoConsolles(string[] lsUnits, System.DateTime flowDate) {
            return base.Channel.GetInfoConsolles(lsUnits, flowDate);
        }
        
        public CalculatePlansDataItem[] GetCalculatePlansData(string[] lsUnits, System.DateTime flowDate) {
            return base.Channel.GetCalculatePlansData(lsUnits, flowDate);
        }
        
        public CalculatePlansDataItem[] GetCalculatePlansDataExtended(string[] lsUnits, System.DateTime flowDate, GetCalculatePlansDataOptions options) {
            return base.Channel.GetCalculatePlansDataExtended(lsUnits, flowDate, options);
        }
        
        public SerieEntity[] GetSeries(string serieSource, string serieSymbol, string serieClass, string serieChannel, string serieContext) {
            return base.Channel.GetSeries(serieSource, serieSymbol, serieClass, serieChannel, serieContext);
        }
        
        public SerieDataItem[] GetSerieData(string serieSource, string serieSymbol, string serieClass, string serieChannel, string serieContext, System.DateTime dateFrom, System.DateTime dateTo) {
            return base.Channel.GetSerieData(serieSource, serieSymbol, serieClass, serieChannel, serieContext, dateFrom, dateTo);
        }
        
        public void SetSerieData(SerieDataItem[] serieDataItems) {
            base.Channel.SetSerieData(serieDataItems);
        }
        
        public void DeleteSerieData(string serieSource, string serieSymbol, string serieClass, string serieChannel, string serieContext, System.DateTime dateFrom, System.DateTime dateTo) {
            base.Channel.DeleteSerieData(serieSource, serieSymbol, serieClass, serieChannel, serieContext, dateFrom, dateTo);
        }
        
        public void SetSemaphore(string serieSource, string serieSymbol, string serieClass, string serieChannel, string serieContext, System.DateTime date, string semaphoreValue, string semaphoreNotes) {
            base.Channel.SetSemaphore(serieSource, serieSymbol, serieClass, serieChannel, serieContext, date, semaphoreValue, semaphoreNotes);
        }
        
        public Unit[] GetUnits(string filter) {
            return base.Channel.GetUnits(filter);
        }
        
        public Unit GetUnit(string unitName) {
            return base.Channel.GetUnit(unitName);
        }
        
        public bool AddUnit(out string[] messages, string unitName, string unitType, string description, string externalId) {
            return base.Channel.AddUnit(out messages, unitName, unitType, description, externalId);
        }
        
        public bool EditUnit(out string[] messages, string unitName, string unitType, string description, string externalId) {
            return base.Channel.EditUnit(out messages, unitName, unitType, description, externalId);
        }
        
        public bool DeleteUnit(out string[] messages, string unitName) {
            return base.Channel.DeleteUnit(out messages, unitName);
        }
        
        public bool AddAsset(out string[] messages, string unitName, string assetId, string description, string externalId) {
            return base.Channel.AddAsset(out messages, unitName, assetId, description, externalId);
        }
        
        public bool EditAsset(out string[] messages, string unitName, string assetId, string description, string externalId) {
            return base.Channel.EditAsset(out messages, unitName, assetId, description, externalId);
        }
        
        public bool DeleteAsset(out string[] messages, string unitName, string assetId) {
            return base.Channel.DeleteAsset(out messages, unitName, assetId);
        }
        
        public bool AddBand(out string[] messages, string unitName, string assetId, string bandId, string description, string externalId) {
            return base.Channel.AddBand(out messages, unitName, assetId, bandId, description, externalId);
        }
        
        public bool EditBand(out string[] messages, string unitName, string assetId, string bandId, string description, string externalId) {
            return base.Channel.EditBand(out messages, unitName, assetId, bandId, description, externalId);
        }
        
        public bool DeleteBand(out string[] messages, string unitName, string assetId, string bandId) {
            return base.Channel.DeleteBand(out messages, unitName, assetId, bandId);
        }
        
        public bool SetUnitTags(string unitName, string[] tags) {
            return base.Channel.SetUnitTags(unitName, tags);
        }
        
        public Tag[] GetTags(string filter) {
            return base.Channel.GetTags(filter);
        }
        
        public Tag AddNewTag(string tagName) {
            return base.Channel.AddNewTag(tagName);
        }
        
        public Tag AddTagUnits(string tagName, string[] units) {
            return base.Channel.AddTagUnits(tagName, units);
        }
        
        public Tag RemoveTagUnits(string tagName, string[] units) {
            return base.Channel.RemoveTagUnits(tagName, units);
        }
        
        public bool DeleteTag(string tagName) {
            return base.Channel.DeleteTag(tagName);
        }
        
        public bool SetDefaultTag(string tagName) {
            return base.Channel.SetDefaultTag(tagName);
        }
        
        public bool SetUnitProperties(string unitName, System.Collections.Generic.Dictionary<string, object> properties) {
            return base.Channel.SetUnitProperties(unitName, properties);
        }
        
        public Activity[] GetAllActivities() {
            return base.Channel.GetAllActivities();
        }
        
        public bool SetActivityAssociation(string activityName, string unitName, bool enable) {
            return base.Channel.SetActivityAssociation(activityName, unitName, enable);
        }
        
        public ActivityAssociation[] GetActivityAssociations(string unitName) {
            return base.Channel.GetActivityAssociations(unitName);
        }
        
        public int PushUnitSeriesReconfiguration(UserEntity user, string unitName) {
            return base.Channel.PushUnitSeriesReconfiguration(user, unitName);
        }
    }
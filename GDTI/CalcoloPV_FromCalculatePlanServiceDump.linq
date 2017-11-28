<Query Kind="Program">
  <Output>DataGrids</Output>
  <Reference>C:\Work\TMS\DEV\TMS\Common\Energy.RealTime.Core\bin\x64\Debug\Energy.RealTime.Core.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationCore.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Serialization.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceModel.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>C:\Work\TMS\DEV\TMS\Common\TMS.Common\bin\x64\Debug\TMS.Common.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <NuGetReference Prerelease="true">OxyPlot.Wpf</NuGetReference>
  <Namespace>Energy.RealTime.Core</Namespace>
  <Namespace>Energy.RealTime.Core.Adapters</Namespace>
  <Namespace>Energy.RealTime.Core.Types</Namespace>
  <Namespace>Energy.RealTime.Core.Types.Messages</Namespace>
  <Namespace>Energy.RealTime.Core.Types.Plans</Namespace>
  <Namespace>Energy.RealTime.Core.Utils</Namespace>
  <Namespace>OxyPlot</Namespace>
  <Namespace>OxyPlot.Wpf</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.ServiceModel</Namespace>
  <Namespace>System.Windows</Namespace>
  <Namespace>System.Windows.Controls</Namespace>
  <Namespace>System.Windows.Input</Namespace>
  <Namespace>TMS.Common.Utils.Serialization</Namespace>
</Query>

void Main()
{
	
	string endpointAddress = "net.tcp://win2008r2test1:8734/CalculatePlans/"; //local
	//string endpointAddress = "net.tcp://srvegt01.master.local:8734/CalculatePlans/"; //iren test
	//string endpointAddress = "net.tcp://srvegt02.master.local:18734/CalculatePlans/"; //iren prod
	
	string unitName = "UP_VADOTERM_5";
	DateTime flowDate = DateTime.Parse("09/09/2017");

	/////////////////////////////////////////////////////////////////////////////////////////////////////////////
	Console.Write("Getting dump data from service.... ");
	byte[] buffer = GetDumpBufferFromService(endpointAddress, unitName, flowDate);
	Console.WriteLine("OK!");

	Console.Write("Deserializing dump data.... ");
	PVMC p = BinarySerialization.Deserialize<PVMC>(buffer);
	Console.WriteLine("OK!");
	
	p.Unit.Dump("Unit");
	p.FlowDate.Dump("FlowDate");
	
	///////////////////////
	p.Debug = false;
	///////////////////////

//	p.CommandsToIgnore.Add("0004131740");
//	
//	p.CommandsToDebug.Add("0004184104");
	//p.StopAt("23:57:00");
	
	//////////////////////////////////////////////
	// Dump Messaggi
	//////////////////////////////////////////////
	p.Commands
	.Where(x => x is BalanceOrderMessage)
	.Cast<BalanceOrderMessage>()
	.Select(x => new
	{
		x.FileName,
		x.Unit,
		Start = x.StartDate.Value,
		End = x.EndDate.Value,
		x.ContinuationType,
		x.PVPowerDelta_TINI,
		x.PVPowerDelta_TFIN,
		LinkOrder = x.IsLinkOrder
	})
	.Dump("Ordini Bilanciamento");
	
	//////////////////////////////////////////////
	// Event handlers
	//////////////////////////////////////////////
	//
	p.BeforeExecutingCommand += (o,e) => { Console.WriteLine ("BDE Start: ID={0} From={1} To={2}", e.Message.ID, e.Message.StartDate, e.Message.EndDate); };
	p.AfterExecutingCommand += (o,e) => { Console.WriteLine ("BDE End: ID={0} Status={1}", e.Message.ID, e.Message.Status); };
	p.SetupChanged += (o,e) => { Console.WriteLine ("SetupChanged: TS={0} From={1} To={2}", e.StartTS, e.From, e.To); };
	p.RampViolation += (o,e) => { Console.WriteLine ("Rampa violata! TS={0}, Requested={1}, Max={2}", e.TS.ToString(), e.RequestedRamp, e.MaxRamp); };
	p.CompletionTimeViolation += (o,e) => { Console.WriteLine ("Violato tempo di esecuzione! MsgId={0}, Expected={1}, Actual={2}", e.Message.ID, e.ExpectedCompletionTime, e.ActualCompletionTime); };
	p.CommandIgnored += (o,e) => { Console.WriteLine ("Messaggio ignorato. MsgId={0}, Reason={1}", e.Message.ID, e.Reason); };
	p.GenericLog += (o, e) => { Console.WriteLine("LOG: ts={0}. \"{1}\"", e.TS, e.Message); };
	//
	
	//////////////////////////////////////////////
	// CALCULATION!!
	//////////////////////////////////////////////
	//
	Console.WriteLine ("Calculation in progress!");
	p.InitWorkingRUP();
	p.Calculate();
	Console.WriteLine ("Calculation finished!");
	
	//////////////////////////////////////////////
	// Esito Messaggi
	//////////////////////////////////////////////
	//
	p.Commands
	.OrderBy(x => x.ID).Select(x => new { ID = x.ID, Status = x.Status, Type = x.GetType().Name, Detail = x.ToString(), Obj = x }).Dump("Esito BDE");
	//
	
	//////////////////////////////////////////////
	// Risultato
	//////////////////////////////////////////////
	
	p.Plan
	.Join(p.OriginalPlan, x => x.TS, x => x.TS, (x,y) => new { TS = x.TS, PV = y.Val, PianoCalcolato = x.Val })
	.Dump("Risultato");
	

	
	p.PreviousWorkingRUP.Dump("Previous Working RUP");
	p.CurrentWorkingRUP.Dump("Current Working RUP");
	p.FollowingWorkingRUP.Dump("Following Working RUP");
	//p.RegulationSignal.Dump("Regulation Signal");
	p.Unavailabilities.Dump("Unavailabilities");
	p.CountPlanSetupChanges().Dump("Setup Changes");
	
	p.Dump("Raw Object");
	
	var pm = FillPlotModel(p);
	
	Show(pm);
}

byte[] GetDumpBufferFromService(string endpointAddress, string unitName, DateTime flowDate, string planType = "PVMC")
{
	NetTcpBinding myNetTcpBinding = new NetTcpBinding() {
		MaxReceivedMessageSize = 1024 * 64 * 100,
		ReceiveTimeout = TimeSpan.FromMinutes(5),
		SendTimeout = TimeSpan.FromMinutes(5),
		Security = new NetTcpSecurity { Mode = SecurityMode.None }
	};

	ChannelFactory<ICalculatePlansService> myNetTcpChannelFactory = new ChannelFactory<ICalculatePlansService>(myNetTcpBinding, new EndpointAddress(endpointAddress));

	ICalculatePlansService px = myNetTcpChannelFactory.CreateChannel();
	return px.GetPlanCalculationDump(unitName, flowDate, planType);
}

void ExportImages(PlotModel pm, string fileName)
{
	using(var stream = new FileStream(fileName + ".DUMP.png", FileMode.OpenOrCreate, FileAccess.Write))
	{
		PngExporter png = new PngExporter();
		png.Width = 1600;
		png.Height = 1200;
		png.Export(pm, stream);
	}
}

PlotModel FillPlotModel(PVMC pvmc)
{
	var pm = new PlotModel();
	pm.Title = string.Format("{0} {1}", pvmc.Unit.Name, pvmc.FlowDate.ToShortDateString());
	
	// Axes
	pm.Axes.Clear();
	pm.Axes.Add(new OxyPlot.Axes.DateTimeAxis()
	{
		Position = OxyPlot.Axes.AxisPosition.Bottom,
		Key = "Time",
		StringFormat = "HH:mm",
		MajorGridlineStyle = LineStyle.Solid,
		MinorGridlineStyle = LineStyle.Dot,
		MajorGridlineColor = OxyColors.LightGray,
		MinorGridlineColor = OxyColors.LightGray,
	});
	
	pm.Axes.Add(new OxyPlot.Axes.LinearAxis()
	{
		Position = OxyPlot.Axes.AxisPosition.Left,
		Key = "Plans",
		Minimum = 0,
		MaximumPadding = 0.1,
		MinimumPadding = 0.1,
		MajorGridlineStyle = LineStyle.Solid,
		MinorGridlineStyle = LineStyle.Dot,
		MajorGridlineColor = OxyColors.LightGray,
		MinorGridlineColor = OxyColors.LightGray
	});
	
	// RUP
	var lsDynRUP = pvmc.OriginalDynamicRUP.Data.SelectMany(rd => rd.Assetti.AvailableOnly().Select(a => new { rd.TS, a.Id, a.PMin, a.PMax } )).GroupBy(x => x.Id);
	var lsStaticRUP = GetAllDayMinutes(pvmc.FlowDate).SelectMany(rd => pvmc.OriginalStaticRUP.Assetti.AvailableOnly().Select(a => new { TS = rd.TimeOfDay, a.Id, a.PMin, a.PMax })).GroupBy(x => x.Id);
	var lsOriginalRUP = lsDynRUP.Any() ? lsDynRUP : lsStaticRUP;
	var lsWorkingRUP = pvmc.CurrentWorkingRUP.Data.SelectMany(rd => rd.Assetti.AvailableOnly().Select(a => new { rd.TS, a.Id, a.PMin, a.PMax } )).GroupBy(x => x.Id);
	var joinedRUP = lsOriginalRUP.Join(lsWorkingRUP, o => o.Key, i => i.Key, (o, i) => new { Original = o, Working = i });
	
	foreach(var grp in joinedRUP)
	{
		var s_rup = new OxyPlot.Series.AreaSeries();
		s_rup.MarkerFill = OxyColors.Transparent;
		s_rup.StrokeThickness = 1;
		s_rup.DataFieldY = "PSMAX";
		s_rup.DataFieldY2 = "PSMIN";
		s_rup.Title = grp.Original.Key;
		s_rup.YAxisKey = "Plans";
		
		var s_wrup = new OxyPlot.Series.AreaSeries();
		s_wrup.Color = OxyColors.Transparent;
		s_wrup.Color2 = OxyColors.Transparent;
		//s_wrup.Fill = OxyColors.Transparent;
		s_wrup.LineStyle = LineStyle.Dot;
		s_wrup.StrokeThickness = 1;
		s_wrup.DataFieldY = "PSMAX";
		s_wrup.DataFieldY2 = "PSMIN";
		s_wrup.Title = grp.Working.Key + " (SB)";
		s_wrup.YAxisKey = "Plans";
		
		// Original RUP
		foreach (var pt in grp.Original.OrderBy(x => x.TS))
		{
			var d = pvmc.FlowDate + pt.TS;
			s_rup.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(d), Convert.ToDouble(pt.PMin)));
			s_rup.Points2.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(d), Convert.ToDouble(pt.PMax)));
			s_rup.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(d.AddMinutes(1)), Convert.ToDouble(pt.PMin)));
			s_rup.Points2.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(d.AddMinutes(1)), Convert.ToDouble(pt.PMax)));
		}
		
		// Working RUP
		foreach (var pt in grp.Working.OrderBy(x => x.TS))
		{
			var d = pvmc.FlowDate + pt.TS;
			s_wrup.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(d), Convert.ToDouble(pt.PMin)));
			s_wrup.Points2.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(d), Convert.ToDouble(pt.PMax)));
			s_wrup.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(d.AddMinutes(1)), Convert.ToDouble(pt.PMin)));
			s_wrup.Points2.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(d.AddMinutes(1)), Convert.ToDouble(pt.PMax)));
		}
		
		pm.Series.Add(s_rup);
		pm.Series.Add(s_wrup);
	}

	// PV
	var s_pv = new OxyPlot.Series.StairStepSeries();
	s_pv.Title = "PV";
	s_pv.Color = OxyColors.Black;
	foreach(var pt in pvmc.OriginalPlan)
	{
		var d = pvmc.FlowDate + pt.TS;
		s_pv.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(d), Convert.ToDouble(pt.Val)));
	}
	pm.Series.Add(s_pv);
	
	// PVM
	var s_pvm = new OxyPlot.Series.StairStepSeries();
	s_pvm.Title = "PVM";
	s_pvm.Color = OxyColors.Blue;
	foreach(var pt in pvmc.PVM)
	{
		var d = pvmc.FlowDate + pt.TS;
		s_pvm.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(d), Convert.ToDouble(pt.Val)));
	}
	pm.Series.Add(s_pvm);
	
	// PVMC
	var s_pvmc = new OxyPlot.Series.StairStepSeries();
	s_pvmc.Title = "PVMC";
	s_pvmc.Color = OxyColors.Red;
	foreach(var pt in pvmc.Plan)
	{
		var d = pvmc.FlowDate + pt.TS;
		s_pvmc.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(d), Convert.ToDouble(pt.Val)));
	}
	pm.Series.Add(s_pvmc);


	// BDE
	var msgColl = pvmc.Commands
		.Where(x => (x is BalanceOrderMessage) && !x.HasBeenRevoked(pvmc))
		.Cast<BalanceOrderMessage>();
	foreach (var cmd in msgColl)
	{
		var anno = new OxyPlot.Annotations.LineAnnotation();
		anno.Type = OxyPlot.Annotations.LineAnnotationType.Vertical;
		var sd = pvmc.FlowDate + cmd.StartDate.GetTimeSpan(pvmc.FlowDate);
		anno.X = OxyPlot.Axes.DateTimeAxis.ToDouble(sd);
		anno.Color = OxyColors.Green;
		anno.Text = string.Format("{3} {0} {1:0.000} {2}", cmd.ID, cmd.PVPowerDelta_TFIN, cmd.ContinuationType == BalanceOrderType.RampAndStay ? "ST" : "MD", cmd.StartDate.Value.ToString("HH:mm"));
		pm.Annotations.Add(anno);		
	}
	return pm;
}

void Show(PlotModel model, double width = 1000, double height = 800)
{
	var w = new Window() { Title = "OxyPlot.Wpf.PlotView : " + model.Title, Width = width, Height = height };
	var plot = new PlotView();
	var ls = model.Series;
	var cmenu = new ContextMenu();
	
	ls.ToList().ForEach(x => {
		var mi = new MenuItem() { IsCheckable = true, IsChecked = true, Header = x.Title };
		mi.Click += (s,e) =>
		{
			var _mi = (e.Source as MenuItem);
			var header = _mi.Header.ToString();
			var ischecked = _mi.IsChecked;
			model.Series.First(serie => serie.Title == header).IsVisible = ischecked; 
			model.InvalidatePlot(true);
		};
		cmenu.Items.Add(mi);
	});
	
	
	var ctrl = new PlotController();
	ctrl.BindMouseDown(OxyMouseButton.Left, OxyPlot.PlotCommands.PanAt);
	ctrl.BindMouseEnter(OxyPlot.PlotCommands.HoverPointsOnlyTrack);
	ctrl.UnbindMouseDown(OxyMouseButton.Right);
	
	plot.ContextMenu = cmenu;
	plot.Model = model;
	plot.Controller = ctrl;
	w.Content = plot;
	
	//plot.Model.RenderingDecorator = rc => new XkcdRenderingDecorator(rc);
	w.Show();
}

public static IEnumerable<DateTime> GetAllDayMinutes(DateTime d)
{
	var baseDate = d.Date;
	var allMinutes = Enumerable.Range(0, 60 * 24).Select(x => baseDate.AddMinutes(x));
	return allMinutes;
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
        private System.DateTime LatestMeasureTimeField;
        
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
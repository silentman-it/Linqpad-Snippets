<Query Kind="Statements">
  <Connection>
    <ID>a8ee61ce-9327-4eb9-9151-c5a59953329e</ID>
    <Persist>true</Persist>
    <Driver Assembly="IQDriver" PublicKeyToken="5b59726538a49684">IQDriver.IQDriver</Driver>
    <Provider>Devart.Data.Oracle</Provider>
    <CustomCxString>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAqn+JISAp+UKFm1BDMQWnzAAAAAACAAAAAAADZgAAwAAAABAAAADNa8pspNOLsuf1mpyqPzQQAAAAAASAAACgAAAAEAAAADpNJN7L9tXbkf3xYkS2vF1AAAAAwLYEvYgKIVk4Z+8M3lCZRqv3Te6//onkyTYOolWZL0pQiXqbqrsVhj/40kBrbJOtO2snSnW+PAARpEDnCCNOCBQAAACXdsfHG6JaAP6KKmfVpaixsFPmRg==</CustomCxString>
    <Server>emodsdemoroom</Server>
    <UserName>ed_warehouse</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAqn+JISAp+UKFm1BDMQWnzAAAAAACAAAAAAADZgAAwAAAABAAAACBH5XHoZXwzX5TPB292CsEAAAAAASAAACgAAAAEAAAAHdxpcVdjg6sgPY1JI2lou0QAAAA16CNtqGln2MAUlzlHK5yzRQAAADBPOWNrSwd9uOJdtiHdrskEWX/MA==</Password>
    <EncryptCustomCxString>true</EncryptCustomCxString>
    <DriverData>
      <StripUnderscores>true</StripUnderscores>
      <QuietenAllCaps>true</QuietenAllCaps>
      <ConnectAs>Default</ConnectAs>
      <UseOciMode>true</UseOciMode>
    </DriverData>
  </Connection>
  <Output>DataGrids</Output>
</Query>

var lsSerie = Series.ToList();
lsSerie.Dump();

//int count = 0;

Stopwatch sw = new Stopwatch();

foreach(var serie in lsSerie)
{
	sw.Start();
	var lsSerieDetails = SeriesDetails.Where(x => x.SerieID == serie.SerieID);
	
	Console.Write(string.Format("Adding data for serie: {0}/{1}/{2}/{3}/{4} ({5} details)... ", serie.SourceCd, serie.SymbolCd, serie.ClassCd, serie.ChannelCd, serie.ContextCd, lsSerieDetails.Count()));
	foreach (var x in lsSerieDetails)
	{
		File.AppendAllText(@"c:\temp\EMODS_Dump.csv",
			string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8}\r\n",
				x.Serie.SourceCd,
				x.Serie.SymbolCd,
				x.Serie.ClassCd,
				x.Serie.ChannelCd,
				x.Serie.ContextCd,
				x.ValidityStartDate.ToString("dd/MM/yyyy HH:mm:ss"),
				x.ValidityEndDate.ToString("dd/MM/yyyy HH:mm:ss"),
				x.Value,
				x.Label));
	}
	
	sw.Stop();
	Console.WriteLine(string.Format("DONE in {0}!", sw.Elapsed));
	sw.Reset();

}

Console.WriteLine("ALL DONE!!!!");
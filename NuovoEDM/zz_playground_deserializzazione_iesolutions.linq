<Query Kind="Statements">
  <Reference>C:\Work\EMODS\DEV\EMODS2\Common\EMODS.Common\bin\Debug\EMODS.Common.dll</Reference>
  <Reference>C:\Work\EMODS\DEV\EMODS2\Activities\EMODS.Data.IESolutions\bin\Debug\EMODS.Data.IESolutions.dll</Reference>
  <Namespace>EMODS.Data.IESolutions.Schemas</Namespace>
  <Namespace>EMODS.Common.Serialization</Namespace>
  <Namespace>System.Globalization</Namespace>
</Query>

string fileName = @"C:\Temp\Archive\2013-12-18.xml";
misuratore o = XmlHelper.Deserialize<misuratore>(File.ReadAllBytes(fileName));

var measuresTable = o.item.Select(x => new {
	Misuratore = x.Attributes.nome.item.Value,
	UoM = x.Attributes.misura.Value,
	Energia = x.Attributes.energia.Value,
	Data = DateTime.ParseExact(x.Attributes.data.Value, "yyyy-MM-dd", CultureInfo.InvariantCulture),
	Misure = x.consumo.ora.item.Select(h => new {
		H = h.Text.Value,
		QtrList = h.quarti.Text.Select(q => new { Q = q.idx, Val = q.Value })
	})
})
.SelectMany(d =>
	d.Misure.SelectMany(h =>
		h.QtrList.Select(q => new
		{
			Misuratore = d.Misuratore,
			UoM = d.UoM,
			Energia = d.Energia,
			Data = d.Data,
			H = h.H,
			Q = q.Q,
			DataOra = d.Data.AddHours((double)h.H).AddMinutes((double)(q.Q - 1) * 15),
			Val = q.Val
		})
		)
	)
//.Dump()
;

measuresTable.GroupBy(x => new { Symbol = x.Misuratore, Class = x.Energia.Split(' ').First().Trim(), Channel = x.Energia.Split(' ').Last().Trim() })
.Dump();
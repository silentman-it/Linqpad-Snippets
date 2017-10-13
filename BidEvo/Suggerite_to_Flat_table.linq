<Query Kind="Program">
  <Reference>C:\Work\BidEvolution\DEV\Gme\Messaging.Formats\bin\Debug\Bid.Messaging.Formats.dll</Reference>
  <Namespace>Bid.Messaging.Formats.BidEvo</Namespace>
  <Namespace>System.Xml.Serialization</Namespace>
  <Namespace>System.Runtime.Serialization.Formatters.Binary</Namespace>
  <Namespace>System.Globalization</Namespace>
</Query>

void Main()
{
	var o = XmlSerialization.Deserialize<BMTransactionSUG>(File.ReadAllText(@"C:\Temp\Archive\Suggerite_MGP_20171001.xml"));
	
	CultureInfo it = CultureInfo.CreateSpecificCulture("it-IT");
	
	o.Suggested.Coordinate
	.SelectMany(x => x.SG1.DefaultIfNull().Select(sg => new {
		FlowDate = x.FlowDate,
		Unit = x.IDUnit,
		Market = x.Mercato,
		Hour = sg.Value,
		Gradino = "SG1",
		Qty = sg.QUA,
		Prc = sg.PRE,
		PurposeSpecified = sg.AZIONESpecified,
		Purpose = sg.AZIONE
	}).Union(x.SG2.DefaultIfNull().Select(sg => new {
		FlowDate = x.FlowDate,
		Unit = x.IDUnit,
		Market = x.Mercato,
		Hour = sg.Value,
		Gradino = "SG2",
		Qty = sg.QUA,
		Prc = sg.PRE,
		PurposeSpecified = sg.AZIONESpecified,
		Purpose = sg.AZIONE
	})).Union(x.SG3.DefaultIfNull().Select(sg => new {
		FlowDate = x.FlowDate,
		Unit = x.IDUnit,
		Market = x.Mercato,
		Hour = sg.Value,
		Gradino = "SG3",
		Qty = sg.QUA,
		Prc = sg.PRE,
		PurposeSpecified = sg.AZIONESpecified,
		Purpose = sg.AZIONE
	})).Union(x.SG4.DefaultIfNull().Select(sg => new {
		FlowDate = x.FlowDate,
		Unit = x.IDUnit,
		Market = x.Mercato,
		Hour = sg.Value,
		Gradino = "SG4",
		Qty = sg.QUA,
		Prc = sg.PRE,
		PurposeSpecified = sg.AZIONESpecified,
		Purpose = sg.AZIONE
	})))
	.Select(x => new {
		FlowDate = DateTime.ParseExact(x.FlowDate, "yyyyMMdd", CultureInfo.InvariantCulture),
		Unit = x.Unit,
		Market = x.Market,
		Hour = Convert.ToInt16(x.Hour),
		Gradino = x.Gradino,
		Quantity = Double.Parse(x.Qty, it),
		Price = Double.Parse(x.Prc, it),
		Purpose = x.PurposeSpecified ? (x.Purpose == TipoAzione.ACQ ? "A" : "V") : null
	})
	.Dump();
	
}

public static class MyExt3982 {
	public static IEnumerable<T> DefaultIfNull<T>(this IEnumerable<T> c)
	{
		return c != null ? c : new List<T>().AsEnumerable();
	}
}

// Define other methods and classes here
public static class XmlSerialization
{
   public static string Serialize<T>(T p)
   {
       XmlSerializer serializer = new XmlSerializer(typeof(T));
       StringWriter stringWriter = new System.IO.StringWriter();

       XmlWriterSettings xmlWriterSettings = new XmlWriterSettings 
       { 
           Indent = true, 
           OmitXmlDeclaration = true, 
           Encoding = Encoding.UTF8 
       };
       XmlWriter xmlWriter = XmlWriter.Create(stringWriter, xmlWriterSettings);

       serializer.Serialize(xmlWriter, p);
       stringWriter.Close();
       return stringWriter.GetStringBuilder().ToString();
   }

   public static T Deserialize<T>(string p)
   {
       if (string.IsNullOrEmpty(p))
           throw new ArgumentNullException("InputString");

       XmlSerializer deserializer = new XmlSerializer(typeof(T));
       StringReader stringReader = new StringReader(p);
       T o = (T)deserializer.Deserialize(stringReader);
       stringReader.Close();
       return o;
   }

   public static T Deserialize<T>(byte[] a)
   {
       return Deserialize<T>(ASCIIEncoding.ASCII.GetString(a));
   }
}

public static class BinarySerialization
{
   public static byte[] Serialize<T>(T p)
   {
       if (p == null)
           return null;

       MemoryStream writeStream = new MemoryStream();
       BinaryFormatter formatter = new BinaryFormatter();
       formatter.Serialize(writeStream, p);
       writeStream.Seek(0, SeekOrigin.Begin);
       byte[] buff = new byte[writeStream.Length];
       writeStream.Read(buff, 0, Convert.ToInt32(writeStream.Length));
       writeStream.Close();
       return buff;
   }

   public static T Deserialize<T>(byte[] p)
   {
       MemoryStream readStream = new MemoryStream(p);
       BinaryFormatter formatter = new BinaryFormatter();
       T readData = (T)formatter.Deserialize(readStream);
       readStream.Close();
       return readData;
   }
}
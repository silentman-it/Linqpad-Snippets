<Query Kind="Program" />

void Main()
{
	
	
}

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


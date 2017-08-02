<Query Kind="Program">
  <Connection>
    <ID>1202f3d3-b7d5-4258-ac52-f1ee0160d302</ID>
    <Persist>true</Persist>
    <Driver Assembly="IQDriver" PublicKeyToken="5b59726538a49684">IQDriver.IQDriver</Driver>
    <Provider>Devart.Data.Oracle</Provider>
    <CustomCxString>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAg4VPWvAGA0K8d2W/MBVMggAAAAACAAAAAAAQZgAAAAEAACAAAAAKrfwu2OSOqwolUiPWik9UN40wR5Dzp4939gu7/wVSuwAAAAAOgAAAAAIAACAAAACEHPLUc7dObeuKE6aYQstf+XRl/tjcDVDoCpnw4ihNuUAAAAC0VT8gG05av7bVR2DERhfHx3gtwL8WhST0zmfkwV8ip/oDMVYiXXx0hgdbNoDB0vyx7Rg4JLZL1BbrRfvx2KfFQAAAAGcFgzHUIB3znMMgGvIP1f3+0+6mNB1vIQEny/V/UbBz0mNNCGdQJ3ueiwZ2jOBed3lJ/4gj4yWxCf6BYbSCAGk=</CustomCxString>
    <Server>svil4</Server>
    <UserName>ed_framework</UserName>
    <EncryptCustomCxString>true</EncryptCustomCxString>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA8yqkEwwIyEy9PPj7RuTVJgAAAAACAAAAAAAQZgAAAAEAACAAAACvZ09a4O49pXr13xw2SRRmG6aSJ3FKsaCyYw3Xl3RMzQAAAAAOgAAAAAIAACAAAAB8la2vpFtF4sC3guzRW/tgam52bSl04YCE2RfWfn6v1hAAAABwfqVOxamEcNGTSCO9QM5VQAAAABP4/dXbp0cm+gGrwa6hMG2W26osBYKrZYAZDS11X+dBr7CPCq/mM7m83JuzL+GIVuNg0bXIIlzQMpME/OmtGBQ=</Password>
    <DisplayName>svil4.ed_framework</DisplayName>
    <DriverData>
      <StripUnderscores>true</StripUnderscores>
      <QuietenAllCaps>true</QuietenAllCaps>
      <ConnectAs>Default</ConnectAs>
      <UseOciMode>true</UseOciMode>
    </DriverData>
  </Connection>
  <Reference>C:\Work\TMS\DEV\TMS\Common\TMS.Common\bin\x64\Debug\TMS.Common.dll</Reference>
  <Namespace>TMS.Common.Types.Schemas.TMS_Commands</Namespace>
  <Namespace>System.Xml.Serialization</Namespace>
  <Namespace>System.Runtime.Serialization.Formatters.Binary</Namespace>
</Query>

void Main()
{
	// Command TYPE
	var o = new TmsCommands() { Type = "GasConsumptionForecast" };
	
	// Command PARAMS
	object[] items =
	{
		new TmsCommandsCommandString() { Name = "Units", Value = "UP_NAPOLIL_4" },
		new TmsCommandsCommandDate()   { Name = "FlowDate", Value = DateTime.Parse("13/02/2016") }
	};
	
	TmsCommandsCommand[] cmds = { new TmsCommandsCommand() { Items = items }};	
	o.Command = cmds;
	
	XmlSerialization.Serialize(o).Dump();
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
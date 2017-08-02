<Query Kind="Statements" />

var fileName = @"C:\Users\Federico\Desktop\US_MI1D_20160920.994892149844883.OEISNTP.out.xml"; // cum yes
//var fileName = @"C:\Users\Federico\Desktop\US_MI1D_20160920.994892149842854.OEISNTP.out.xml"; // cum no

var fileContent = File.ReadAllText(fileName);



XmlDocument doc = new XmlDocument();
doc.LoadXml(fileContent);

//var xpath = "/*[local-name()='PIPEDocument']/*[local-name()='PIPTransaction']/*[local-name()='BidNotification']";
//var xpath = "/*[local-name()='PIPEDocument']/*[local-name()='PIPTransaction']/*[local-name()='UnitSchedule']/*[local-name()='Market'][text()='MI1']";

var xpath = "/*[local-name()='PIPEDocument']/*[local-name()='PIPTransaction']/*[local-name()='UnitSchedule'][@Cummulative='Yes']";


var n = doc.SelectNodes(xpath);


(n != null && n.Count > 0).Dump("Found?");

n.Dump();
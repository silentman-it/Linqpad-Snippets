<Query Kind="Statements">
  <Namespace>System.Net.Mail</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

SmtpClient s = new SmtpClient();
s.Host = "grpgepwmx002.grptop.net";
//s.Host = "wmail.elsag.it";
//s.Host = "smtp.elsag.it";
s.EnableSsl = true;
s.UseDefaultCredentials = false;
s.Credentials = new NetworkCredential(@"EDG\CoppolaF", "checculo");
s.DeliveryMethod = SmtpDeliveryMethod.Network;

s.Port = 465;

s.Send("designatore@fipav.org", "federico.coppola@selexelsag.com", "Designazione di prova", "Sei designato");

Console.WriteLine ("Email sent!");

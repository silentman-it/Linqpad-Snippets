<Query Kind="Program">
  <Namespace>System.Net.Mail</Namespace>
</Query>

void Main()
{
	SendEmail("fedesilent@gmail.com", "Prova prova 123", "Prova da linqpad");
}

// Define other methods and classes here
private void SendEmail(string to, string subj, string body)
{
	MailMessage msg = new MailMessage();
	msg.From = new MailAddress("bat@ergnet.it");
	
	// Recipients
	to
		.Split(';', ',')
		.Select(x => x.Trim())
		.ToList()
		.ForEach(x => {
			msg.To.Add(new MailAddress(x));
		});
	
	// Subject
	msg.Subject = subj;
	
	// Body
	msg.Body = body;
	
	// Send message!
	
	SmtpClient client = new SmtpClient("posta.enterprise.ergnet.it", 25);
	client.DeliveryMethod = SmtpDeliveryMethod.Network;
	//retries sending an e-mail if the timeout it hit
	try
	{
		client.Timeout = 30000;
		client.Send(msg);
		Console.WriteLine ("Mail inviata correttamente!");
	}
	catch(Exception ex)
	{
		Console.WriteLine ("ERRORE durante l'invio della mail: " + ex.Message);
	}

}
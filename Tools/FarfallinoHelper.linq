<Query Kind="Program" />

void Main()
{
	string encoded = Farfallino.Encode("Ciao amici! Sono il vostro fafafafafafaJohnny Glamour").Dump();
	string decoded = Farfallino.Decode(encoded).Dump();
}

public static class Farfallino
{
	public static string Encode(string decoded)
	{
		return decoded
			.Replace("a", "afa")
			.Replace("e", "efe")
			.Replace("i", "ifi")
			.Replace("o", "ofo")
			.Replace("u", "ufu")
			.Replace("A", "Afa")
			.Replace("E", "Efe")
			.Replace("I", "Ifi")
			.Replace("O", "Ofo")
			.Replace("U", "Ufu");
	}
	
	public static string Decode(string encoded)
	{
		return encoded
			.Replace("afa", "a")
			.Replace("efe", "e")
			.Replace("ifi", "i")
			.Replace("ofo", "o")
			.Replace("ufu", "u")
			.Replace("Afa", "A")
			.Replace("Efe", "E")
			.Replace("Ifi", "I")
			.Replace("Ofo", "O")
			.Replace("Ufu", "U");
	}
}

<Query Kind="Statements">
  <Connection>
    <ID>a4da0b26-e220-492d-a3ae-4b29358701ef</ID>
    <Persist>true</Persist>
    <Server>172.19.122.63\,3433</Server>
    <SqlSecurity>true</SqlSecurity>
    <UserName>sa</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAvm6QbRxlJUuLCV01lsuo5AAAAAACAAAAAAAQZgAAAAEAACAAAABCZX2NEJwBBxkMXKe3E6O8nnZCKWTREELMs7QORETUmQAAAAAOgAAAAAIAACAAAACnWDa1IoHM8yW+QOJTbPTIJ6KBEZGaGMuiRljo/BHB3BAAAACjSL7Cv48d/LckZxgcQjtJQAAAAK7cjXK/2lUepF0NlbJVxfFOPO52z69ENMR99XvafjoOrKt5x2io/dvDOEHjzmB/Qe1BHgF5Ux/lGJ26GwyzvKQ=</Password>
    <Database>BIDEVO_DEV</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Reference>C:\Work\BidEvolution\DEV\Gme\Core\bin\Debug\Bid.Core.dll</Reference>
  <Reference>C:\Work\BidEvolution\DEV\Gme\Public\bin\Debug\Bid.Public.dll</Reference>
</Query>

var clearTextPassword = "federico";
var salt = Bid.Public.Encryption.EncryptionHelper.GenerateSalt().Dump("Salt");
var pswd = Bid.Public.Encryption.EncryptionHelper.Encrypt(salt + clearTextPassword).Dump("Encrypted Password");


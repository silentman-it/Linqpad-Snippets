<Query Kind="Statements" />

var input = "CamelCaseTextStoCazzo";

System.Text.RegularExpressions.Regex.Replace(input, "(?<=.)([A-Z])", "_$0", System.Text.RegularExpressions.RegexOptions.Compiled).ToUpper().Dump();
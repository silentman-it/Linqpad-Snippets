<Query Kind="Statements">
  <Namespace>System.Globalization</Namespace>
</Query>

var dateFormat = "dd/MM/yyyy HH.mm";
var cultureListSeparator = CultureInfo.CurrentCulture.TextInfo.ListSeparator;
string[] arrayOfSeparators = { cultureListSeparator };

//var fileName = @"C:\Users\Federico\Desktop\Exempio_v4_inviato_ISAB - Copy.csv";
var fileName = @"C:\Users\Federico\Desktop\Exempio_v4_inviato_ISAB.csv";

var rows = File.ReadAllLines(fileName);

var temp1 = rows
.Select(x => x.Split(arrayOfSeparators, StringSplitOptions.None))
.ToList()
//.Dump()
;


// Get info from header
var header = temp1.ElementAt(0);
temp1.RemoveAt(0);

var unitName = header.ElementAtOrDefault(1).Dump("UnitName");

// Remove empty lines between header & body
while(true)
{
	// se non contiene informazioni, e' una linea inutile
	bool useless = temp1.ElementAt(0).All(x => string.IsNullOrEmpty(x.Trim()));
	if(useless)
		temp1.RemoveAt(0);
	else
		break;
}

temp1.RemoveAt(0);

var temp2 = temp1.Select(x => new
{
	VariationId  = x[0],
	MotivationId = x[1],
	Notes        = x[2],
	StartDate    = x[3],
	EndDate      = x[4],
	PSMIN        = x[5],
	PSMAX        = x[6],
	AssetId      = x[7],
	PTMIN        = x[8],
	PTMAX        = x[9],
	TRAMPA       = x[10],
	TDERAMPA     = x[11],
	TAVA         = x[12],
	TARA         = x[13],
	TRISP        = x[14],
	GPA          = x[15],
	GPD          = x[16],
	BRP          = x[17],
	PQNR         = x[18]
})
//.Dump()
.Select(x => new 
{
	VariationId  = !string.IsNullOrEmpty(x.VariationId)  ? x.VariationId  : null ,
	MotivationId = !string.IsNullOrEmpty(x.MotivationId) ? x.MotivationId : null ,
	Notes        = !string.IsNullOrEmpty(x.Notes)        ? x.Notes : null        ,
	StartDate    = !string.IsNullOrEmpty(x.StartDate)    ? DateTime.ParseExact(x.StartDate, dateFormat, CultureInfo.InvariantCulture) : (DateTime?)null,
	EndDate      = !string.IsNullOrEmpty(x.EndDate)      ? DateTime.ParseExact(x.EndDate, dateFormat, CultureInfo.InvariantCulture)   : (DateTime?)null,
	PSMIN        = Decimal.Parse(x.PSMIN   ),
	PSMAX        = Decimal.Parse(x.PSMAX   ),
	AssetId      = x.AssetId,
	PTMIN        = Decimal.Parse(x.PTMIN   ),
	PTMAX        = Decimal.Parse(x.PTMAX   ),
	TRAMPA       = Decimal.Parse(x.TRAMPA  ),
	TDERAMPA     = Decimal.Parse(x.TDERAMPA),
	TAVA         = Decimal.Parse(x.TAVA    ),
	TARA         = Decimal.Parse(x.TARA    ),
	TRISP        = Decimal.Parse(x.TRISP   ),
	GPA          = Decimal.Parse(x.GPA     ),
	GPD          = Decimal.Parse(x.GPD     ),
	BRP          = Decimal.Parse(x.BRP     ),
	PQNR         = x.PQNR.Split('/', '|')	
})
.ToList()
.Dump()
;


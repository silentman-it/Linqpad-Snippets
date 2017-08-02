<Query Kind="Statements">
  <NuGetReference>ZXing.Net</NuGetReference>
  <Namespace>ZXing</Namespace>
  <Namespace>System.Drawing</Namespace>
  <Namespace>ZXing.Common</Namespace>
  <Namespace>ZXing.QrCode.Internal</Namespace>
</Query>

var writer = new BarcodeWriter
	{
		Format = BarcodeFormat.QR_CODE,
		Options = new EncodingOptions
		{
			//Height = 00,
			//Width = 200,
			Margin = 0
		},
	};
	
	writer.Options.Hints[EncodeHintType.ERROR_CORRECTION] = ErrorCorrectionLevel.L;

	
	var bmp = writer.Write("eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJmZWRlQHNpbGVudG1hbi5pdCIsInJvbGUiOiJBZG1pbiIsImJpZC5jbGFpbTEiOiJ0ZXN0MSIsIm5iZiI6MTUwMTY4OTEwMiwiZXhwIjoxNTAxNjg5MTYyLCJpYXQiOjE1MDE2ODkxMDIsImlzcyI6Imh0dHA6Ly9iZWUubGVvbmFyZG9jb21wYW55LmNvbS8iLCJhdWQiOiJodHRwOi8vYmlkZXZvbHV0aW9uLmxlb25hcmRvY29tcGFueS5jb20vIn0.fcfjCW1pCyX8R0ZQCTyKOAJ0Ou81RbTHyCZJse4pVEfkPXkE-tdomHx9pCCZwljLdFx3_q5C63N9Hm5MJXP0lG2RhqlRlHz0_TRkkL302qTDVQPmd7k9FOJZ8M4T0dJSo79Hf-Eks4NhUmbWRZghsDJ4ceWKvAqUwEBL18X-G4LJsoLBVazzr5_JHxT6t8k-ZRES1Ok6g9ohMrihL6NUvqnODoxNnAuGDeSCfKFE0C4rsqXqEsU_v1lYrV4oJD3jQtcb09Xiap6hQEgE_wILhV09RyBPFgFVommTVj_Yz_kKZuXESGPmhbu3pBmnlfWbInJqDZ9GNzJWY4bTx69GDA");
	
	bmp.Dump();



// Reading QR

// create a barcode reader instance
IBarcodeReader reader = new BarcodeReader();
// load a bitmap
var barcodeBitmap = new Bitmap(bmp);
// detect and decode the barcode inside the bitmap
var result = reader.Decode(barcodeBitmap);
// do something with the result
if (result != null)
{
   result.BarcodeFormat.Dump("Format");
   result.Text.Dump("Text");
}
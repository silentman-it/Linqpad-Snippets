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

	
	var bmp = writer.Write("M1COPPOLA/FEDERICO    EFAYUSV GOAFCOAZ 1380 196Y019A0079 162>5321MR7195BAZ                                        2A0552332761005 0                          N");
	
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
<Query Kind="Program">
  <NuGetReference>QRCoder</NuGetReference>
  <Namespace>QRCoder</Namespace>
  <Namespace>System.Drawing</Namespace>
  <Namespace>System.Windows</Namespace>
</Query>

void Main()
{
	QRCodeGenerator.ECCLevel eccLevel = QRCodeGenerator.ECCLevel.L;

	using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
	{
		using (QRCodeData qrCodeData = qrGenerator.CreateQrCode("eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJmZWRlQHNpbGVudG1hbi5pdCIsInJvbGUiOiJBZG1pbiIsIm5iZiI6MTUwMTU4ODY0OSwiZXhwIjoxNTAxNTkyMjQ5LCJpYXQiOjE1MDE1ODg2NDksImlzcyI6Imh0dHA6Ly9iZWUubGVvbmFyZG9jb21wYW55LmNvbS8iLCJhdWQiOiJodHRwOi8vYmlkZXZvbHV0aW9uLmxlb25hcmRvY29tcGFueS5jb20vIn0.OInAhxytnKPEkFWTPIx0xX8nLAlFmEPfEPNR4eq551qOukgCDTV7mKpfROYH0dfEHw66cmmHZo3Hl03yP6p-tNbWnd8g_hkTDXRDsahqnL-VCVIuWceqnpWGiihb5-d9cn0joy8E41uIm4DBiv-BxssYdcYlJlG9NOA7Vs879ALNjtx3Pg4-XMvBJ_xRl-kn6s_uuMg6tOIPOExEl7AkuGwgdaN-PwskCZP_Iksui3z7hk7K3o9DNwEzFKwyj_bqyY-cggzJuP2eBZJ2cBdV_nnRTNnE5OpymShbmloidVZPyM4ajlH_yO5yyNVGJlDMhwuDMfxLhlRgG110VkL3Pw", eccLevel))
		{
			using (QRCode qrCode = new QRCode(qrCodeData))
			{
				
				var bmp = qrCode.GetGraphic(3);
				bmp.Dump();
			}
		}
	}


}

// Define other methods and classes here
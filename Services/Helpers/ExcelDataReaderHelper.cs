using System.Text;

namespace Services.Helpers
{
	public static class ExcelDataReaderHelper
	{
		public static void AddEncodingSupport()
		{
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
		}
	}
}

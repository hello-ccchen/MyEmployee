using Core;
using System.Collections.Generic;
using System.IO;

namespace Services
{
	public interface IExcelService
	{
		List<Employee> ReadEmployeeExcelFile(Stream excelFileStream);
	}
}

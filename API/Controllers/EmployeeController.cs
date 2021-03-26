using Microsoft.AspNetCore.Mvc;
using Services;
using System;
using System.IO;
using System.Linq;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmployeeController : ControllerBase
	{
		private readonly IExcelService _excelService;
		private static readonly string[] AcceptedFileTypes = { ".xlsx", ".xls" };
		
		public EmployeeController(IExcelService excelService)
		{
			_excelService = excelService;
		}

		[HttpPost]
		public IActionResult UploadExcel()
		{
			var file = Request.Form.Files[0];

			if (file == null)
				return BadRequest();

			if (!AcceptedFileTypes.Contains(Path.GetExtension(file.FileName)))
				return BadRequest();

			using var memoryStream = new MemoryStream();
			file.CopyTo(memoryStream);
			memoryStream.Position = 0;

			var result = _excelService.ReadEmployeeExcelFile(memoryStream);

			return Ok(result);
		}
	}
}

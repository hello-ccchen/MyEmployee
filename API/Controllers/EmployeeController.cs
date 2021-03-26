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
			var files = Request.Form.Files;

			if (files == null)
				return BadRequest();

			if (!AcceptedFileTypes.Contains(Path.GetExtension(files[0].FileName)))
				return BadRequest();

			using var excelFileStream = new MemoryStream();
			files[0].CopyTo(excelFileStream);
			excelFileStream.Position = 0;

			var result = _excelService.ReadEmployeeExcelFile(excelFileStream);

			return Ok(result);
		}
	}
}

using Services.Helpers;
using System;
using System.IO;
using Xunit;

namespace Services.Tests
{
	public class ExcelServiceTests
	{
		public ExcelServiceTests()
		{
			ExcelDataReaderHelper.AddEncodingSupport();
		}
		
		[Fact]
		public void ReadEmployeeExcelFile_ShouldReturnCorrectResult()
		{
			// Arrange
			using var excelFileStream = File.Open(Path.Combine(Environment.CurrentDirectory + "\\MockData", "Employee.xlsx"), FileMode.Open, FileAccess.Read);

			// Act
			var results = new ExcelService().ReadEmployeeExcelFile(excelFileStream);

			// Assert
			Assert.Equal(3, results.Count);

			Assert.Equal("001", results[0].EmployeeNumber);
			Assert.Equal("John", results[0].FirstName);
			Assert.Equal("Doe", results[0].LastName);
			Assert.Equal("Regular", results[0].EmployeeStatus);

			Assert.Equal("002", results[1].EmployeeNumber);
			Assert.Equal("Jane", results[1].FirstName);
			Assert.Equal("Doe", results[1].LastName);
			Assert.Equal("Contractor", results[1].EmployeeStatus);

			Assert.Equal("003", results[2].EmployeeNumber);
			Assert.Equal("Harry", results[2].FirstName);
			Assert.Equal("Potter", results[2].LastName);
			Assert.Equal("Regular", results[2].EmployeeStatus);
		}

		[Fact]
		public void ReadEmployeeExcelFile_ShouldThrowInvalidDataException()
		{
			// Arrange
			// Act
			// Assert
			var exception = Assert.Throws<InvalidDataException>( () => new ExcelService().ReadEmployeeExcelFile(null));
			Assert.Equal("Invalid excel file stream", exception.Message);
		}
	}
}

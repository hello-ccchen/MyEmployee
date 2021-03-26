using Core;
using ExcelDataReader;
using ExcelDataReader.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace Services
{
	public class ExcelService : IExcelService
	{
        public List<Employee> ReadEmployeeExcelFile(Stream excelFileStream)
        {
            var result = ReadExcelFile(excelFileStream);
            return GetEmployeeEntries(result);
        }

        private static DataSet ReadExcelFile(Stream excelFileStream)
        {
            DataSet result;

            try
            {
                using var reader = ExcelReaderFactory.CreateReader(excelFileStream);
                result = reader.AsDataSet(new ExcelDataSetConfiguration
                {
                    ConfigureDataTable = _ => new ExcelDataTableConfiguration { UseHeaderRow = true }
                });
            }
            catch (Exception ex) when (ex is HeaderException)
            {
                throw new InvalidDataException("Invalid Employee Excel Header");
            }

            return result;
        }

        private static List<Employee> GetEmployeeEntries(DataSet result)
        {
            var entries = new List<Employee>();
            var rows = result.Tables[0].Select();

            foreach (var row in rows)
            {
                var employee = new Employee
                {
                    EmployeeNumber = Convert.ToString(row[0]),
                    FirstName = Convert.ToString(row[1]),
                    LastName = Convert.ToString(row[2]),
                    EmployeeStatus = Convert.ToString(row[3])
                };

                entries.Add(employee);
            }
            return entries;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Text;
using TimesheetApp.BusinessLogic.Interfaces;

namespace TimesheetApp.BusinessLogic
{
    public class CsvGenerator : ICsvGenerator
    {
        public string GenerateCsv<T>(IEnumerable<T> records)
        {
            var properties = typeof(T).GetProperties();
            var csv = new StringBuilder();

            csv.AppendLine(string.Join(",", properties.Select(p => p.Name)));

            foreach (var record in records)
            {
                var line = string.Join(",", properties.Select(p => p.GetValue(record)?.ToString() ?? ""));
                csv.AppendLine(line);
            }

            return csv.ToString();
        }
    }
}

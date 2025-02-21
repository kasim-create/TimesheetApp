using System.Collections.Generic;

namespace TimesheetApp.BusinessLogic.Interfaces
{
    public interface ICsvGenerator
    {
        string GenerateCsv<T>(IEnumerable<T> records);

    }

}

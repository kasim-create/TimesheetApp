using Microsoft.EntityFrameworkCore;
using TimesheetApp.Models.EntityModels;

namespace TimesheetApp.Data.Repositories.Interfaces
{
    public interface ITimesheetRepository
    {
        public List<Timesheet> GetTimesheets();

        public void CreateTimesheet(Timesheet timesheet);

    }
}

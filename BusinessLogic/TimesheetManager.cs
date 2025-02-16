using TimesheetApp.Data;
using TimesheetApp.Data.Repositories.Interfaces;
using TimesheetApp.Models.EntityModels;

namespace TimesheetApp.BusinessLogic
{
    public class TimesheetManager
    {
        private readonly ITimesheetRepository _timesheetRepository;
        public TimesheetManager(ITimesheetRepository timesheetRepository) 
        { 
            _timesheetRepository = timesheetRepository;
        }
        public List<Timesheet> GetTimesheets() => _timesheetRepository.GetTimesheets();

        public void CreateTimesheet(Timesheet timesheet) => _timesheetRepository.CreateTimesheet(timesheet);

    }
}

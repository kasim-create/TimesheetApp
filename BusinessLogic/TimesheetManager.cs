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

        public void CreateTimesheet(Timesheet timesheet) 
        {
            _timesheetRepository.CreateTimesheet(timesheet);

            var timesheets = _timesheetRepository.GetTimesheets();

            var matchedTimesheets = timesheets?.Where(x => x.Name == timesheet.Name &&  x.Date == timesheet.Date).ToList();

            if (matchedTimesheets != null)
            {
                if (matchedTimesheets.Any() && matchedTimesheets.Count > 1)
                {
                    var sum = matchedTimesheets.Sum(m => m.HoursWorked);

                    foreach (var matchedTimesheet in matchedTimesheets)
                    {
                        matchedTimesheet.TotalHoursWorked = 0;
                        matchedTimesheet.TotalHoursWorked += sum;
                    }
                    _timesheetRepository.UpdateTimesheets(matchedTimesheets);
                }
            }
        } 

    }
}

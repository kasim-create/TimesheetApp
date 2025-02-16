using TimesheetApp.Data.Repositories.Interfaces;
using TimesheetApp.Models.EntityModels;

namespace TimesheetApp.Data.Repositories
{
    public class TimesheetRepository : ITimesheetRepository
    {
        private readonly ApplicationDbContext _context;
        public TimesheetRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Timesheet> GetTimesheets() => _context.Timesheets.ToList();

        public void CreateTimesheet(Timesheet timesheet)
        {
            _context.Timesheets.Add(timesheet);
            _context.SaveChanges();
        }
    }
}

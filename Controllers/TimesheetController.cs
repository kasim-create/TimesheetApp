using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimesheetApp.BusinessLogic;
using TimesheetApp.Data;
using TimesheetApp.Models;
using TimesheetApp.Models.EntityModels;

namespace TimesheetApp.Controllers
{
    public class TimesheetController : Controller
    {
        private readonly TimesheetManager _timesheetManager;

       public TimesheetController(TimesheetManager timesheetManager)
        {
            _timesheetManager = timesheetManager;
        }

        [HttpGet("Timesheets")]
        public IActionResult Timesheets()
        {
            var timesheets = _timesheetManager.GetTimesheets()
                .Select(t => new TimesheetViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    Date = t.Date,
                    Project = t.Project,
                    DescriptionOfTasks = t.DescriptionOfTasks,
                    HoursWorked = t.HoursWorked
                })
                .ToList();

            if (timesheets != null)
            {
                return View(timesheets);
            }
            
            return View();
        }

        [HttpGet("CreateTimesheet")]
        public IActionResult CreateTimesheet()
        {
            return View();
        }

        [HttpPost("CreateTimesheet")]
        public IActionResult CreateTimesheet(TimesheetViewModel model)
        {
            if (ModelState.IsValid)
            {
                var timesheet = new Timesheet
                {
                    Name = model.Name,
                    Date = model.Date,
                    Project = model.Project,
                    DescriptionOfTasks = model.DescriptionOfTasks,
                    HoursWorked = model.HoursWorked
                };

                _timesheetManager.CreateTimesheet(timesheet);
                return RedirectToAction("Timesheets");
            }
            return View(model);
        }
    }
}

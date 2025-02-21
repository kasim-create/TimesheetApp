using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using TimesheetApp.BusinessLogic;
using TimesheetApp.BusinessLogic.Interfaces;
using TimesheetApp.Data;
using TimesheetApp.Models;
using TimesheetApp.Models.EntityModels;

namespace TimesheetApp.Controllers
{
    public class TimesheetController : Controller
    {
        private readonly TimesheetManager _timesheetManager;
        private readonly ICsvGenerator _csvGenerator;

        public TimesheetController(TimesheetManager timesheetManager, ICsvGenerator csvGenerator)
        {
            _timesheetManager = timesheetManager;
            _csvGenerator = csvGenerator;
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
                    HoursWorked = t.HoursWorked,
                    TotalHoursWorked = t.TotalHoursWorked,
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
                    HoursWorked = model.HoursWorked,
                    TotalHoursWorked = model.HoursWorked
                };

                _timesheetManager.CreateTimesheet(timesheet);
                return RedirectToAction("Timesheets");
            }
            return View(model);
        }

        [HttpGet("DownloadAllTimesheetsCsv")]
        public IActionResult DownloadAllTimesheetsCsv()
        {
            var timesheets = _timesheetManager.GetTimesheets();
            var csvContent = _csvGenerator.GenerateCsv(timesheets);
            var csvBytes = Encoding.UTF8.GetBytes(csvContent);
            var output = new MemoryStream(csvBytes);
            var fileName = $"Timesheets_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

            return File(output, "text/csv", fileName);
        }
    }
}
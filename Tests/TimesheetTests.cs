using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System.Text;
using TimesheetApp.BusinessLogic;
using TimesheetApp.Data.Repositories.Interfaces;
using TimesheetApp.Models.EntityModels;

namespace TimesheetApp.Tests
{
    [TestFixture]
    public class TimesheetTests
    {

        private Mock<ITimesheetRepository> _mockRepo;
        private TimesheetManager _timesheetManager;

        [SetUp]
        public void SetUp()
        {
            _mockRepo = new Mock<ITimesheetRepository>();
            _timesheetManager = new TimesheetManager(_mockRepo.Object);
        }

        [Test]
        public void CreateTimesheet_Success()
        {
            var timesheet = new Timesheet
            {
                Name = "Tester",
                Date = DateTime.Now,
                Project = "TestProject",
                HoursWorked = 8,
                DescriptionOfTasks = "Testing"
            };

            _timesheetManager.CreateTimesheet(timesheet);

            _mockRepo.Verify(r => r.CreateTimesheet(It.IsAny<Timesheet>()), Times.Once);
        }

        [Test]
        public void GetTimesheets_ReturnsCorrectTimesheets()
        {

            var timesheets = new List<Timesheet>
                {
                    new Timesheet { Id = 1, Name = "Tester1", Date = DateTime.Now, Project = "TestProject1", HoursWorked = 8, DescriptionOfTasks = "Testing1" },
                    new Timesheet { Id = 2, Name = "Tester2", Date = DateTime.Now, Project = "TestProject2", HoursWorked = 7, DescriptionOfTasks = "Testing2" }
                };

            _mockRepo.Setup(r => r.GetTimesheets()).Returns(timesheets);

            var result = _timesheetManager.GetTimesheets();

            ClassicAssert.AreEqual(2, result.Count);
            ClassicAssert.AreEqual("Tester1", result[0].Name);
            ClassicAssert.AreEqual("Tester2", result[1].Name);
        }

        [Test]
        public void GenerateTimesheetCSV_Success()
        {
            var timesheets = new List<Timesheet>
        {
            new Timesheet { Id = 1, Name = "Tester1", Date = DateTime.Now, Project = "TestProject1", HoursWorked = 8, DescriptionOfTasks = "Testing1" },
            new Timesheet { Id = 2, Name = "Tester2", Date = DateTime.Now, Project = "TestProject2", HoursWorked = 7, DescriptionOfTasks = "Testing2" }
        };

            var csv = new StringBuilder();
            csv.AppendLine("Id,Name,Date,Project,DescriptionOfTasks,HoursWorked");

            foreach (var timesheet in timesheets)
            {
                csv.AppendLine($"{timesheet.Id},{timesheet.Name},{timesheet.Date.ToShortDateString()},{timesheet.Project},{timesheet.DescriptionOfTasks},{timesheet.HoursWorked}");
            }

            var csvBytes = Encoding.UTF8.GetBytes(csv.ToString());
            var output = new MemoryStream(csvBytes);
            var fileName = $"Timesheets_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

            var result = new FileStreamResult(output, "text/csv") { FileDownloadName = fileName };

            ClassicAssert.IsNotNull(result);
            ClassicAssert.AreEqual("text/csv", result.ContentType);
            ClassicAssert.AreEqual(fileName, result.FileDownloadName);

            using (var reader = new StreamReader(result.FileStream))
            {
                var generatedCsvContent = reader.ReadToEnd();
                ClassicAssert.AreEqual(csv.ToString(), generatedCsvContent);
            }
        }
    }
}

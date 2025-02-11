namespace TimesheetApp.Models
{
    public class Timesheet
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Project { get; set; }

        public string DescriptionOfTasks { get; set; }

        public int HoursWorked { get; set; }
    }
}

namespace apicampusjob.Models.DataInfo
{
    public class StudentAvailabilityDTO:BaseDTO
    {
        public string StudentUuid { get; set; }
        public string DayOfWeek { get; set; } = null!;

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }
    }
}

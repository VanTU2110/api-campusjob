namespace apicampusjob.Models.DataInfo
{
    public class ScheduleInfoCatalogDTO:BaseDTO
    {
        public JobInfoCatalogDTO Job { get; set; }
        public string DayOfWeek { get; set; } = null!;
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}

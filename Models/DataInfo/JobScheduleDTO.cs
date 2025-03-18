namespace apicampusjob.Models.DataInfo
{
    public class JobScheduleDTO: BaseDTO
    {
        public string JobUuid { get; set; } = null!;

        public string DayOfWeek { get; set; } = null!;

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

    }
}

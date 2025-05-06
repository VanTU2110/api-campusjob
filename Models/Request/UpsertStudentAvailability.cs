namespace apicampusjob.Models.Request
{
    public class UpsertStudentAvailability: UuidRequest
    {
        public string studentUuid { get; set; }
        public string dayOfWeek { get; set; }
        public TimeOnly startTime { get; set; }
        public TimeOnly endTime { get; set; }
    }
}

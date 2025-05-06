namespace apicampusjob.Models.Request
{
    public class GetJobsByScheduleRequest:BaseKeywordPageRequest
    {
        public string dayOfWeek { get; set; }
        public TimeOnly? startTime {  get; set; }
        public TimeOnly? endTime { get; set; }
    }
}

namespace apicampusjob.Models.Request
{
    public class GetPageListSchedule : BaseKeywordPageRequest
    {
        public string? Job_Uuid { get; set; }
        public string? Day_of_week { get; set; }
        public TimeOnly? Start_Time { get; set; }
        public TimeOnly? End_Time { get;set; }
    }
}

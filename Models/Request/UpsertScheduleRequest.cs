namespace apicampusjob.Models.Request
{
    public class UpsertScheduleRequest:UuidRequest
    {
        public string Job_Uuid { get; set; }
        public string Day_of_week { get; set; }
        public TimeOnly Start_time { get; set; }
        public TimeOnly End_time { get; set; }
    }
}

namespace apicampusjob.Models.Request
{
    public class UpsertStudentAvailability: UuidRequest
    {
        public string Student_Uuid { get; set; }
        public string Day_of_week { get; set; }
        public TimeOnly Start_time { get; set; }
        public TimeOnly End_time { get; set; }
    }
}

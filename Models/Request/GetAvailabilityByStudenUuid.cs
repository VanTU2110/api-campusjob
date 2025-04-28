namespace apicampusjob.Models.Request
{
    public class GetAvailabilityByStudenUuid:BaseKeywordRequest
    {
        public string Student_Uuid { get; set; }
    }
}

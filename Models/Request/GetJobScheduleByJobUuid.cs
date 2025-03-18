namespace apicampusjob.Models.Request
{
    public class GetListJobScheduleByJobUuid:BaseKeywordRequest
    {
        public string JobUuid { get; set; }
    }
}

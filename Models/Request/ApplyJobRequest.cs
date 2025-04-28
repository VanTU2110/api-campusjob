namespace apicampusjob.Models.Request
{
    public class ApplyJobRequest:UuidRequest
    {
        public string StudentUuid { get; set; } = null!;

        public string JobUuid { get; set; } = null!;
        public string? CoverLetter { get; set; }
    }
}

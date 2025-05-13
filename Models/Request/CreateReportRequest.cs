namespace apicampusjob.Models.Request
{
    public class CreateReportRequest:UuidRequest
    {
        public string ReporterUuid { get; set; } = null!;

        public string TargetType { get; set; } = null!;

        public string TargetUuid { get; set; } = null!;

        public string Reason { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

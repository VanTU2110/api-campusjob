namespace apicampusjob.Models.DataInfo
{
    public class ReportDTO:BaseDTO
    {
        public string ReporterUuid { get; set; } = null!;

        public string TargetType { get; set; } = null!;

        public string TargetUuid { get; set; } = null!;

        public string Reason { get; set; } = null!;

        public string? Description { get; set; }

        public string Status { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
    }
}

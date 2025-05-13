using static apicampusjob.Enums.EnumDatabase;

namespace apicampusjob.Models.Request
{
    public class UpdateReportStatus
    {
        public string ReportUuid { get; set; } = string.Empty;
        public ReportStatus NewStatus { get; set; }
    }
}

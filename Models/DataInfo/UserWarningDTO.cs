namespace apicampusjob.Models.DataInfo
{
    public class UserWarningDTO:BaseDTO
    {
        public string TargetType { get; set; } = null!;

        public string TargetUuid { get; set; } = null!;

        public string Messages { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
    }
}

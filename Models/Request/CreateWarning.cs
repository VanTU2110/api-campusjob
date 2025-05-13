namespace apicampusjob.Models.Request
{
    public class CreateWarning:UuidRequest
    {
        public string TargetType { get; set; } = null!;

        public string TargetUuid { get; set; } = null!;

        public string Messages { get; set; } = null!;
    }
}

namespace apicampusjob.Models.Request
{
    public class InsertCVRequest:UuidRequest
    {
        public string StudentUuid { get; set; } = null!;

        public string CloudinaryPublicId { get; set; } = null!;

        public string Url { get; set; } = null!;
    }
}

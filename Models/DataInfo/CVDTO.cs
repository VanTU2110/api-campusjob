namespace apicampusjob.Models.DataInfo
{
    public class CVDTO:BaseDTO
    {
        public string StudentUuid { get; set; } = null!;

        public string CloudinaryPublicId { get; set; } = null!;

        public string Url { get; set; } = null!;

        public DateTime UploadAt { get; set; }
    }
}

namespace apicampusjob.Models.DataInfo
{
    public class ConversationDTO:BaseDTO
    {
        public string StudentUuid { get; set; } = null!;
        public string CompanyUuid { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}

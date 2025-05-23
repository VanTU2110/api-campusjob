namespace apicampusjob.Models.DataInfo
{
    public class ConversationDTO:BaseDTO
    {
        public InfoCatalogDTO Student { get; set; } = null!;
        public InfoCatalogDTO Company { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}

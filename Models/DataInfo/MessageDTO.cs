namespace apicampusjob.Models.DataInfo
{
    public class MessageDTO:BaseDTO
    {
        public string Content { get; set; }
        public string SenderUuid { get; set; }
        public DateTime SentAt { get; set; }
    }
}

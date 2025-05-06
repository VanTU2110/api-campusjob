namespace apicampusjob.Models.Request
{
    public class AddNoteToApplicationRequest:UuidRequest
    {
        public string Note {  get; set; } = string.Empty;
    }
}

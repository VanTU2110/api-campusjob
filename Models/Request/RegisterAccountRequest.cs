namespace apicampusjob.Models.Request
{
    public class RegisterAccountRequest : UuidRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
}

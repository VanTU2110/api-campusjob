namespace apicampusjob.Models.Request
{
    public class UpsertCompaniesRequest : UuidRequest
    {
        public string UserUuid { get; set; } = null!;
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Matp { get; set; } = null!;

        public string Maqh { get; set; } = null!;

        public string Xaid { get; set; } = null!;
    }
}

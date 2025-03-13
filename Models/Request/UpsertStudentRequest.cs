namespace apicampusjob.Models.Request
{
    public class UpsertStudentRequest:UuidRequest
    {
        public string UserUuid { get; set; } = null!;
        public string? Fullname { get; set; }
        public string? PhoneNumber { get; set; }
        public sbyte Gender { get; set; }

        public DateOnly Birthday { get; set; }

        public string? University { get; set; }

        public string? Major { get; set; }
        public string Matp { get; set; } = null!;

        public string Maqh { get; set; } = null!;

        public string Xaid { get; set; } = null!;
    }
}

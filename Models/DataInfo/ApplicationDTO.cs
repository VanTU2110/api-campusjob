using apicampusjob.Models.DataInfo;

namespace apicampusjob.Models.DataInfo
{
    public class ApplicationDTO:BaseDTO
    {
        public string StudentUuid { get; set; } = null!;

        public string JobUuid { get; set; } = null!;

        public string Status { get; set; } = null!;

        public string? CoverLetter { get; set; }

        public string? Note { get; set; }

        public DateTime? AppliedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

    }
    public class ApplicationAdminDTO : ApplicationDTO
    {
        public bool? IsActive { get; set; }
    }

}

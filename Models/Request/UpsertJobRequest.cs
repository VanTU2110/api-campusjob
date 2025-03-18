namespace apicampusjob.Models.Request
{
    public class UpsertJobRequest:UuidRequest
    {
        public string CompanyUuid { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string? Description { get; set; }
        public string JobType { get; set; }

        public string SalaryType { get; set; } = null!;

        public decimal? SalaryMin { get; set; }

        public decimal? SalaryMax { get; set; }
        public decimal? SalaryFixed { get; set; }

        public string? Currency { get; set; }

        public string? Requirements { get; set; }
    }
}

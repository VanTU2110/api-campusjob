namespace apicampusjob.Models.DataInfo
{
    public class JobDTO:BaseDTO
    {
        public InfoCatalogDTO Company {  get; set; }
        public string Tittle { get; set; } = null!;
        public string? Description { get; set; }
        public string JobType { get; set; }

        public string SalaryType { get; set; } = null!;

        public decimal? SalaryMin { get; set; }

        public decimal? SalaryMax { get; set; }
        public decimal? SalaryFixed { get; set; }

        public string? Currency { get; set; }

        public string? Requirements { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Updated { get; set; }
        public List<JobScheduleDTO> Schedule { get; set; }
    }
}

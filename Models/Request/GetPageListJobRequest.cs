namespace apicampusjob.Models.Request
{
    public class GetPageListJobRequest : BaseKeywordPageRequest
    {
        public string? CompanyUuid { get; set; }
        public string? JobType { get; set; }
        public string? SalaryType { get; set; }
        public decimal? SalaryMin { get; set; }
        public decimal? SalaryMax { get; set; }
        public decimal? SalaryFixed { get; set; }

    }
}

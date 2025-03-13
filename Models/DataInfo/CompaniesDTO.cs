namespace apicampusjob.Models.DataInfo
{
    public class CompaniesDTO:BaseDTO
    {
        public string UserUuid { get; set; } = null!;
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Email {  get; set; }
        public string? PhoneNumber { get; set; }
        public InfoCatalogDTO TP { get; set; } = null!;
        public InfoCatalogDTO QH { get; set; } = null!;
        public InfoCatalogDTO Xa { get; set; } = null!;
    }
}

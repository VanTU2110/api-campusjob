﻿namespace apicampusjob.Models.DataInfo
{
    public class Student_Response:BaseDTO
    {
        public string UserUuid { get; set; } = null!;

        public string? Fullname { get; set; }

        public string? PhoneNumber { get; set; }
        public sbyte Gender { get; set; }
        public DateOnly Birthday { get; set; }
        public string? University { get; set; }
        public string? Major { get; set; }
        public InfoCatalogDTO TP { get; set; } = null!;
        public InfoCatalogDTO QH { get; set; } = null!;
        public InfoCatalogDTO Xa { get; set; } = null!;
    }
}

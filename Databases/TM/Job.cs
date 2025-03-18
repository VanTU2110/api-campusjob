using System;
using System.Collections.Generic;

namespace apicampusjob.Databases.TM;

public partial class Job
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public string CompanyUuid { get; set; } = null!;

    public string Tittle { get; set; } = null!;

    public string? Description { get; set; }

    public string? JobType { get; set; }

    public string SalaryType { get; set; } = null!;

    public decimal? SalaryMin { get; set; }

    public decimal? SalaryMax { get; set; }

    public decimal? SalaryFixed { get; set; }

    public string? Currency { get; set; }

    public string? Requirements { get; set; }

    public DateTime? Created { get; set; }

    public DateTime? Updated { get; set; }

    public virtual Companies CompanyUu { get; set; } = null!;

    public virtual ICollection<JobSchedule> JobSchedule { get; set; } = new List<JobSchedule>();
}

using apicampusjob.Databases.TM;
using apicampusjob.Models.Request;

namespace apicampusjob.Repository
{
    public interface IReportRepository:IBaseRepository
    {
        List<Report> GetPageListReport(GetPageListReport report);
        Report GetDetailReport(string uuid);
    }
    public class ReportRepository : BaseRepository, IReportRepository
    {
        public ReportRepository(DBContext dbContext) : base(dbContext)
        {
        }

        public Report GetDetailReport(string uuid)
        {
            return _dbContext.Report.FirstOrDefault(x => x.Uuid == uuid);
        }

        public List<Report> GetPageListReport(GetPageListReport request)
        {
            return _dbContext.Report
                .Where(x=>string.IsNullOrEmpty(request.TargetUuid) || x.TargetUuid==request.TargetUuid)
                .Where(x =>string.IsNullOrEmpty(request.TargetType)|| x.TargetType==request.TargetType)
                .ToList();
        }
    }
}

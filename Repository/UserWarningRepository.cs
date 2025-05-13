using apicampusjob.Databases.TM;
using apicampusjob.Models.Request;

namespace apicampusjob.Repository
{
    public interface IUserWarningRepository:IBaseRepository
    {
        List<UserWarning> GetPageListWarning(GetPageListWarning request);
        
    }
    public class UserWarningRepository : BaseRepository, IUserWarningRepository
    {
        public UserWarningRepository(DBContext dbContext) : base(dbContext)
        {
        }

        public List<UserWarning> GetPageListWarning(GetPageListWarning request)
        {
           return _dbContext.UserWarning
                .Where(x=>string.IsNullOrEmpty(request.TargetType) || x.TargetType == request.TargetType)
                 .Where(x => string.IsNullOrEmpty(request.TargetUuid) || x.TargetUuid == request.TargetUuid)
                 .ToList();

        }
    }
}

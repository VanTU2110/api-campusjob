using apicampusjob.Databases.TM;
using apicampusjob.Models.Request;

namespace apicampusjob.Repository
{
    public interface IRegionsRepository : IBaseRepository
    {
        List<DevvnTinhthanhpho> GetPageListProvinsie(GetProvinsie request);
        List<DevvnQuanhuyen> GetPageListDistrictbyProvinsie(GetPageListDistrictbyProvinsieRequest request);
        List<DevvnXaphuongthitran> GetPageListCommunebyDistrict (GetPageListCommunebyDistrictRequest request);

    }
    public class RegionsRepository : BaseRepository, IRegionsRepository
    {
        public RegionsRepository(DBContext dbContext) : base(dbContext)
        {
        }

        public List<DevvnXaphuongthitran> GetPageListCommunebyDistrict(GetPageListCommunebyDistrictRequest request)
        {
             return _dbContext.DevvnXaphuongthitran.Where(x =>x.Maqh == request.maqh).Where(x => string.IsNullOrEmpty(request.Keyword) || x.Name.Contains(request.Keyword)).ToList();
            
        }

        public List<DevvnQuanhuyen> GetPageListDistrictbyProvinsie(GetPageListDistrictbyProvinsieRequest request)
        {
            return _dbContext.DevvnQuanhuyen.Where(x => x.Matp == request.Matp).Where(x => string.IsNullOrEmpty(request.Keyword) || x.Name.Contains(request.Keyword)).ToList();
        }

        public List<DevvnTinhthanhpho> GetPageListProvinsie(GetProvinsie request)
        {
            return _dbContext.DevvnTinhthanhpho.Where(x => string.IsNullOrEmpty(request.Keyword) || x.Name.Contains(request.Keyword)).ToList();
        }
    }
}

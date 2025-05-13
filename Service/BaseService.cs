using AutoMapper;
using apicampusjob.Databases.TM;
using apicampusjob.Models.Response;

namespace apicampusjob.Service
{
    public class BaseService
    {
        protected readonly DBContext _dbContext;
        protected readonly IMapper _mapper;
        protected readonly IConfiguration _configuration;

     

        public BaseService(DBContext dbContext, IMapper mapper, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _configuration = configuration;
        }

        public DBContext DbContext { get; }
        public IMapper Mapper { get; }

        protected TResult ExecuteInTransaction<TResult>(Func<TResult> action)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var result = action();
                    transaction.Commit();
                    return result;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        protected Task<BaseResponse> ExecuteInTransactionAsync(Func<Task<BaseResponse>> action)
        {
            return ExecuteInTransactionWrapperAsync(action);
        }

        private async Task<BaseResponse> ExecuteInTransactionWrapperAsync(Func<Task<BaseResponse>> action)
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var result = await action();
                await transaction.CommitAsync();
                return result;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


    }
}

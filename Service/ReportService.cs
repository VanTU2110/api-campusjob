using apicampusjob.Configuaration;
using apicampusjob.Databases.TM;
using apicampusjob.Enums;
using apicampusjob.Extensions;
using apicampusjob.Models.DataInfo;
using apicampusjob.Models.Request;
using apicampusjob.Models.Response;
using apicampusjob.Repository;
using AutoMapper;

namespace apicampusjob.Service
{
    public interface IReportService
    {
        BaseResponse CreateReport(CreateReportRequest request);
        BaseResponseMessagePage<ReportDTO> GetPageListReport(GetPageListReport request);
        BaseResponseMessage<ReportDTO> GetDetailReport(string uuid);
        BaseResponse UpdateStatus(UpdateReportStatus request);
    }
    public class ReportService : BaseService, IReportService
    {
        private readonly IReportRepository _reportRepository;
        public ReportService(DBContext dbContext, IMapper mapper, IConfiguration configuration, IReportRepository reportRepository) : base(dbContext, mapper, configuration)
        {
            _reportRepository = reportRepository;
        }

        public BaseResponse CreateReport(CreateReportRequest request)
        {
            var newReport = new Report
            {
                ReporterUuid = request.ReporterUuid,
                TargetType = request.TargetType,
                TargetUuid = request.TargetUuid ,
                Reason = request.Reason,
                Description = request.Description,
            };
            return ExecuteInTransaction(() =>
            {
                return new BaseResponseMessage<ReportDTO>
                {
                    Data = _mapper.Map<ReportDTO>(_reportRepository.CreateItem(newReport))
                };
            });
        }

        public BaseResponseMessage<ReportDTO> GetDetailReport(string uuid)
        {
            var response = new BaseResponseMessage<ReportDTO>();
            var report = _reportRepository.GetDetailReport(uuid);
            if (report == null)
            {
                throw new ErrorException(ErrorCode.REPORT_NOT_FOUND);
            }
            var detailReportDTO = _mapper.Map<ReportDTO>(report);
            response.Data = detailReportDTO;
            return response;
        }

        public BaseResponseMessagePage<ReportDTO> GetPageListReport(GetPageListReport request)
        {
            var response = new BaseResponseMessagePage<ReportDTO>();
            var lstReport = _reportRepository.GetPageListReport(request);
            int count = lstReport.Count;
            if (lstReport != null && count > 0)
            {
                var result = lstReport.OrderByDescending(x => x.CreatedAt).TakePage(request.Page, request.PageSize);
                var lstReportDTO = _mapper.Map<List<ReportDTO>>(result);

                response.Data.Items = lstReportDTO;
                response.Data.Pagination = new Paginations()
                {
                    TotalPage = result.TotalPages,
                    TotalCount = result.TotalCount,
                };
            }
            return response;
        }

        public BaseResponse UpdateStatus(UpdateReportStatus request)
        {
            return ExecuteInTransaction(() =>
            {
                var report = _reportRepository.GetDetailReport(request.ReportUuid);
                if (report == null)
                {
                    throw new ErrorException(ErrorCode.REPORT_NOT_FOUND); // hoặc REPORT_NOT_FOUND
                }

                string newStatus = request.NewStatus.ToString().ToLower();

                if (report.Status == newStatus)
                {
                    throw new ErrorException(ErrorCode.NO_CHANGE); // Trạng thái không thay đổi
                }

                report.Status = newStatus;
                report.UpdatedAt = DateTime.Now;

                _reportRepository.UpdateItem(report);

                return new BaseResponseMessage<ReportDTO>
                {
                    Data = _mapper.Map<ReportDTO>(report)
                };
            });
        }

    }
}

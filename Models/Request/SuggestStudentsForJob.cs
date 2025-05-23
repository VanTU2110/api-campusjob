using apicampusjob.Models.BaseRequest;

namespace apicampusjob.Models.Request
{
    public class SuggestStudentsForJob:DpsPagingParamBase
    {
        public string jobUuid {  get; set; }
    }
}

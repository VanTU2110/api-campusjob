using apicampusjob.Models.BaseRequest;
using System.ComponentModel.DataAnnotations;

namespace apicampusjob.Models.Request
{
    public class UuidRequest : DpsParamBase
    {
       
        public string? Uuid { get; set; }
    }
}

using System.ComponentModel;

namespace apicampusjob.Enums
{
    public class EnumDatabase
    {
        public enum edAccountType
        {
            USER = 1,
            IDOL,
            ADMIN,
            MANAGER,
        }

        public enum edAccountState
        {
            [Description("Bị khóa")]
            LOCK = 0,
            [Description("Đang hoạt động")]
            ACTIVE,
        }

        public enum edIsEnable
        {
            [Description("Đã xóa")]
            FALSE,
            [Description("Đang tồn tại")]
            TRUE,
        }

        public enum edActionAuditType
        {
            INSERT = 1,
            UPDATE,
            DELETE
        }
        public enum edStatusOrder
        {
            [Description("Chờ xử lí")]
            PENDING = 0,
            [Description("Đang xử lí")]
            PROCESSING = 1,
            [Description("Vận chuyển")]
            SHIP = 2,
            [Description("Đã thanh toán")]
            PAID = 3,
        }
        public enum edStatusType
        {
            [Description("Bị khóa")]
            LOCK = 0,
            [Description("Đang hoạt động")]
            ACTIVE,
        }
    }
}

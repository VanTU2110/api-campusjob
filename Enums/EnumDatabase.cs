﻿using System.ComponentModel;

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
      
        public enum edStatusType
        {
            [Description("Bị khóa")]
            LOCK = 0,
            [Description("Đang hoạt động")]
            ACTIVE,
        }
        public enum ApplicationStatus
        {
            Pending,
            Interviewing,
            Accepted,
            Rejected,
            Cancelled,
            Hired
        }
        public enum ReportStatus
        {
            Pending,
            Resolved,
            Rejected
        }
        public enum ReportReason
        {
            FakeInformation,
            ScamFraud,
            Inappropriate,
            Spam,
            Duplicate,
            WrongCategory,
            Offensive,
            Other
        }

    }
}

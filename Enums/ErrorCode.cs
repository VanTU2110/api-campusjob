using System.ComponentModel;

namespace apicampusjob.Enums
{
    public enum ErrorCode
    {
        [Description("Failed")] FAILED = -1,
        [Description("Success")] SUCCESS = 0,
        [Description("Token Invalid")] TOKEN_INVALID = 2,
        [Description("System error")] SYSTEM_ERROR = 3,
        [Description("Database failed")] DB_FAILED = 4,
        [Description("Xung đột dữ liệu")] CONFLICT =302,
        [Description("Thư mục chứa ảnh chưa được cấu hình")]
        FOLDER_IMAGE_NOT_FOUND = 5,

        [Description("Định dạng tập tin không được hỗ trợ")]
        DOES_NOT_SUPPORT_FILE_FORMAT = 6,
        [Description("Not found")] NOT_FOUND = 7,
        [Description("Định dạng sai!")] INVALID_PARAM = 8,
        [Description("Exists")] EXISTS = 9,
        [Description("Key cert invalid")] INVALID_CERT = 10,
        [Description("Bad request")] BAD_REQUEST = 400,
        [Description("Unauthorization")] UNAUTHOR = 401,
        [Description("Mật khẩu không chính xác. Vui lòng thử lại")]
        INVALID_PASS = 24,
        [Description("Tài khoản không tồn tại. Vui lòng kiểm tra lại")]
        ACCOUNT_NOTFOUND = 25,
        [Description("Tài khoản đã bị khóa. Vui lòng liên hệ Admin để biết thêm chi tiết")]
        ACCOUNT_LOCKED = 23,
        [Description("Email không hợp lệ ")]
        EMAIL_NOT_VALIDATOR,
        [Description("Mật khẩu không hợp lệ ")]
        PASSWORD_NOT_VALIDATOR,
        [Description("Email này đã được sử dụng ")]
        EMAIL_IS_USED,
        [Description("Khong tim thay email nay")]
        EMAIL_NOT_FOUND,
        [Description("Password không đúng")]
        INVALID_CREDENTIALS,
        [Description("Không tìm thấy sinh viên này")]
        STUDENT_NOT_FOUND,
        [Description("Đã tồn tại thông tin về sinh viên này")]
        STUDENT_ALREADY_EXISTS,
        [Description("Khong tim thay USER")]
        USER_NOT_FOUND,
        [Description("Khong tim thay dia chi nay")]
        ADDRESS_NOT_FOUND,
        [Description("đã tồn tại thông tin về công ty này")]
        COMPANY_ALREADY_EXIT,
        [Description("Không tìm thấy công ty này")]
        COMPANY_NOT_FOUND,
        [Description("Khong tim thay cong viec")]
        REPORT_NOT_FOUND,
        [Description("Khong tim thay danh sach lich lam viec cua cong viec nay")]
        SCHEDULE_NOT_FOUND,
        [Description("Khong tim thay CV")]
        CV_NOT_FOUND,
        [Description("Khong tim thay lich ranh")]
        AVAILABLITY_NOT_FOUND,
        [Description("Khong tim thay ki nang")]
        SKILL_NOT_FOUND,
        [Description("Khong tim thay i nang cong viec")]
        JOBSKILL_NOT_FOUND,
        [Description("Khong tim thay ki nang sinh vien")]
        STUDENTSKILL_NOT_FOUND,
        [Description("Ứng tuyển không tồn tại")]
        APPLICATION_NOT_FOUND,
        [Description("Trạng thái không hợp lệ.")]
        INVALID_STATUS_TRANSITION,
        [Description("Trạng thái hiện tại không hợp lệ trong hệ thống.")]
        INVALID_APPLICATION_STATUS,
        [Description("Khong the cap nhat trang thai")]
        CANNOT_CANCEL_APPLICATION,
        CONVERSATION_NOT_FOUND,
        NO_CHANGE,
    }
}
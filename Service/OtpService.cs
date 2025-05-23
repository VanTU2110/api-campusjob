using apicampusjob.Configuaration;
using apicampusjob.Enums;
using static apicampusjob.Service.UserServicecs;

namespace apicampusjob.Service
{
    public interface IOtpService
    {
        Task SendOtpAsync(string email);
        Task VerifyOtpAsync(string email, string otp);
    }
    public class OtpService : IOtpService
    {
        private static readonly Dictionary<string, (string Otp, DateTime Expiry)> OtpStorage = new Dictionary<string, (string, DateTime)>();
        private static readonly string FixedOtp = "123456"; // Mã OTP cố định
        private const int OtpExpiryMinutes = 1; // Thời gian hết hạn của mã OTP
        private readonly IUserService _userService;
        public OtpService(IUserService userService)
        {
            _userService = userService;
        }
        public static string GenerateAndStoreOtp(string email)
        {
            DateTime expiryTime = DateTime.Now.AddMinutes(OtpExpiryMinutes);
            OtpStorage[email] = (FixedOtp, expiryTime);
            return FixedOtp;
        }

        public static bool ValidateOtp(string email, string otp)
        {
            return otp == FixedOtp;
        }
        public static bool IsOtpExpired(string email, string otp)
        {
            // Lấy thời điểm tạo OTP
            if (OtpStorage.TryGetValue(email, out var otpData) && otpData.Otp == otp)
            {
                DateTime otpCreationTime = otpData.Expiry;
                // Tính thời gian đã trôi qua từ khi tạo OTP đến hiện tại
                var timeElapsed = DateTime.Now;
                // Kiểm tra xem thời gian đã trôi qua có vượt quá thời hạn của OTP không
                return timeElapsed > otpCreationTime;
            }
            // Trả về true nếu không thể tìm thấy thời gian tạo OTP
            return true;
        }

        public async Task SendOtpAsync(string email)
        {
            var user = _userService.GetDetailUserByEmail(email);
            if (user == null)
            { throw new ErrorException(ErrorCode.USER_NOT_FOUND); };

            DateTime expiryTime = DateTime.Now.AddMinutes(OtpExpiryMinutes);
            OtpStorage[email] = (FixedOtp, expiryTime);

            // TODO: Gửi mail hoặc in console (ở đây tôi in console)
            Console.WriteLine($"[OTP gửi tới {email}]: {FixedOtp}");

            await Task.CompletedTask; // giả lập async
        }

        public async Task VerifyOtpAsync(string email, string otp)
        {
            var user = _userService.GetDetailUserByEmail(email)
                       ?? throw new ErrorException(ErrorCode.USER_NOT_FOUND);

            if (!OtpStorage.TryGetValue(email, out var otpData))
                throw new ErrorException(ErrorCode.OTP_NOT_FOUND);

            if (DateTime.Now > otpData.Expiry)
                throw new ErrorException(ErrorCode.OTP_EXPIRED);

            if (otp != otpData.Otp)
                throw new ErrorException(ErrorCode.OTP_INVALID);

            OtpStorage.Remove(email);

            await Task.CompletedTask;
        }

    }
}

namespace apicampusjob.Models.DataInfo
{
    public class UserDTO:BaseDTO
    {
        public string Email { get; set; } = null!;
        public int Role { get; set; }
        public bool? isVerify { get; set; }
        /// <summary>
        /// 0-Khóa, 1-Đang hoạt động
        /// </summary>
        public sbyte Status { get; set; }
        public DateTime CreateAt { get; set; }
        // 0 : buyer , 1 : seller  
    }
}

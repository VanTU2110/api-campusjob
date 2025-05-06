namespace apicampusjob.Models.DataInfo
{
    public class LogInResp
    {
        public string Token { get; set; }
        public string Uuid { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int Role { get; set; }
        public bool? IsVerify { get; set; }
    }
}

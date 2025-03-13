using apicampusjob.Databases.TM;

namespace apicampusjob.Utils
{
    public class TokenInfo
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string UserUuid { get; set; } = string.Empty;
        public int Role { get; set; }

        private DateTime ExpiredDate { get; set; }


        public bool IsExpired()
        {
            return ExpiredDate < DateTime.Now;
        }

        public void ResetExpired()
        {
            ExpiredDate = DateTime.Now.AddMonths(1);
        }
    }
}

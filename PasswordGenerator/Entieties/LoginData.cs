using System;

namespace PasswordGenerator.Entieties
{
    public class LoginData
    {
        public string UserId { get; set; }
        public DateTime TimeStamp { get; set; }
        public Boolean PasswordUsed { get; set; }
    }
}

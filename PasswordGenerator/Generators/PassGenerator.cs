using System;
using System.Security.Cryptography;
using System.Text;
using PasswordGenerator.Entieties;
using PasswordGenerator.Repositories;

namespace PasswordGenerator.Generators
{
    public class PassGenerator
    {
        public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public LoginDataRepository Repository = new LoginDataRepository();

        public string Generate(string userId)
        {
            VerifyData(userId, DateTime.Now);
            return GeneratePassword(userId);
        }

        public void VerifyData(string userId, DateTime date)
        {
            var user = Repository.GetById(userId);
            if (user == null)
            {
                user = new LoginData() { UserId = userId, TimeStamp = date };
                Repository.Insert(user);
            }

            if (date <= user.TimeStamp.AddSeconds(30) && !user.PasswordUsed) return;
            user.TimeStamp = date;
            user.PasswordUsed = false;
            Repository.Update(user);
        }

        public string GeneratePassword(string userId, int digits = 6)
        {
            var user = Repository.GetById(userId);
            var currTime = user?.TimeStamp ?? DateTime.Now;

            var counter = BitConverter.GetBytes((long)(currTime - UnixEpoch).TotalSeconds);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(counter);

            var key = Encoding.ASCII.GetBytes(userId);
            var hmac = new HMACSHA1(key, true);
            var hash = hmac.ComputeHash(counter);
            var offset = hash[hash.Length - 1] & 0xf;
            var binary =
                ((hash[offset] & 0x7f) << 24)
                | ((hash[offset + 1] & 0xff) << 16)
                | ((hash[offset + 2] & 0xff) << 8)
                | (hash[offset + 3] & 0xff);
            var password = binary % (int)Math.Pow(10, digits);

            return password.ToString(new string('0', digits));
        }
    }
}

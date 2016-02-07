using System.Collections.Generic;
using System.Linq;
using PasswordGenerator.Entieties;

namespace PasswordGenerator.Repositories
{
    public class LoginDataRepository : IRepository
    {
        private readonly List<LoginData> _loginDataList;

        public LoginDataRepository()
        {
            _loginDataList = new List<LoginData>();
        }


        public IEnumerable<LoginData> GetAll()
        {
            return _loginDataList;
        }

        public LoginData GetById(string id)
        {
            return _loginDataList.SingleOrDefault(l => l.UserId == id);
        }

        public void Insert(LoginData data)
        {
            if (data != null && GetById(data.UserId) == null)
                _loginDataList.Add(data);
        }

        public void Update(LoginData data)
        {
            if (data == null) return;
            var oldUser = from users in _loginDataList
                where users.UserId == data.UserId
                select users;
            _loginDataList.Remove(oldUser.FirstOrDefault());

            _loginDataList.Add(data);
        }
    }
}

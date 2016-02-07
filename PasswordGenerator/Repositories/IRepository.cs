using System.Collections.Generic;
using PasswordGenerator.Entieties;

namespace PasswordGenerator.Repositories
{
    interface IRepository
    {
        IEnumerable<LoginData> GetAll();
        LoginData GetById(string id);
        void Insert(LoginData data);
        void Update(LoginData data);
    }
}

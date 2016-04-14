using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Oracle.DataAccess.Client;

using Sobits.DataEngine.Repo.Impl;
using Sobits.Story.Logic.BLL;

namespace Sobits.Story.Logic.Repo.Impl
{
    internal class UserRepo : AbstractRepo<User>, IUserRepo
    {
        public UserRepo(DataEngine.IDataSession session)
            : base(session)
        { }

        public User GetByEmail(String email, Guid password)
        {
            return GetAll().Where(x => x.Email == email &&
                                       x.Password == password)
                           .FirstOrDefault();
        }
    }
}

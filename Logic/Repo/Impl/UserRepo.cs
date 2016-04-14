using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Oracle.DataAccess.Client;

using Sobits.Story.Logic.BLL;
using NHibernate;

namespace Sobits.Story.Logic.Repo.Impl
{
    internal class UserRepo : BaseRepo<User>, IUserRepo
    {
        public UserRepo(ISession session)
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

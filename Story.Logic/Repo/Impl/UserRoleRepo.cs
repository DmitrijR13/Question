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
    internal class UserRoleRepo : AbstractRepo<UserRole>, IUserRoleRepo
    {
        public UserRoleRepo(DataEngine.IDataSession session)
            : base(session)
        { }

        public UserRole GetByUserAndRole(Int32 userID, String roleServiceName)
        {
            return GetAll().Where(x => x.User.ID == userID &&
                                       x.Role.ServiceName == roleServiceName)
                           .SingleOrDefault();
        }

        public UserRole GetByUserAndRole(Int32 userID, Int32 roleID)
        {
            return GetAll().Where(x => x.User.ID == userID &&
                                       x.Role.ID == roleID)
                           .SingleOrDefault();
        }

        public UserRole GetByUser(Int32 userID)
        {
            return GetAll().Where(x => x.User.ID == userID)
                           .SingleOrDefault();
        }
    }
}

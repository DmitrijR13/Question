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
    internal class PermissionRoleRepo : AbstractRepo<PermissionRole>, IPermissionRoleRepo
    {
        public PermissionRoleRepo(DataEngine.IDataSession session)
            : base(session)
        { }

        public IQueryable<Role> GetByPermission(Int32 permissionID)
        {
            return GetAll().Where(x => x.Permission.ID == permissionID)
                           .Select(x => x.Role);
        }

        public IQueryable<PermissionRole> GetByRole(Int32 roleID)
        {
            return GetAll().Where(x => x.Role.ID == roleID);
        }

        public PermissionRole GetByRoleAndPermission(Int32 roleID, Int32 permissionID)
        {
            return GetAll().Where(x => x.Role.ID == roleID && x.Permission.ID == permissionID).SingleOrDefault();
        }
    }
}

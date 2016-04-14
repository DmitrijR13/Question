using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Oracle.DataAccess.Client;

using NHibernate;
using Sobits.Story.Logic.BLL;

namespace Sobits.Story.Logic.Repo.Impl
{
    internal class PermissionRoleRepo : BaseRepo<PermissionRole>, IPermissionRoleRepo
    {
        public PermissionRoleRepo(ISession session)
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

        public override void DeleteList(List<PermissionRole> entities)
        {
            foreach (PermissionRole item in entities)
            {
                String querySql = String.Format(@"DELETE FROM tbl_PermissionRole dt WHERE dt.ID = {0}", item.ID);
                RunSQL(querySql);
            }
        }
    }
}

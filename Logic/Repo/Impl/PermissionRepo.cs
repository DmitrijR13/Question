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
    internal class PermissionRepo : BaseRepo<Permission>, IPermissionRepo
    {
        public PermissionRepo(ISession session)
            : base(session)
        { }

        public Permission GetByValue(String value)
        {
            return GetAll().Where(x => x.Value == value).SingleOrDefault();
        }
    }
}

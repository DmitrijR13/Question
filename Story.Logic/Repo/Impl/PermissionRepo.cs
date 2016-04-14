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
    internal class PermissionRepo : AbstractRepo<Permission>, IPermissionRepo
    {
        public PermissionRepo(DataEngine.IDataSession session)
            : base(session)
        { }

        public Permission GetByValue(String value)
        {
            return GetAll().Where(x => x.Value == value).SingleOrDefault();
        }
    }
}

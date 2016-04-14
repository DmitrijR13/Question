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
    internal class RoleRepo : AbstractRepo<Role>, IRoleRepo
    {
        public RoleRepo(DataEngine.IDataSession session)
            : base(session)
        { }

        public Role GetByServiceName(String serviceName)
        {
            return GetAll().Where(x => x.ServiceName == serviceName).SingleOrDefault();
        }
    }
}

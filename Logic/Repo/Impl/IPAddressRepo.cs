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
    internal class IPAddressRepo : BaseRepo<IPAddress>, IIPAddressRepo
    {
        public IPAddressRepo(ISession session)
            : base(session)
        { }

        public IPAddress GetByIPAddress(String ipAddress)
        {
            return GetAll().Where(x => x.Value == ipAddress).SingleOrDefault();
        }
    }
}
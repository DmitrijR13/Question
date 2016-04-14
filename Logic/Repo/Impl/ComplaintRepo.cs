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
    internal class ComplaintRepo : BaseRepo<Complaint>, IComplaintRepo
    {
        public ComplaintRepo(ISession session)
            : base(session)
        { }
    }
}

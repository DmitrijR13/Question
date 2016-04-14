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
    internal class ComplaintRepo : AbstractRepo<Complaint>, IComplaintRepo
    {
        public ComplaintRepo(DataEngine.IDataSession session)
            : base(session)
        { }
    }
}

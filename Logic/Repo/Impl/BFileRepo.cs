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
    internal class BFileRepo : BaseRepo<BFile>, IBFileRepo
    {
        public BFileRepo(ISession session)
            : base(session)
        { }
    }
}

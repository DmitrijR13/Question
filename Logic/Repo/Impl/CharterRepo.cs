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
    internal class CharterRepo : BaseRepo<Charter>, ICharterRepo
    {
        public CharterRepo(ISession session)
            : base(session)
        { }

        public IQueryable<Charter> GetWithoutDelete()
        {
            return GetAll().Where(x => !x.IsDelete);
        }
    }
}

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
    internal class CharterRepo : AbstractRepo<Charter>, ICharterRepo
    {
        public CharterRepo(DataEngine.IDataSession session)
            : base(session)
        { }

        public IQueryable<Charter> GetWithoutDelete()
        {
            return GetAll().Where(x => !x.IsDelete);
        }
    }
}

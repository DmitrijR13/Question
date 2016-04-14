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
    internal class SMOOrganizationRepo : AbstractRepo<SMOOrganization>, ISMOOrganizationRepo
    {
        public SMOOrganizationRepo(DataEngine.IDataSession session)
            : base(session)
        { }

        public IQueryable<SMOOrganization> GetByCodeAndName(String text)
        {
            Int32 code = 0;
            if (!Int32.TryParse(text, out code))
            {
                return GetAll().Where(x => x.Name.ToLower().Contains(text.ToLower()));
            }
            else
            {
                return GetAll().Where(x => x.Code == code);
            }
        }
    }
}

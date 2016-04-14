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
    internal class TypeCategoryComplaintRepo : BaseRepo<TypeCategoryComplaint>, ITypeCategoryComplaintRepo
    {
        public TypeCategoryComplaintRepo(ISession session)
            : base(session)
        { }

        public IQueryable<TypeCategoryComplaint> GetAllExists()
        {
            return GetAll().Where(x => !x.IsDelete);
        }
    }
}

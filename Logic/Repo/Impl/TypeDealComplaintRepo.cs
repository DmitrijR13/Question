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
    internal class TypeDealComplaintRepo : BaseRepo<TypeDealComplaint>, ITypeDealComplaintRepo
    {
        public TypeDealComplaintRepo(ISession session)
            : base(session)
        { }

        /// <summary>
        /// Получить все существующие
        /// </summary>
        /// <returns></returns>
        public IQueryable<TypeDealComplaint> GetAllExists()
        {
            return GetAll().Where(x => !x.IsDelete);
        }
    }
}

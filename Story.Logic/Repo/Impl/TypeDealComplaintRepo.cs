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
    internal class TypeDealComplaintRepo : AbstractRepo<TypeDealComplaint>, ITypeDealComplaintRepo
    {
        public TypeDealComplaintRepo(DataEngine.IDataSession session)
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

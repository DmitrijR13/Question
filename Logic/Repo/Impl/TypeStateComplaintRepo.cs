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
    internal class TypeStateComplaintRepo : BaseRepo<TypeStateComplaint>, ITypeStateComplaintRepo
    {
        public TypeStateComplaintRepo(ISession session)
            : base(session)
        { }

        public TypeStateComplaint GetFirstExists()
        {
            return GetAll().Where(x => !x.IsDelete).FirstOrDefault();
        }

        /// <summary>
        /// Получить все существующие
        /// </summary>
        /// <returns></returns>
        public IQueryable<TypeStateComplaint> GetAllExists()
        {
            return GetAll().Where(x => !x.IsDelete);
        }
    }
}

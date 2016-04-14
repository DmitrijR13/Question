using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sobits.Story.Logic.BLL;

namespace Sobits.Story.Logic.Repo
{
    /// <summary>
    /// Интерфейс репозитория сущности "TypeCategoryComplaint"
    /// </summary>
    public interface ITypeCategoryComplaintRepo : IRepo<TypeCategoryComplaint>
    {
        /// <summary>
        /// Получить все существующие
        /// </summary>
        /// <returns></returns>
        IQueryable<TypeCategoryComplaint> GetAllExists();
    }
}
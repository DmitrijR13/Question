using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sobits.Story.Logic.BLL;

namespace Sobits.Story.Logic.Repo
{
    /// <summary>
    /// Интерфейс репозитория сущности "TypeDealComplaint"
    /// </summary>
    public interface ITypeDealComplaintRepo : IRepo<TypeDealComplaint>
    {
        /// <summary>
        /// Получить все существующие
        /// </summary>
        /// <returns></returns>
        IQueryable<TypeDealComplaint> GetAllExists();
    }
}
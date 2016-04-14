using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sobits.DataEngine.Repo;
using Sobits.Story.Logic.BLL;

namespace Sobits.Story.Logic.Repo
{
    /// <summary>
    /// Интерфейс репозитория сущности "TypeStateComplaint"
    /// </summary>
    public interface ITypeStateComplaintRepo : IRepo<TypeStateComplaint>
    {
        /// <summary>
        /// Получить первое неудаленное (существующее) значение
        /// </summary>
        /// <returns></returns>
        TypeStateComplaint GetFirstExists();

        /// <summary>
        /// Получить все существующие
        /// </summary>
        /// <returns></returns>
        IQueryable<TypeStateComplaint> GetAllExists();
    }
}
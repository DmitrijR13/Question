using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sobits.DataEngine.Repo;
using Sobits.Story.Logic.BLL;

namespace Sobits.Story.Logic.Repo
{
    /// <summary>
    /// Интерфейс репозитория сущности "Charter"
    /// </summary>
    public interface ICharterRepo : IRepo<Charter>
    {
        /// <summary>
        /// Получить все неудаленные разделы
        /// </summary>
        /// <returns></returns>
        IQueryable<Charter> GetWithoutDelete();
    }
}
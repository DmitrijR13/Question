using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sobits.DataEngine.Repo;
using Sobits.Story.Logic.BLL;

namespace Sobits.Story.Logic.Repo
{
    /// <summary>
    /// Интерфейс репозитория сущности "MOOrganization"
    /// </summary>
    public interface IMOOrganizationRepo : IRepo<MOOrganization>
    {
        /// <summary>
        /// Получить организации по коду или названию
        /// </summary>
        /// <param name="text">Текст для фильтра</param>
        /// <returns></returns>
        IQueryable<MOOrganization> GetByCodeAndName(String text);
    }
}
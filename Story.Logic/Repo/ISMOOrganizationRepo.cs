using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sobits.DataEngine.Repo;
using Sobits.Story.Logic.BLL;

namespace Sobits.Story.Logic.Repo
{
    /// <summary>
    /// Интерфейс репозитория сущности "SMOOrganization"
    /// </summary>
    public interface ISMOOrganizationRepo : IRepo<SMOOrganization>
    {
        /// <summary>
        /// Получить организации по коду или названию
        /// </summary>
        /// <param name="text">Текст для фильтра</param>
        /// <returns></returns>
        IQueryable<SMOOrganization> GetByCodeAndName(String text);
    }
}
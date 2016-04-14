using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sobits.Story.Logic.BLL;

namespace Sobits.Story.Logic.Repo
{
    /// <summary>
    /// Интерфейс репозитория сущности "Role"
    /// </summary>
    public interface IRoleRepo : IRepo<Role>
    {
        /// <summary>
        /// Получить роль по служебному названию
        /// </summary>
        /// <param name="serviceName">Служебное название</param>
        /// <returns></returns>
        Role GetByServiceName(String serviceName);
    }
}
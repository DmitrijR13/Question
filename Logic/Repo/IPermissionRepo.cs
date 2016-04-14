using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sobits.Story.Logic.BLL;

namespace Sobits.Story.Logic.Repo
{
    /// <summary>
    /// Интерфейс репозитория сущности "Permission"
    /// </summary>
    public interface IPermissionRepo : IRepo<Permission>
    {
        /// <summary>
        /// Получение разрешения по значению
        /// </summary>
        /// <param name="value">Разрешение</param>
        /// <returns></returns>
        Permission GetByValue(String value);
    }
}
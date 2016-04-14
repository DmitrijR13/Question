using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sobits.DataEngine.Repo;
using Sobits.Story.Logic.BLL;

namespace Sobits.Story.Logic.Repo
{
    /// <summary>
    /// Интерфейс репозитория сущности "PermissionRole"
    /// </summary>
    public interface IPermissionRoleRepo : IRepo<PermissionRole>
    {
        /// <summary>
        /// Получение ролей по разрешению
        /// </summary>
        /// <param name="permissionID">Идентификатор разрешения</param>
        /// <returns></returns>
        IQueryable<Role> GetByPermission(Int32 permissionID);

        /// <summary>
        /// Получение связей по роли
        /// </summary>
        /// <param name="roleID">Идентификатор роли</param>
        /// <returns></returns>
        IQueryable<PermissionRole> GetByRole(Int32 roleID);

        /// <summary>
        /// Получение связей по роли и разрешению
        /// </summary>
        /// <param name="roleID">Идентификатор роли</param>
        /// <param name="permissionID">Идентификатор разрешения</param>
        /// <returns></returns>
        PermissionRole GetByRoleAndPermission(Int32 roleID, Int32 permissionID);
    }
}
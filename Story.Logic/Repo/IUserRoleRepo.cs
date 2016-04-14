using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sobits.DataEngine.Repo;
using Sobits.Story.Logic.BLL;

namespace Sobits.Story.Logic.Repo
{
    /// <summary>
    /// Интерфейс репозитория сущности "UserRole"
    /// </summary>
    public interface IUserRoleRepo : IRepo<UserRole>
    {
        /// <summary>
        /// Получить связь пользователя и роли
        /// </summary>
        /// <param name="userID">Идентификатор пользователя</param>
        /// <param name="roleServiceName">Служебное название роли</param>
        /// <returns></returns>
        UserRole GetByUserAndRole(Int32 userID, String roleServiceName);

        /// <summary>
        /// Получить связь пользователя и роли
        /// </summary>
        /// <param name="userID">Идентификатор пользователя</param>
        /// <param name="roleID">Идентификатор роли</param>
        /// <returns></returns>
        UserRole GetByUserAndRole(Int32 userID, Int32 roleID);

        /// <summary>
        /// Получить связь пользователя и роли
        /// </summary>
        /// <param name="userID">Идентификатор пользователя</param>
        /// <returns></returns>
        UserRole GetByUser(Int32 userID);
    }
}
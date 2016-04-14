using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

using Sobits.DataEngine;
using Sobits.DataEngine.BLL;
using Sobits.Story.Logic.Repo;

namespace Sobits.Story.Logic.BLL
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User : AbstractUser
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public virtual String FirstName { get; set; }

        /// <summary>
        /// Отчество пользователя
        /// </summary>
        public virtual String SecondName { get; set; }

        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public virtual String LastName { get; set; }

        /// <summary>
        /// Показывать анкету по вопросно?
        /// </summary>
        public virtual Boolean IsOneQuestion { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public virtual DateTime DateCreate { get; set; }

        /// <summary>
        /// Дата редактирования
        /// </summary>
        public virtual DateTime DateUpdate { get; set; }

        /// <summary>
        /// Пользователь создавший запись
        /// </summary>
        public virtual User UserCreate { get; set; }

        /// <summary>
        /// Пользователь редактировавший запись
        /// </summary>
        public virtual User UserUpdate { get; set; }

        /// <summary>
        /// Связь с ролями
        /// </summary>
        public virtual IList<UserRole> UserRoles { get; set; }

        #region methods

        /// <summary>
        /// Change password
        /// </summary>
        /// <param name="newPassword">New password</param>
        public virtual void ChangePassword(Guid newPassword)
        {
            Password = newPassword;
        }

        /// <summary>
        /// Definition permissions user
        /// </summary>
        public override Boolean IsRole(String permissionVal)
        {
            Permission permission = RepoFactory.Instance.GetRepo<IPermissionRepo>().GetByValue(permissionVal);

            if (permission == null) { return false; }

            List<Role> roles = RepoFactory.Instance.GetRepo<IPermissionRoleRepo>().GetByPermission(permission.ID).ToList();
            
            if (roles == null) { return false; }

            if (roles.Select(x => x.ID)
                     .Contains(this.UserRoles
                                   .Select(x => x.Role.ID)
                                   .FirstOrDefault()))
            {
                return true;
            }

            return false;
        }

        #endregion methods

    }
}
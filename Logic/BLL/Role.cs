using Sobits.WebEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sobits.Story.Logic.BLL
{
    /// <summary>
    /// Роль пользователя
    /// </summary>
    public class Role : AbstractEntity
    {
        /// <summary>
        /// Название роли
        /// </summary>
        public virtual String Name { get; set; }

        /// <summary>
        /// Служебное название роли
        /// </summary>
        public virtual String ServiceName { get; set; }

        /// <summary>
        /// Список связей с разрешениями
        /// </summary>
        public virtual IList<PermissionRole> PermissionRoles { get; set; }
    }
}

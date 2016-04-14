using Sobits.WebEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sobits.Story.Logic.BLL
{
    /// <summary>
    /// Связь роли и пользователя
    /// </summary>
    public class UserRole : AbstractEntity
    {
        /// <summary>
        /// Пользователь
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Роль
        /// </summary>
        public virtual Role Role { get; set; }
    }
}

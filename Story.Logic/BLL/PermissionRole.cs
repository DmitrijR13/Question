using Sobits.DataEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sobits.Story.Logic.BLL
{
    /// <summary>
    /// Связь разрешений и ролей
    /// </summary>
    public class PermissionRole : AbstractEntity
    {
        /// <summary>
        /// Роль
        /// </summary>
        public virtual Role Role { get; set; }

        /// <summary>
        /// Разрешение
        /// </summary>
        public virtual Permission Permission { get; set; }
    }
}

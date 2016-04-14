using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Questionnaire.WebSite.Models
{
    /// <summary>
    /// Модель ролей
    /// </summary>
    public class RoleModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Int32 ID { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        [Required(ErrorMessage = "*")]
        public String Name { get; set; }

        /// <summary>
        /// Разрешения
        /// </summary>
        public List<PermissionModel> Permissions { get; set; }
    }
}
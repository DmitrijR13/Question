using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire.WebSite.Models
{
    /// <summary>
    /// Модель разрешений
    /// </summary>
    public class PermissionModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Int32 ID { get; set; }

        /// <summary>
        /// Значение
        /// </summary>
        public String Value { get; set; }

        /// <summary>
        /// Выбрано?
        /// </summary>
        public Boolean IsChecked { get; set; }
    }
}
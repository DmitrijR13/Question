using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Questionnaire.WebSite.Models
{
    /// <summary>
    /// Модель МО организации
    /// </summary>
    public class MOOrganizationModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Int32 ID { get; set; }

        /// <summary>
        /// Код
        /// </summary>
        [Required(ErrorMessage = "*")]
        public Int32 Code { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        [Required(ErrorMessage = "*")]
        public String Name { get; set; }
    }
}
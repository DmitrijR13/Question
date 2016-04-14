using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Questionnaire.WebSite.Models
{
    /// <summary>
    /// Модель категорий
    /// </summary>
    public class CharterModel
    {
        /// <summary>
        /// Идентификатор записи Charter 
        /// </summary>
        public Int32 ID { get; set; }

        /// <summary>
        /// Название категории 
        /// </summary>
        [Required(ErrorMessage = "*")]
        public String Name { get; set; }

        /// <summary>
        /// Идентификатор картинки
        /// </summary>
        public Int32 ImageID { get; set; }

        /// <summary>
        /// Название картинки
        /// </summary>
        public String ImageName { get; set; }

        /// <summary>
        /// Картинка
        /// </summary>
        public HttpPostedFileBase Image { get; set; }
    }
}
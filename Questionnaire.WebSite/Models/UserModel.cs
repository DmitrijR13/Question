using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Questionnaire.WebSite.Models
{
    /// <summary>
    /// Модель пользователей
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Int32 ID { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [Required(ErrorMessage="*")]
        public String FirstName { get; set; }

        /// <summary>
        /// Отчетство
        /// </summary>
        [Required(ErrorMessage = "*")]
        public String SecondName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [Required(ErrorMessage = "*")]
        public String LastName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [Required(ErrorMessage = "*")]
        public Boolean IsOneQuestion { get; set; }

        /// <summary>
        /// E-mail
        /// </summary>
        [Required(ErrorMessage = "*")]
        public String Email { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public String Password { get; set; }

        /// <summary>
        /// Идентификатор роли
        /// </summary>
        public Int32 RoleID { get; set; }

        /// <summary>
        /// Название роли
        /// </summary>
        public String NameRole { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Questionnaire.Service.Models
{
    /// <summary>
    /// Модель пользователей
    /// </summary>
    [DataContract]
    public class UserSM
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [DataMember]
        public Int32 ID { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [DataMember]
        public String FirstName { get; set; }

        /// <summary>
        /// Отчетство
        /// </summary>
        [DataMember]
        public String SecondName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [DataMember]
        public String LastName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [DataMember]
        public Boolean IsOneQuestion { get; set; }

        /// <summary>
        /// E-mail
        /// </summary>
        [DataMember]
        public String Email { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [DataMember]
        public String Password { get; set; }

        /// <summary>
        /// Идентификатор роли
        /// </summary>
        [DataMember]
        public Int32 RoleID { get; set; }

        /// <summary>
        /// Название роли
        /// </summary>
        [DataMember]
        public String NameRole { get; set; }
    }
}
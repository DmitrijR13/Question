using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Questionnaire.Service.Models
{
    /// <summary>
    /// Модель ролей
    /// </summary>
    [DataContract]
    public class RoleSM
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [DataMember]
        public Int32 ID { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        [DataMember]
        public String Name { get; set; }

        /// <summary>
        /// Разрешения
        /// </summary>
        [DataMember]
        public List<PermissionSM> Permissions { get; set; }
    }
}
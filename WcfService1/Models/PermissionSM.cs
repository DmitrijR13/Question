using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Questionnaire.Service.Models
{
    /// <summary>
    /// Модель разрешений
    /// </summary>
    [DataContract]
    public class PermissionSM
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [DataMember]
        public Int32 ID { get; set; }

        /// <summary>
        /// Значение
        /// </summary>
        [DataMember]
        public String Value { get; set; }

        /// <summary>
        /// Выбрано?
        /// </summary>
        [DataMember]
        public Boolean IsChecked { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Questionnaire.Service.Models
{
    /// <summary>
    /// Модель МО организации
    /// </summary>
    [DataContract]
    public class PassportMOSM
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [DataMember]
        public Int32 ID { get; set; }

        /// <summary>
        /// Код
        /// </summary>
        [DataMember]
        public Int32 Code { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        [DataMember]
        public String Name { get; set; }
    }
}
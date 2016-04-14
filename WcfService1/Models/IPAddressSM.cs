using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Questionnaire.Service.Models
{
    /// <summary>
    /// IP адрес организации
    /// </summary>
    [DataContract]
    public class IPAddressSM
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [DataMember]
        public Int32 ID { get; set; }

        /// <summary>
        /// Непосредственно IP адрес организации
        /// </summary>
        [DataMember]
        public String IPAddress { get; set; }

        /// <summary>
        /// Название организации
        /// </summary>
        [DataMember]
        public String NameOrganization { get; set; }
    }
}
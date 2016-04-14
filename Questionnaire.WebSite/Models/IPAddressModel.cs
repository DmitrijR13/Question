using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Questionnaire.WebSite.Models
{
    /// <summary>
    /// IP адрес организации
    /// </summary>
    public class IPAddressModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Int32 ID { get; set; }

        /// <summary>
        /// Непосредственно IP адрес организации
        /// </summary>
        [Required(ErrorMessage="*")]
        [RegularExpression(@"^[0-9]?[0-9]?[0-9]\.[0-9]?[0-9]?[0-9]\.[0-9]?[0-9]?[0-9]\.[0-9]?[0-9]?[0-9]$", ErrorMessage = "*")]
        public String IPAddress { get; set; }

        /// <summary>
        /// Название организации
        /// </summary>
        [Required(ErrorMessage="*")]
        public String NameOrganization { get; set; }
    }
}
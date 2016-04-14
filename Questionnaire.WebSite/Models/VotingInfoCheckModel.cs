using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire.WebSite.Models
{
    /// <summary>
    /// Модель проверки данных голосующего
    /// </summary>
    public class VotingInfoCheckModel
    {
        /// <summary>
        /// Идентификатор голосующего
        /// </summary>
        public Int32 VotingID { get; set; }

        /// <summary>
        /// Идентификатор раздела
        /// </summary>
        public Int32 CharterID { get; set; }
    }
}
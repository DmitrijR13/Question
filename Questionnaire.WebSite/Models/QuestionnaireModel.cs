using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire.WebSite.Models
{
    /// <summary>
    /// Модель первого открытия анкеты
    /// </summary>
    public class QuestionnaireModel
    {
        /// <summary>
        /// Голосующий
        /// </summary>
        public Int32 VotingID { get; set; }

        /// <summary>
        /// Категория
        /// </summary>
        public Int32 CharterID { get; set; }

        /// <summary>
        /// Работаем от плагина
        /// </summary>
        public Boolean IsPlugin { get; set; }
    }
}
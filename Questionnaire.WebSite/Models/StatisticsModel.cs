using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire.WebSite.Models
{
    /// <summary>
    /// Модель для статистика
    /// </summary>
    public class StatisticsModel
    {
        /// <summary>
        /// Идентификатор анкеты
        /// </summary>
        public Int32 CharterID { get; set; }

        /// <summary>
        /// Список вопросов
        /// </summary>
        public List<QuestionModel> Questions { get; set; }
    }
}
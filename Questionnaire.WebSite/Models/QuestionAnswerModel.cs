using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire.WebSite.Models
{
    /// <summary>
    /// Вопрос-ответная модель для статистики
    /// </summary>
    public class QuestionAnswerModel
    {
        /// <summary>
        /// Номер вопроса
        /// </summary>
        public String NumberQuestion { get; set; }

        /// <summary>
        /// Текст вопроса
        /// </summary>
        public String TextQuestion { get; set; }

        /// <summary>
        /// Номер ответа
        /// </summary>
        public String NumberAnswer { get; set; }

        /// <summary>
        /// Текст ответа
        /// </summary>
        public String TextAnswer { get; set; }
    }
}
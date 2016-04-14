using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire.WebSite.Models
{
    /// <summary>
    /// Модель ответа в журнале
    /// </summary>
    public class JournalMarkAnswerModel
    {
        /// <summary>
        /// Идентификатор ответа
        /// </summary>
        public Int32 AnswerID { get; set; }

        /// <summary>
        /// Значение ответа
        /// </summary>
        public String AnswerValue { get; set; }

        /// <summary>
        /// Для мульти-ответов
        /// </summary>
        public String AnswerIDs { get; set; }

        /// <summary>
        /// Номер вопроса в последовательности
        /// </summary>
        public Int32 QuestionNumberSequence { get; set; }

        /// <summary>
        /// Значение вопроса
        /// </summary>
        public String QuestionValue { get; set; }
    }
}
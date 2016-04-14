using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire.WebSite.Models
{
    /// <summary>
    /// Модель голосующего для журнала
    /// </summary>
    public class JournalVotingModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Int32 ID { get; set; }

        /// <summary>
        /// Дата голосования
        /// </summary>
        public String DateVote { get; set; }

        /// <summary>
        /// Название организации
        /// </summary>
        public String NameOrganization { get; set; }

        /// <summary>
        /// Вопрос-ответы
        /// </summary>
        public List<QuestionAnswerModel> QuestionAnswers { get; set; }
    }
}
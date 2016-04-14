using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire.WebSite.Models
{
    /// <summary>
    /// МО
    /// </summary>
    public class JournalMarkMOModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Int32 ID { get; set; }

        /// <summary>
        /// Код
        /// </summary>
        public Int32 Code { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Все вопросы
        /// </summary>
        public List<JournalMarkQuestionModel> Questions { get; set; }

        /// <summary>
        /// Вся сборочная информация
        /// </summary>
        public List<JournalMarkAnswerModel> Answers { get; set; }

        /// <summary>
        /// Непосредственно анкеты
        /// </summary>
        public List<JournalMarkVotingModel> Votings { get; set; }
    }
}
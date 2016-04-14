using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire.WebSite.Models
{
    /// <summary>
    /// Непосредственно анкеты в журнале
    /// </summary>
    public class JournalMarkVotingModel
    {
        /// <summary>
        /// Идентификатор голосующего
        /// </summary>
        public Int32 ID { get; set; }

        /// <summary>
        /// Ответы
        /// </summary>
        public List<JournalMarkAnswerModel> Answers { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire.WebSite.Models
{
    /// <summary>
    /// Вопросы журнала
    /// </summary>
    public class JournalMarkQuestionModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Int32 ID { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Номер в последовательности
        /// </summary>
        public Int32 NumberSequence { get; set; }
    }
}
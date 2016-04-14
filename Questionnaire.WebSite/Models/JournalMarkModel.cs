using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire.WebSite.Models
{
    /// <summary>
    /// Модель журнала МО/СМО с бальной системой
    /// </summary>
    public class JournalMarkModel
    {
        /// <summary>
        /// Все вопросы данного раздела
        /// </summary>
        public List<JournalMarkQuestionModel> Questions { get; set; }

        /// <summary>
        /// Организации МО
        /// </summary>
        public List<JournalMarkMOModel> MOOrganizations { get; set; }

        /// <summary>
        /// Организации СМО
        /// </summary>
        public List<JournalMarkSMOModel> SMOOrganizations { get; set; }
    }
}
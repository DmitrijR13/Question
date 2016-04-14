using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire.WebSite.Models
{
    /// <summary>
    /// Журнал анкет
    /// </summary>
    public class JournalModel
    {
        /// <summary>
        /// Идентификатор анкеты (фильтр)
        /// </summary>
        public String FltVotingID { get; set; }

        /// <summary>
        /// Дата голосования (фильтр)
        /// </summary>
        public String FltDateVote { get; set; }

        /// <summary>
        /// Название организации (фильтр)
        /// </summary>
        public String FltNameOrganization { get; set; }

        /// <summary>
        /// Журнал - голосующие
        /// </summary>
        public List<JournalVotingModel> JournalVoting { get; set; }

        /// <summary>
        /// Все вопросы данного раздела
        /// </summary>
        public List<QuestionModel> Questions { get; set; }

        /// <summary>
        /// Номер текущей страницы
        /// </summary>
        public Int32 NumberPage { get; set; }

        /// <summary>
        /// Количество страниц
        /// </summary>
        public Int32 CountPages { get; set; }

        /// <summary>
        /// Идентификатор раздела
        /// </summary>
        public Int32 CharterID { get; set; }

        /// <summary>
        /// Выбранные ответы (если они есть...)
        /// </summary>
        /// <returns></returns>
        public List<Int32> AnswerIDs { get; set; }
    }
}
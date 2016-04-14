using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire.WebSite.Models
{
    /// <summary>
    /// Текст ответа после завершения анкетирования
    /// </summary>
    public class ReadyModel
    {
        /// <summary>
        /// Тема анкетирования
        /// </summary>
        public String ThemeQuestionnaire { get; set; }

        /// <summary>
        /// Номер анкеты (голосующего)
        /// </summary>
        public String NumberVoting { get; set; }

        /// <summary>
        /// Список всех анкет (разделов)
        /// </summary>
        public List<CharterModel> Charters { get; set; }
    }
}
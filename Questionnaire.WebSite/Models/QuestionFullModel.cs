using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire.WebSite.Models
{
    /// <summary>
    /// Модель всех вопросов-ответов
    /// </summary>
    public class QuestionFullModel
    {
        /// <summary>
        /// Идентификатор раздела
        /// </summary>
        public Int32 CharterID { get; set; }

        /// <summary>
        /// Название раздела
        /// </summary>
        public String CharterName { get; set; }

        /// <summary>
        /// Голос
        /// </summary>
        public Int32 VotingID { get; set; }

        /// <summary>
        /// Вопросы
        /// </summary>
        public List<VotingQuestionModel> Questions { get; set; }
    }
}
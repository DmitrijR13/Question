using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Questionnaire.WebSite.Models
{
    /// <summary>
    /// Модель вопроса при голосовании
    /// </summary>
    public class VotingQuestionModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Required(ErrorMessage = "*")]
        public Int32 QuestionID { get; set; }

        /// <summary>
        /// Голосующий
        /// </summary>
        [Required(ErrorMessage = "*")]
        public Int32 VotingID { get; set; }

        /// <summary>
        /// Текст вопроса
        /// </summary>
        public String TextQuestion { get; set; }

        /// <summary>
        /// Первый вопрос в анкете?
        /// </summary>
        public Boolean IsFirst { get; set; }

        /// <summary>
        /// Последний вопрос?
        /// </summary>
        public Boolean IsLast { get; set; }

        /// <summary>
        /// ID выбранного ответа
        /// </summary>
        [Required(ErrorMessage = "*")]
        public Int32 IDCheck { get; set; }

        /// <summary>
        /// IDs выбранного ответа
        /// </summary>
        [Required(ErrorMessage = "*")]
        public List<Int32> IDChecks { get; set; }

        /// <summary>
        /// Номер
        /// </summary>
        public String Number { get; set; }

        /// <summary>
        /// Тип вопроса
        /// </summary>
        public Int32 TypeQuestion { get; set; }

        /// <summary>
        /// Текст ответа
        /// </summary>
        [Required(ErrorMessage="*")]
        public String TextAnswer { get; set; }

        /// <summary>
        /// Дата начала
        /// </summary>
        [Required(ErrorMessage = "*")]
        public String DateStart { get; set; }

        /// <summary>
        /// Дата конца
        /// </summary>
        [Required(ErrorMessage = "*")]
        public String DateEnd { get; set; }

        /// <summary>
        /// Индекс текущего вопроса
        /// </summary>
        public Int32 IndexQuestion { get; set; }

        /// <summary>
        /// Количество вопросов в анкете
        /// </summary>
        public Int32 CountQuestions { get; set; }

        /// <summary>
        /// Ответы
        /// </summary>
        public List<VotingAnswerModel> Answers { get; set; }
    }
}
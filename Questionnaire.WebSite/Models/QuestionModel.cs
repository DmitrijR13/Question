using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Questionnaire.WebSite.Models
{
    /// <summary>
    /// Модель вопроса и списка ответов
    /// </summary>
    public class QuestionModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Int32 ID { get; set; }
        
        /// <summary>
        /// Текст вопроса
        /// </summary>
        [Required(ErrorMessage="*")]
        public String TextQuestion { get; set; }

        /// <summary>
        /// Номер последовательности
        /// </summary>
        [Required(ErrorMessage = "*")]
        public Int32 NumberSequence { get; set; }

        /// <summary>
        /// Идентификатор раздела
        /// </summary>
        [Required(ErrorMessage = "*")]
        public Int32 CharterID { get; set; }

        /// <summary>
        /// Название раздела
        /// </summary>
        public String NameCharter { get; set; }

        /// <summary>
        /// Тип вопроса
        /// </summary>
        public Int32 TypeQuestion { get; set; }

        /// <summary>
        /// Тип вопроса (название)
        /// </summary>
        public String TypeQuestionName { get; set; }

        /// <summary>
        /// Ответы
        /// </summary>
        [Required(ErrorMessage = "*")]
        public List<AnswerModel> Answers { get; set; }
    }
}
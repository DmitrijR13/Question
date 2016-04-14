using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Questionnaire.WebSite.Models
{
    /// <summary>
    /// Модель ответа
    /// </summary>
    public class AnswerModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Int32 ID { get; set; }

        /// <summary>
        /// Номер последовательности
        /// </summary>
        [Required(ErrorMessage="*")]
        public Int32 NumberSequence { get; set; }

        /// <summary>
        /// Текст ответа
        /// </summary>
        //[Required(ErrorMessage = "*")]
        public String TextAnswer { get; set; }

        /// <summary>
        /// Дополнительный текст ответа 1
        /// </summary>
        //[Required(ErrorMessage = "*")]
        public String TextAnswerAdditional1 { get; set; }

        /// <summary>
        /// Дополнительный текст ответа 2
        /// </summary>
        //[Required(ErrorMessage = "*")]
        public String TextAnswerAdditional2 { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// Цена ответа
        /// </summary>
        [Required(ErrorMessage = "*")]
        public Decimal Score { get; set; }

        /// <summary>
        /// Отмеченный?
        /// </summary>
        public Boolean IsCheck { get; set; }

        /// <summary>
        /// Вопрос
        /// </summary>
        public Int32? NextQuestionID { get; set; }
    }
}
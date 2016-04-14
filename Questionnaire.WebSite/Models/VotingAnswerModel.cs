using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Questionnaire.WebSite.Models
{
    /// <summary>
    /// Модель ответа при голосовании
    /// </summary>
    public class VotingAnswerModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Int32 AnswerID { get; set; }

        /// <summary>
        /// Текст ответа
        /// </summary>
        public String TextAnswer { get; set; }
        
        /// <summary>
        /// Отмеченный?
        /// </summary>
        [Required(ErrorMessage = "*")]
        public Boolean IsCheck { get; set; }
    }
}
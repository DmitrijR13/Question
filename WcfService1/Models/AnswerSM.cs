using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Questionnaire.Service.Models
{
    /// <summary>
    /// Модель ответа
    /// </summary>
    [DataContract]
    public class AnswerSM
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [DataMember]
        public Int32 ID { get; set; }

        /// <summary>
        /// Номер последовательности
        /// </summary>
        [DataMember]
        public Int32 NumberSequence { get; set; }

        /// <summary>
        /// Текст ответа
        /// </summary>
        [DataMember]
        public String TextAnswer { get; set; }

        /// <summary>
        /// Дополнительный текст ответа 1
        /// </summary>
        [DataMember]
        public String TextAnswerAdditional1 { get; set; }

        /// <summary>
        /// Дополнительный текст ответа 2
        /// </summary>
        [DataMember]
        public String TextAnswerAdditional2 { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        [DataMember]
        public String Description { get; set; }

        /// <summary>
        /// Цена ответа
        /// </summary>
        [DataMember]
        public Decimal Score { get; set; }

        /// <summary>
        /// Отмеченный?
        /// </summary>
        [DataMember]
        public Boolean IsCheck { get; set; }

        /// <summary>
        /// Вопрос
        /// </summary>
        [DataMember]
        public Int32? NextQuestionID { get; set; }
    }
}
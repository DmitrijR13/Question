using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Questionnaire.Service.Models
{
    /// <summary>
    /// Модель вопроса и списка ответов
    /// </summary>
    [DataContract]
    public class QuestionSM
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [DataMember]
        public Int32 ID { get; set; }
        
        /// <summary>
        /// Текст вопроса
        /// </summary>
        [DataMember]
        public String TextQuestion { get; set; }

        /// <summary>
        /// Номер последовательности
        /// </summary>
        [DataMember]
        public Int32 NumberSequence { get; set; }

        /// <summary>
        /// Идентификатор раздела
        /// </summary>
        [DataMember]
        public Int32 CharterID { get; set; }

        /// <summary>
        /// Название раздела
        /// </summary>
        [DataMember]
        public String NameCharter { get; set; }

        /// <summary>
        /// Тип вопроса
        /// </summary>
        [DataMember]
        public Int32 TypeQuestion { get; set; }

        /// <summary>
        /// Тип вопроса (название)
        /// </summary>
        [DataMember]
        public String TypeQuestionName { get; set; }

        /// <summary>
        /// Ответы
        /// </summary>
        [DataMember]
        public List<AnswerSM> Answers { get; set; }
    }
}
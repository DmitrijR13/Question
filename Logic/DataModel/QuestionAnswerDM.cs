using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sobits.Story.Logic.DataModel
{
    /// <summary>
    /// Вопрос-Ответы модель
    /// </summary>
    public class QuestionAnswerDM
    {
        /// <summary>
        /// ID голосующего
        /// </summary>
        public Int32 VotingID { get; set; }

        /// <summary>
        /// Идентификатор вопроса
        /// </summary>
        public Int32 QuestionID { get; set; }

        /// <summary>
        /// Номер вопроса в последовательности
        /// </summary>
        public Int32 QuestionNumberSequence { get; set; }

        /// <summary>
        /// Текст вопроса
        /// </summary>
        public String QuestionValue { get; set; }

        /// <summary>
        /// Описание вопроса
        /// </summary>
        public String QuestionDescription { get; set; }

        /// <summary>
        /// Раздел, к которому относится вопрос анкеты
        /// </summary>
        public Int32 QuestionCharterID { get; set; }

        /// <summary>
        /// Дата создания вопроса
        /// </summary>
        public DateTime QuestionDateCreate { get; set; }

        /// <summary>
        /// Дата редактирования вопроса
        /// </summary>
        public DateTime QuestionDateUpdate { get; set; }

        /// <summary>
        /// Идентификатор ответа
        /// </summary>
        public Int32 AnswerID { get; set; }

        /// <summary>
        /// Идентификаторы ответов
        /// </summary>
        public String AnswerIDs { get; set; }

        /// <summary>
        /// Номер ответа в последовательности
        /// </summary>
        public Int32 AnswerNumberSequence { get; set; }

        /// <summary>
        /// Текст ответа
        /// </summary>
        public String AnswerValue { get; set; }

        /// <summary>
        /// Описание ответа
        /// </summary>
        public String AnswerDescription { get; set; }

        /// <summary>
        /// Бал (оценка) ответа
        /// </summary>
        public Decimal AnswerScore { get; set; }

        /// <summary>
        /// Ответ удален?
        /// </summary>
        public Boolean AnswerIsDelete { get; set; }

        /// <summary>
        /// Дата создания ответа
        /// </summary>
        public DateTime AnswerDateCreate { get; set; }

        /// <summary>
        /// Дата редактирования ответа
        /// </summary>
        public DateTime AnswerDateUpdate { get; set; }
    }
}

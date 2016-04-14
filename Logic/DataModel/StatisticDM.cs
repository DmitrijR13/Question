using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sobits.Story.Logic.DataModel
{
    /// <summary>
    /// Модель возвращаемых данных статистики
    /// </summary>
    public class StatisticDM
    {
        /// <summary>
        /// Идентификатор анкеты (голосующего)
        /// </summary>
        public Int32 VotingID { get; set; }

        /// <summary>
        /// Дата голосующего
        /// </summary>
        public DateTime DateVote { get; set; }

        /// <summary>
        /// Идентификатор организации
        /// </summary>
        public Int32 IPAddressID { get; set; }

        /// <summary>
        /// Название организации
        /// </summary>
        public String NameOrganization { get; set; }

        /// <summary>
        /// Идентфикатор вопроса
        /// </summary>
        public Int32 QuestionID { get; set; }

        /// <summary>
        /// Текст вопроса
        /// </summary>
        public String TextQuestion { get; set; }

        /// <summary>
        /// Номер в последовательности вопроса
        /// </summary>
        public Int32 NumberSequenceQuestion { get; set; }

        /// <summary>
        /// Идентфикатор ответа
        /// </summary>
        public Int32 AnswerID { get; set; }

        /// <summary>
        /// Текст ответа
        /// </summary>
        public String TextAnswer { get; set; }

        /// <summary>
        /// Номер в последовательности ответа
        /// </summary>
        public Int32 NumberSequenceAnswer { get; set; }
    }
}

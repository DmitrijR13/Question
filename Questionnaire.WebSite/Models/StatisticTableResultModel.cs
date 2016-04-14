using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire.WebSite.Models
{
    /// <summary>
    /// Модель вывода данных для статистики в табличном формате
    /// </summary>
    public class StatisticTableResultModel
    {
        /// <summary>
        /// Идентификатор анкеты (голосующего)
        /// </summary>
        public Int32 VotingID { get; set; }

        /// <summary>
        /// Имя голосующего
        /// </summary>
        public String FirstName { get; set; }

        /// <summary>
        /// Отчетсво голосующего
        /// </summary>
        public String SecondName { get; set; }

        /// <summary>
        /// Фамилия голосующего
        /// </summary>
        public String LastName { get; set; }

        /// <summary>
        /// Анонимный ли пользователь
        /// </summary>
        public String IsAnonymous { get; set; }

        /// <summary>
        /// Дата голосующего
        /// </summary>
        public String DateVote { get; set; }

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
        /// Идентфикатор ответа
        /// </summary>
        public Int32 AnswerID { get; set; }

        /// <summary>
        /// Текст ответа
        /// </summary>
        public String TextAnswer { get; set; }
    }
}
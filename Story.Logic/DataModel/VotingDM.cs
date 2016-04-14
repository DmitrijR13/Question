using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sobits.Story.Logic.DataModel
{
    /// <summary>
    /// Человек, который прошел анкетирование (зачастую анонимус)
    /// </summary>
    public class VotingDM
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Int32 ID { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public String FirstName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public String SecondName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public String LastName { get; set; }

        /// <summary>
        /// Анонимный пользователь?
        /// </summary>
        public Boolean IsAnonymous { get; set; }

        /// <summary>
        /// Дата голосования
        /// </summary>
        public DateTime DateVote { get; set; }

        /// <summary>
        /// IP адрес с которого заполнялась анкета
        /// </summary>
        public Int32 IPAddressID { get; set; }

        /// <summary>
        /// Название организации
        /// </summary>
        public String NameOrganization { get; set; }

        /// <summary>
        /// МО идентификатор
        /// </summary>
        public Int32 MOOrganizationID { get; set; }

        /// <summary>
        /// СМО идентификатор
        /// </summary>
        public Int32 SMOOrganizationID { get; set; }
    }
}

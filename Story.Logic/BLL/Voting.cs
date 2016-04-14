using Sobits.DataEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sobits.Story.Logic.BLL
{
    /// <summary>
    /// Человек, который прошел анкетирование (зачастую анонимус)
    /// </summary>
    public class Voting : AbstractEntity
    {
        /// <summary>
        /// Имя
        /// </summary>
        public virtual String FirstName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public virtual String SecondName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public virtual String LastName { get; set; }

        /// <summary>
        /// Анонимный пользователь?
        /// </summary>
        public virtual Boolean IsAnonymous { get; set; }

        /// <summary>
        /// IP адрес с которого заполнялась анкета
        /// </summary>
        public virtual IPAddress IPAddress { get; set; }

        /// <summary>
        /// Дата голосования
        /// </summary>
        public virtual DateTime DateVote { get; set; }

        /// <summary>
        /// МО организация
        /// </summary>
        public virtual MOOrganization MOOrganization { get; set; }

        /// <summary>
        /// СМО организация
        /// </summary>
        public virtual SMOOrganization SMOOrganization { get; set; }
    }
}

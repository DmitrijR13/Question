using Sobits.DataEngine;
using Sobits.Story.Logic.BLL;
using Sobits.Story.Logic.Enumerable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sobits.Story.Logic.BLL
{
    /// <summary>
    /// Вопросы анкеты
    /// </summary>
    public class Question : AbstractEntity
    {
        /// <summary>
        /// Номер в последовательности
        /// </summary>
        public virtual Int32 NumberSequence { get; set; }

        /// <summary>
        /// Текст вопроса
        /// </summary>
        public virtual String Value { get; set; }

        /// <summary>
        /// Описание вопроса
        /// </summary>
        public virtual String Description { get; set; }

        /// <summary>
        /// Раздел, к которому относится анкета
        /// </summary>
        public virtual Charter Charter { get; set; }

        /// <summary>
        /// Тип вопроса
        /// </summary>
        public virtual TypeQuestion TypeQuestion { get; set; }

        /// <summary>
        /// Удален?
        /// </summary>
        public virtual Boolean IsDelete { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public virtual DateTime DateCreate { get; set; }

        /// <summary>
        /// Дата редактирования
        /// </summary>
        public virtual DateTime DateUpdate { get; set; }

        /// <summary>
        /// Пользователь создавший запись
        /// </summary>
        public virtual User UserCreate { get; set; }

        /// <summary>
        /// Пользователь редактировавший запись
        /// </summary>
        public virtual User UserUpdate { get; set; }

        /// <summary>
        /// Список ответов на этот вопрос
        /// </summary>
        public virtual IList<Answer> Answers { get; set; }
    }
}

using Sobits.WebEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sobits.Story.Logic.BLL
{
    /// <summary>
    /// Ответ на вопрос анкеты
    /// </summary>
    public class Answer : AbstractEntity
    {
        /// <summary>
        /// Номер в последовательности
        /// </summary>
        public virtual Int32 NumberSequence { get; set; }

        /// <summary>
        /// Текст ответа
        /// </summary>
        public virtual String Value { get; set; }

        /// <summary>
        /// Дополнительное поле значения
        /// </summary>
        public virtual String ValueAdditional_1 { get; set; }

        /// <summary>
        /// Дополнительное поле значения
        /// </summary>
        public virtual String ValueAdditional_2 { get; set; }

        /// <summary>
        /// Дата начала (от)
        /// </summary>
        public virtual DateTime? DateStart { get; set; }

        /// <summary>
        /// Дата окончания (до)
        /// </summary>
        public virtual DateTime? DateEnd { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public virtual String Description { get; set; }

        /// <summary>
        /// Бал (оценка) ответа
        /// </summary>
        public virtual Decimal Score { get; set; }

        /// <summary>
        /// Вопрос анкеты, к которому относится ответ
        /// </summary>
        public virtual Question Question { get; set; }

        /// <summary>
        /// Вопрос анкеты, к которому произойдет переход
        /// </summary>
        public virtual Question NextQuestion { get; set; }

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
    }
}

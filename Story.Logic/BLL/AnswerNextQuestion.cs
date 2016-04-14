using Sobits.DataEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sobits.Story.Logic.BLL
{
    /// <summary>
    /// Связь "ответ - следующий вопрос"
    /// </summary>
    public class AnswerNextQuestion : AbstractEntity
    {
        /// <summary>
        /// Только что выбранный ответ
        /// </summary>
        public virtual Answer Answer { get; set; }

        /// <summary>
        /// Вопрос, к которому осуществляется переход
        /// </summary>
        public virtual Question Question { get; set; }
    }
}

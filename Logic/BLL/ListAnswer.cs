using Sobits.WebEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sobits.Story.Logic.BLL
{
    /// <summary>
    /// Список ответов
    /// </summary>
    public class ListAnswer : AbstractEntity
    {
        /// <summary>
        /// Ответ
        /// </summary>
        public virtual Answer Answer { get; set; }

        /// <summary>
        /// Связанные ответы
        /// </summary>
        public virtual Answer AnswerPrev { get; set; }
    }
}

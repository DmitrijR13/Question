using Sobits.WebEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sobits.Story.Logic.BLL
{
    /// <summary>
    /// Связь анкеты и прошедшего анкету
    /// </summary>
    public class VotingQuestion : AbstractEntity
    {
        /// <summary>
        /// Вопрос анкеты
        /// </summary>
        public virtual Question Question { get; set; }

        /// <summary>
        /// Ответ на вопрос анкеты
        /// </summary>
        public virtual Answer Answer { get; set; }

        /// <summary>
        /// Анкетируемый
        /// </summary>
        public virtual Voting Voting { get; set; }
    }
}

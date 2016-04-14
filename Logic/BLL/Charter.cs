using Sobits.WebEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sobits.Story.Logic.BLL
{
    /// <summary>
    /// Раздел, к которому принадлежит анкета
    /// </summary>
    public class Charter : AbstractEntity
    {
        /// <summary>
        /// Название
        /// </summary>
        public virtual String Name { get; set; }

        /// <summary>
        /// Картинка
        /// </summary>
        public virtual BFile Image { get; set; }

        /// <summary>
        /// Удален?
        /// </summary>
        public virtual Boolean IsDelete { get; set; }
    }
}

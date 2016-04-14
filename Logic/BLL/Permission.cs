using Sobits.WebEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sobits.Story.Logic.BLL
{
    /// <summary>
    /// Перечень разрешений
    /// </summary>
    public class Permission : AbstractEntity
    {
        /// <summary>
        /// Название
        /// </summary>
        public virtual String Name { get; set; }

        /// <summary>
        /// Значение
        /// </summary>
        public virtual String Value { get; set; }
    }
}

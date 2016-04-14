using Sobits.WebEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sobits.Story.Logic.BLL
{
    /// <summary>
    /// МО организация
    /// </summary>
    public class MOOrganization : AbstractEntity
    {
        /// <summary>
        /// Код
        /// </summary>
        public virtual Int32 Code { get; set; }

        /// <summary>
        /// Название (краткое) 
        /// </summary>
        public virtual String Name { get; set; }
    }
}

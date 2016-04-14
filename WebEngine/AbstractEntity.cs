using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sobits.WebEngine
{
    /// <summary>
    /// Абстрактный класс сущности
    /// </summary>
    public abstract class AbstractEntity : IEntity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public virtual Int32 ID { get; set; }
    }
}

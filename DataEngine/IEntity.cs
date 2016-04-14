using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sobits.DataEngine
{
    /// <summary>
    /// Base entity interface
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        Int32 ID { get; }
    }
}

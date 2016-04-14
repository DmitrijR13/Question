using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sobits.DataEngine.Listeners
{
    /// <summary>
    /// Entity with external delete event handler
    /// </summary>
    public interface IDeleteEventListener
    {
        /// <summary>
        /// After delete event handler
        /// </summary>
        void AfterDelete();
    }
}

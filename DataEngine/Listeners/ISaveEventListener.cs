using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sobits.DataEngine.Listeners
{
    /// <summary>
    /// Entity with save event handling
    /// </summary>
    public interface ISaveEventListener
    {
        /// <summary>
        /// After save event handler
        /// </summary>
        void AfterSave(SaveEventType eventType);
    }
}

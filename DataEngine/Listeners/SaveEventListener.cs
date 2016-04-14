using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate.Event;

namespace Sobits.DataEngine.Listeners
{
    internal class SaveEventListener : IPostInsertEventListener, IPostUpdateEventListener
    {
        #region IPostInsertEventListener Members

        /// <summary>
        /// Handle insert record
        /// </summary>
        /// <param name="evnt"></param>
        public void OnPostInsert(PostInsertEvent evnt)
        {
            if (evnt.Entity is ISaveEventListener)
            {
                ((ISaveEventListener)evnt.Entity).AfterSave(SaveEventType.Insert);
                evnt.Session.Save(evnt.Entity);
            }
        }

        #endregion

        #region IPostUpdateEventListener Members

        /// <summary>
        /// Handle update record
        /// </summary>
        /// <param name="evnt"></param>
        public void OnPostUpdate(PostUpdateEvent evnt)
        {
            if (evnt.Entity is ISaveEventListener)
            {
                ((ISaveEventListener)evnt.Entity).AfterSave(SaveEventType.Update);
                evnt.Session.Save(evnt.Entity);
            }
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate.Event;
using NHibernate.Event.Default;

namespace Sobits.DataEngine.Listeners
{
    internal class DeleteEventListener : DefaultDeleteEventListener
    {
        public override void OnDelete(DeleteEvent evnt)
        {
            Object entity = evnt.Entity;

            if (entity is IDeleteEventListener)
            {
                ((IDeleteEventListener)entity).AfterDelete();
            }
            
            base.OnDelete(evnt);
        }
    }
}

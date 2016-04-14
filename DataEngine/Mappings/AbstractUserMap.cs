using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sobits.DataEngine.BLL;

namespace Sobits.DataEngine.Mappings
{
    /// <summary>
    /// Abstract mapping for user
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AbstractUserMap<T> : ClassMapBase<T>
        where T : AbstractUser
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public AbstractUserMap()
            : base()
        {
            Map(x => x.Email).Not.Nullable();
            Map(x => x.Password).Not.Nullable();
        }
    }
}

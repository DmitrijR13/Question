using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sobits.DataEngine.Repo.Impl
{
    /// <summary>
    /// Abstract repository for weak entities
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AbstractWeakRepo<T> : AbstractRepoBase<T>
        where T: IEntity
    {
        #region constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        protected AbstractWeakRepo()
        { 
        }

        /// <summary>
        /// Constructor for DataSession
        /// </summary>
        /// <param name="session"></param>
        public AbstractWeakRepo(IDataSession session)
            :base(session)
        {
        }

        #endregion constructors
    }
}

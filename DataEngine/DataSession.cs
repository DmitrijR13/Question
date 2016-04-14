using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;

using NHibernate;

using Sobits.DataEngine.BLL;

namespace Sobits.DataEngine
{
    /// <summary>
    /// Default data session
    /// </summary>
    public class DataSession : IDataSession
    {
        #region constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        protected DataSession()
        { }

        /// <summary>
        /// Authorized constructor
        /// </summary>
        /// <param name="user"></param>
        /// <param name="context"></param>
        protected internal DataSession(IPrincipal user, ISession context)
        {
            User = user;
            Context = context;
        }

        /// <summary>
        /// Anonymus constructor
        /// </summary>
        /// <param name="сontext"></param>
        protected internal DataSession(ISession сontext)
        {
            Context = сontext;
        }

        #endregion constructors

        #region public properties

        /// <summary>
        /// Current session user (null if anonymus session)
        /// </summary>
        public IPrincipal User { get; private set; }

        /// <summary>
        /// True if personalyzed session
        /// </summary>
        public Boolean IsLoggedUser
        {
            get 
            {
                return User != null; 
            }
        }

        #endregion public properties

        #region public methods

        /// <summary>
        /// Disposing session
        /// </summary>
        public void Dispose()
        {
            OnDisposing();
        }

        #endregion public methods

        #region protected properties

        /// <summary>
        /// NHibernate data session
        /// </summary>
        public ISession Context { get; protected set; }

        #endregion protected properties

        #region protected methods

        /// <summary>
        /// Disposing handler
        /// </summary>
        protected virtual void OnDisposing()
        {
            Context.Dispose();
        }

        #endregion protected methods
    }
}

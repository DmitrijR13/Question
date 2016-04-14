using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sobits.DataEngine.BLL.Initialization;

namespace Sobits.DataEngine.Repo
{
    /// <summary>
    /// Abstract repository factory. Mast be singleton
    /// </summary>
    public abstract class AbstractRepoFactory
    {
        #region constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sessionFactory"></param>
        protected AbstractRepoFactory(AbstractSessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
            FillMap();

            //if (Configuration.UseDataCreate)
            //{
            //    try
            //    {
            //        IDataSession session = sessionFactory.GetSession();
            //        foreach (IDataInitProvider provider in GetInitProviders(session))
            //        {
            //            provider.Init();
            //        }
            //    }
            //    catch (InvalidOperationException)
            //    { 
            //        // Do nothing
            //    }
            //}
        }

        #endregion constructors

        #region public methods

        /// <summary>
        /// Create new instance of repository by its interface
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual T GetRepo<T>()
            where T : IRepo
        {
            return GetRepo<T>(_sessionFactory.GetSession());
        }

        /// <summary>
        /// Internal create repository object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <returns></returns>
        internal virtual T GetRepo<T>(IDataSession session)
        { 
            Type type = typeof(T);

            if (RepoMap.ContainsKey(type))
            {
                return (T)Activator.CreateInstance(RepoMap[type],new Object[] { session });
            }
            else
            {
                throw new ArgumentException(String.Format("Interface {0} was not found in RepoMap", type.FullName));
            }
        }

        #endregion public methods

        #region protected properties

        /// <summary>
        /// Map of repository interfaces and implementations
        /// </summary>
        protected virtual Dictionary<Type, Type> RepoMap
        {
            get
            {
                return _repoMap;
            }
        }

        #endregion protected properties

        #region protected methods

        /// <summary>
        /// Fill repository map
        /// </summary>
        protected abstract void FillMap();

        /// <summary>
        /// Get Initialization providers 
        /// </summary>
        /// <returns></returns>
        protected virtual IDataInitProvider[] GetInitProviders(IDataSession session)
        {
            return new IDataInitProvider[0];
        }

        #endregion protected methods

        #region private fields

        private readonly Dictionary<Type, Type> _repoMap = new Dictionary<Type, Type>();
        private readonly AbstractSessionFactory _sessionFactory;

        #endregion private fields
    }
}

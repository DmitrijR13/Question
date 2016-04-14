using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sobits.DataEngine.Repo;

namespace Sobits.DataEngine.BLL.Initialization
{
    /// <summary>
    /// Abstract data initialization provider
    /// </summary>
    public abstract class AbstractDataInitProvider: IDataInitProvider
    {
        #region constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repoFactory"></param>
        /// <param name="session"></param>
        public AbstractDataInitProvider(AbstractRepoFactory repoFactory, IDataSession session)
        {
            _repoFactory = repoFactory;
            _session = session;
        }

        #endregion

        #region public methods

        /// <summary>
        /// Initialize
        /// </summary>
        public abstract void Init();

        #endregion

        #region protected properties

        /// <summary>
        /// Get repository
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected T GetRepo<T>()
        {
            return _repoFactory.GetRepo<T>(_session);
        }

        #endregion

        #region private fields

        private AbstractRepoFactory _repoFactory;
        private IDataSession _session;

        #endregion

    }
}

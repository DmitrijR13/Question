using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate;
using NHibernate.Linq;
using System.Collections;

namespace Sobits.DataEngine.Repo.Impl
{
    /// <summary>
    /// Base implementation of repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AbstractRepoBase<T> : IWeakRepo<T>
        where T : IEntity
    {
        #region constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        protected AbstractRepoBase()
        { 
        }

        /// <summary>
        /// Constructor for DataSession
        /// </summary>
        /// <param name="session"></param>
        public AbstractRepoBase(IDataSession session)
        {
            Session = session;
        }

        #endregion constructors

        #region public methods

        /// <summary>
        /// Get entity by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Get(Int32 id)
        {
            return GetFromDB(id);
        }

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            return GetAllFromDB();
        }

        /// <summary>
        /// Get entries through SQL
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public IList GetFromSQL(String sql)
        {
            return GetFromSQLDB(sql);
        }

        #endregion public methods

        #region protected methods

        /// <summary>
        /// Get entity from DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected virtual T GetFromDB(Int32 id)
        {
            return Context.Get<T>(id);
        }

        /// <summary>
        /// Get all entities from DB
        /// </summary>
        /// <returns></returns>
        protected virtual IQueryable<T> GetAllFromDB()
        {
            return Context.Query<T>();
        }

        /// <summary>
        /// Get entries through SQL from DB
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected virtual IList GetFromSQLDB(String sql)
        {
            return Context.CreateSQLQuery(sql).List();
        }

        #endregion protected methods

        #region protected properties

        /// <summary>
        /// Data context
        /// </summary>
        protected virtual ISession Context
        {
            get
            {
                return _session.Context;
            }
        }

        /// <summary>
        /// Data session
        /// </summary>
        protected IDataSession Session 
        {
            get
            {
                return _session;
            }
            set
            {
                _session = (DataSession)value;
            }
        }

        #endregion protected properties

        private DataSession _session;
    }
}

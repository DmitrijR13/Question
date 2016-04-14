using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate;
using NHibernate.Linq;
using System.Collections;
using Sobits.DataEngine.BLL;

namespace Sobits.DataEngine.Repo.Impl
{
    /// <summary>
    /// Abstract repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AbstractRepo<T> : AbstractRepoBase<T>, IRepo<T>
        where T : AbstractEntity
    {
        #region constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        protected AbstractRepo()
        { 
        }

        /// <summary>
        /// Constructor for DataSession
        /// </summary>
        /// <param name="session"></param>
        public AbstractRepo(IDataSession session)
            :base(session)
        {
        }

        #endregion constructors

        #region public methods

        /// <summary>
        /// Save entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual T Save(T entity)
        {
            using (ITransaction transaction = Context.BeginTransaction())
            {
                if (entity.ID > 0)
                {
                    Context.Merge(entity);
                }
                else
                {
                    Context.Save(entity);
                }

                transaction.Commit();
            }

            return entity;
        }

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(T entity)
        {
            using (ITransaction transaction = Context.BeginTransaction())
            {
                Context.Delete(entity);
                transaction.Commit();
            }
        }

        /// <summary>
        /// Run SQL (transaction)
        /// </summary>
        /// <returns></returns>
        public virtual IList RunSQL(String sql)
        {
            IList result = null;
            using (ITransaction transaction = Context.BeginTransaction())
            {
                result = Context.CreateSQLQuery(sql).List();
                transaction.Commit();
            }

            return result;
        }

        /// <summary>
        /// Save list entities
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual List<T> SaveList(List<T> entities)
        {
            using (ITransaction transaction = Context.BeginTransaction())
            {
                foreach (T item in entities)
                {
                    if (item.ID > 0)
                    {
                        Context.Merge(item);
                    }
                    else
                    {
                        Context.SaveOrUpdate(item);
                    }
                }

                transaction.Commit();
            }

            return entities;
        }

        /// <summary>
        /// Delete list entities
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual void DeleteList(List<T> entities)
        {
            using (ITransaction transaction = Context.BeginTransaction())
            {
                foreach (T item in entities)
                {
                    Context.Delete(item);
                }

                transaction.Commit();
            }
        }

        #endregion public methods
    }
}

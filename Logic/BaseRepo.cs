using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sobits.Story.Logic.Repo;
using Sobits.Story.Logic.BLL;
using NHibernate;
using NHibernate.Linq;
using System.Collections;
using Sobits.WebEngine;

namespace Sobits.Story.Logic
{
    public class BaseRepo<T> : IRepo<T>
         where T : AbstractEntity
    {

        #region constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        protected BaseRepo()
        { 
        }

        /// <summary>
        /// Constructor for DataSession
        /// </summary>
        /// <param name="session"></param>
        public BaseRepo(ISession session)
        {
            Context = session;
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
            //ISession session = Context;

            //// Начинаем явную транзакцию
            //ITransaction tr = session.BeginTransaction();

            //// Сохрананяем объект в сессии
            //session.Save(entity);
            
            //// Завершаем транзакцию. Сейчас данные будут физически записаны в БД
            //// После этого можно отрыть таблицу в БД и убедиться, что там действительно появилаь новая запись
            //tr.Commit();
            //session.Flush();

            //// Очищаем кэш сессии, чтобы быть уверенными, что объект будет получен из базы, а не из сессии
            //session.Clear();

            using (ITransaction transaction = Context.BeginTransaction())
            {
                if (entity.ID > 0)
                {
                    Context.Merge(entity);
                }
                else
                {
                    Context.SaveOrUpdate(entity);
                }

                transaction.Commit();
            }
            
            return entity;
        }

        /// <summary>
        /// Save list entities
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual void SaveAll(List<T> entities)
        {
            using (ITransaction transaction = Context.BeginTransaction())
            {
                foreach (T item in entities)
                {
                    Context.SaveOrUpdate(item);
                }

                transaction.Commit();
            }
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
        /// Get entity by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Get(Int32 id)
        {
            return Context.Get<T>(id);
        }

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            return Context.Query<T>();
        }

        /// <summary>
        /// Run any sql command
        /// </summary>
        public IList RunSQL(String query)
        {
            IList result;
            using (ITransaction transaction = Context.BeginTransaction())
            {
                result = Context.CreateSQLQuery(query).List();
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
                    Context.Merge(item);
                    Context.Evict(item);
                    Context.Delete(item);
                }

                transaction.Commit();
            }
        }

        #endregion public methods

        #region protected properties

        /// <summary>
        /// Data context
        /// </summary>
        protected virtual ISession Context
        {
            get
            {
                return _session;
            }
            set
            {
                _session = value;
            }
        }
        #endregion protected properties

        private ISession _session;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sobits.DataEngine.Repo
{
    /// <summary>
    /// Base repository interface
    /// </summary>
    public interface IRepo
    { }

    /// <summary>
    /// Generic repository interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepo<T> : IRepo
        where T : IEntity
    {
        /// <summary>
        /// Get entity by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(Int32 id);

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Get entries through SQL
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        IList GetFromSQL(String sql);

        /// <summary>
        /// Save entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T Save(T entity);

        /// <summary>
        /// Save list entities
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        List<T> SaveList(List<T> entities);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);

        /// <summary>
        /// Delete list entities
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        void DeleteList(List<T> entities);

        /// <summary>
        /// Run SQL (transaction)
        /// </summary>
        /// <returns></returns>
        IList RunSQL(String sql);
    }
}

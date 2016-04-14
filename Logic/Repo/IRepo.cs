using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sobits.Story.Logic.BLL;
using System.Collections;
using Sobits.WebEngine;

namespace Sobits.Story.Logic.Repo
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
        /// Save entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T Save(T entity);

        /// <summary>
        /// Save all entities
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void SaveAll(List<T> entity);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);

        /// <summary>
        /// Run any sql script
        /// </summary>
        IList RunSQL(String query);

        /// <summary>
        /// Save list data
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        List<T> SaveList(List<T> entities);

        /// <summary>
        /// Delete list data
        /// </summary>
        /// <param name="entities"></param>
        void DeleteList(List<T> entities);
    }
}

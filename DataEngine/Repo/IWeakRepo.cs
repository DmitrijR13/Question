using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sobits.DataEngine.Repo
{
    /// <summary>
    /// Weak entity repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IWeakRepo<T> : IRepo
        where T: IEntity
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
    }
}

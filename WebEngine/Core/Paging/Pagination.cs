using System;
using System.Linq;
using System.Web.Routing;

namespace Sobits.WebEngine.Core.Paging
{
    /// <summary>
    /// Pagination helper
    /// </summary>
    public static class Pagination
    {
        #region Constants

        /// <summary>
        /// Default page size
        /// </summary>
        private const short DefaultPageSize = 10;

        #endregion Constants

        #region Public static methods

        #region Extension methods

        /// <summary>
        /// Create paged list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">Source items list</param>
        /// <param name="index">Page index to get</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="actionName">Action name for links</param>
        /// <returns></returns>
        public static PagedList<T> ToPagedList<T>(this IQueryable<T> source, Int32? index, Int32 pageSize, String actionName)
        {
            return new PagedList<T>(source, index.HasValue ? index.Value - 1 : 0, pageSize, actionName);
        }

        /// <summary>
        /// Create paged list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">Source items list</param>
        /// <param name="index">Page index to get</param>
        /// <param name="actionName">Action name for links</param>
        /// <returns></returns>
        public static PagedList<T> ToPagedList<T>(this IQueryable<T> source, Int32? index, String actionName)
        {
            return new PagedList<T>(source, index.HasValue ? index.Value - 1 : 0, DefaultPageSize, actionName);
        }

        /// <summary>
        /// Create paged list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">Source items list</param>
        /// <param name="index">Page index to get</param>
        /// <param name="actionName">Action name for links</param>
        /// <param name="routeValues">Additional route values</param>
        /// <returns></returns>
        public static PagedList<T> ToPagedList<T>(this IQueryable<T> source, Int32? index, String actionName, Object routeValues)
        {
            return new PagedList<T>(source, index.HasValue ? index.Value - 1 : 0, DefaultPageSize, actionName, routeValues);
        }

        /// <summary>
        /// Create paged list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">Source items list</param>
        /// <param name="index">Page index to get</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="actionName">Action name for links</param>
        /// <param name="routeValues">Additional route values</param>
        /// <returns></returns>
        public static PagedList<T> ToPagedList<T>(this IQueryable<T> source, Int32? index, Int32 pageSize, String actionName, Object routeValues)
        {
            return new PagedList<T>(source, index.HasValue ? index.Value - 1 : 0, pageSize, actionName, routeValues);
        }

        #endregion Extension methods

        /// <summary>
        /// Create action parameter dictionary
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="defaultValues"></param>
        /// <returns></returns>
        public static RouteValueDictionary JoinParameters(String key, Object value, RouteValueDictionary defaultValues)
        {
            RouteValueDictionary routeValueDictionary = (defaultValues == null ? new RouteValueDictionary() : new RouteValueDictionary(defaultValues));

            routeValueDictionary.Add(key, value);

            return routeValueDictionary;
        }

        #endregion Public static methods
    }
}
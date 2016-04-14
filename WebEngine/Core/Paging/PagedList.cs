using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;

namespace Sobits.WebEngine.Core.Paging
{
    /// <summary>
    /// Paged list implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedList<T> : List<T>, IPagedList
    {
        #region Public constructors

        protected PagedList()
        { }

        public PagedList(IQueryable<T> source, Int32 index, Int32 pageSize, String actionName)
        {
            TotalCount = source.Count();
            PageSize = pageSize;
            TotalPages = TotalCount / PageSize + ((TotalCount % PageSize) > 0 ? 1 : 0);

            PageIndex = index < 0 ? 0 : index;
            ActionName = actionName;
            AddRange(source.Skip(PageIndex * pageSize).Take(pageSize).ToList());
        }

        public PagedList(IEnumerable<T> source, Int32 index, Int32 pageSize, String actionName)
            : this(source.AsQueryable<T>(), index, pageSize, actionName)
        { }

        public PagedList(IQueryable<T> source, Int32 index, Int32 pageSize, String actionName, Object routeValues)
            : this(source, index, pageSize, actionName)
        {
            RouteValues = new RouteValueDictionary(routeValues);
        }

        #endregion Public constructors

        #region Public properties

        #region IPagedList implementation

        /// <summary>
        /// Total No of items
        /// </summary>
        public Int32 TotalCount
        {
            get;
            private set;
        }

        /// <summary>
        /// Current page index
        /// </summary>
        public Int32 PageIndex
        {
            get;
            set;
        }

        /// <summary>
        /// Page size
        /// </summary>
        public Int32 PageSize
        {
            get;
            set;
        }

        /// <summary>
        /// Number of pages
        /// </summary>
        public Int32 TotalPages
        {
            get;
            private set;
        }

        /// <summary>
        /// Does prev page exist?
        /// </summary>
        public Boolean HasPreviousPage
        {
            get
            {
                return (PageIndex > 0);
            }
        }

        /// <summary>
        /// Does next page exist?
        /// </summary>
        public Boolean HasNextPage
        {
            get
            {
                return (PageIndex * PageSize) < TotalCount;
            }
        }

        /// <summary>
        /// Action name for URLs
        /// </summary>
        public String ActionName
        {
            get;
            private set;
        }

        /// <summary>
        /// Additional route values
        /// </summary>
        public RouteValueDictionary RouteValues
        {
            get;
            private set;
        }

        #endregion IPagedList implementation

        #endregion Public properties
    }
}
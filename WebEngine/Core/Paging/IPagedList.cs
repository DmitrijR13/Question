using System;
using System.Collections;
using System.Web.Routing;

namespace Sobits.WebEngine.Core.Paging
{
    /// <summary>
    /// Paged list interface
    /// </summary>
    public interface IPagedList : IList
    {
        #region Public properties

        /// <summary>
        /// Total No of items
        /// </summary>
        Int32 TotalCount
        {
            get;
        }

        /// <summary>
        /// Current page index
        /// </summary>
        Int32 PageIndex
        {
            get;
            set;
        }

        /// <summary>
        /// Page size
        /// </summary>
        Int32 PageSize
        {
            get;
            set;
        }

        /// <summary>
        /// Number of pages
        /// </summary>
        Int32 TotalPages
        {
            get;
        }

        /// <summary>
        /// Does prev page exist?
        /// </summary>
        Boolean HasPreviousPage
        {
            get;
        }

        /// <summary>
        /// Does next page exist?
        /// </summary>
        Boolean HasNextPage
        {
            get;
        }

        /// <summary>
        /// Action name for URLs
        /// </summary>
        String ActionName
        {
            get;
        }

        /// <summary>
        /// Additional route values
        /// </summary>
        RouteValueDictionary RouteValues
        {
            get;
        }

        #endregion Public properties
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Questionnaire.WebSite.Core
{

    /// <summary>
    /// 
    /// </summary>
    public abstract class AbstractWebViewPage : WebViewPage<dynamic>
    {
        /// <summary>
        /// When overridden in a derived class, gets the <see cref="T:System.Web.HttpSessionState"/> object for the current HTTP request.
        /// </summary>
        /// <returns>Session data for the current request.</returns>
        public new UserSession Session
        {
            get
            {
                return ((AbstractController)ViewContext.Controller).Session;
            }
        }
    }

    /// <summary>
    /// Abstract web view page
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public abstract class AbstractWebViewPage<TModel> : System.Web.Mvc.WebViewPage<TModel>
    {
        /// <summary>
        /// When overridden in a derived class, gets the <see cref="T:System.Web.HttpSessionState"/> object for the current HTTP request.
        /// </summary>
        /// <returns>Session data for the current request.</returns>
        public new UserSession Session
        {
            get
            {
                return ((AbstractController)ViewContext.Controller).Session;
            }
        }
    }

}
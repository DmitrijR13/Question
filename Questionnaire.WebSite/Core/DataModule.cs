using System;
using System.Web;
using System.Linq;
using System.Security.Principal;

using Sobits.WebEngine;
using Sobits.Story.Logic;

namespace Questionnaire.WebSite.Core
{
    public class DataModule : IHttpModule
    {
        #region IHttpModule Members

        public void Dispose()
        {
        }

        public virtual void Init(HttpApplication context)
        {
            context.PreRequestHandlerExecute += OnPreRequestHandlerExecute;
        }

        #endregion

        protected IUserSession CreateUserSession(System.Web.SessionState.HttpSessionState session)
        {
            return new UserSession(session);
        }

        /// <summary>
        /// Handle PreRequest
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPreRequestHandlerExecute(Object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication)sender;

            if (application.Context.Session != null)
            {
                IUserSession userSession = CreateUserSession(application.Context.Session);

                if (userSession.UserEmail != String.Empty)
                {
                    application.Context.User = userSession.User;
                }
            }
        }
    }
}

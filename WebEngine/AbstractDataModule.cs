//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Web;
//using System.Web.SessionState;
//using System.Security.Principal;

//using Sobits.DataEngine;
//using Sobits.DataEngine.BLL;


//namespace Sobits.WebEngine
//{
//    public abstract class AbstractDataModule : IHttpModule
//    {
//        #region IHttpModule Members

//        public void Dispose()
//        {
//        }

//        public virtual void Init(HttpApplication context)
//        {
//            context.PreRequestHandlerExecute += OnPreRequestHandlerExecute;
//            context.Error += OnError;
//        }

//        #endregion

//        #region protected methods

//        protected abstract IUserSession CreateUserSession(HttpSessionState session);

//        protected abstract IDataSession CreateDataSession(IPrincipal user);

//        protected abstract IDataSession CreateDataSession();

//        protected virtual void OnDataSessionCreated(IUserSession userSession, IDataSession dataSession)
//        {
//        }

//        protected virtual void OnError(Exception exception)
//        {
//        }

//        #endregion proteceted methods

//        /// <summary>
//        /// Handle exception
//        /// </summary>
//        /// <param name="sender"></param>
//        /// <param name="e"></param>
//        private void OnError(object sender, EventArgs e)
//        {
//            Exception exception = HttpContext.Current.Server.GetLastError();
//            OnError(exception);
//        }

//        /// <summary>
//        /// Handle PreRequest
//        /// </summary>
//        /// <param name="sender"></param>
//        /// <param name="e"></param>
//        private void OnPreRequestHandlerExecute(Object sender, EventArgs e)
//        {
//            //todo: Chrome multible calls PreRequestHandlerExecute. Should call once
//            HttpApplication application = (HttpApplication)sender;

//            if (application.Context.Session != null)
//            {
//                IUserSession userSession = CreateUserSession(application.Context.Session);

//                IDataSession dataSession = CreateDataSession();

//                if (userSession.UserEmail != String.Empty)
//                {
//                    application.Context.User = userSession.User;
//                    dataSession = CreateDataSession(userSession.User);
//                }

//                OnDataSessionCreated(userSession, dataSession);
//            }
//        }
//    }
//}

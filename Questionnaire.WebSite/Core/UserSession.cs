using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Security.Principal;

using Sobits.WebEngine;
using Sobits.Story.Logic;
using Sobits.Story.Logic.Repo;
using Sobits.Story.Logic.BLL;
using System.Web.Security;


namespace Questionnaire.WebSite.Core
{
    public class UserSession : AbstractUserSession
    {
        #region constructors

        internal UserSession(HttpSessionStateBase session)
            : base(session)
        {
        }

        internal UserSession(HttpSessionState session)
            : base(session)
        {
        }

        #endregion constructors

        #region public properties

        /// <summary>
        /// Session user
        /// </summary>
        public new User User
        {
            get
            {
                return (User)base.User;
            }
        }

        /// <summary>
        /// Слепой?
        /// </summary>
        public Boolean IsBlind
        {
            get
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies.Get("IsBlind");
                if (cookie != null && cookie.Value != null)
                {
                    return Convert.ToBoolean(cookie.Value);
                }
                else
                {
                    return false;
                }
            }
            set
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies.Get("IsBlind");

                if (cookie == null)
                {
                    cookie = new HttpCookie("IsBlind", Convert.ToString(value));
                    cookie.Expires = DateTime.Now.AddDays(1);
                    HttpContext.Current.Response.Cookies.Add(cookie);
                }
                else
                {
                    cookie = new HttpCookie("IsBlind", Convert.ToString(value));
                    cookie.Expires = DateTime.Now.AddDays(1);
                    HttpContext.Current.Response.Cookies.Set(cookie);
                }
            }
        }

        #endregion public properties

        #region public methods
        
        #endregion public methods

        #region protected methods

        protected override void OnLogin(IPrincipal user)
        {
            base.OnLogin(user);
            
            //SessionFactory.Instance.CloseSession();
            //SessionFactory.Instance.OpenSession(user);
        }

        protected override void OnLogout()
        {
            HttpSession.Clear();
            //SessionFactory.Instance.CloseSession();
            //SessionFactory.Instance.OpenSession();
        }

        protected override IPrincipal GetUser(String email)
        {
            if (String.IsNullOrEmpty(email)) { return null; }
            IUserRepo repo = RepoFactory.Instance.GetRepo<IUserRepo>();
            return repo.GetAll().Where(x => x.Email == email).SingleOrDefault();
        }

        #endregion protected methods

        #region protected properties

        #endregion protected properties

        #region provate fields

        #endregion provate fields
    }
}
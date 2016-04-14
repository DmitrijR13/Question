using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Security.Principal;

using System.Web.Security;


namespace Sobits.WebEngine
{
    /// <summary>
    /// Abstract user session
    /// </summary>
    public abstract class AbstractUserSession : IUserSession
    {
        #region constructors

        protected AbstractUserSession(HttpSessionStateBase session)
        {
            _session = session;
        }

        public AbstractUserSession(HttpSessionState session)
        {
            _session = new HttpSessionStateWrapper(session);
        }

        #endregion constructors

        #region public properties

        public String UserEmail
        {
            get
            {
                return HttpContext.Current.User.Identity.Name;
            }
            protected set
            {
                if (_rememberMe) 
                {
                    FormsAuthentication.SetAuthCookie(value, _rememberMe); 
                }
                else 
                {
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(value, false, 40);
                    String encTicket = FormsAuthentication.Encrypt(ticket);
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    HttpContext.Current.Response.Cookies.Add(cookie);
                }
            }
        }

        public IPrincipal User
        {
            get
            {
                if (_user == null && UserEmail != String.Empty)
                {
                    _user = GetUser(UserEmail);
                }

                return _user;
            }
        }

        #endregion public properties

        #region public methods
        
        public virtual void DoLogin(IPrincipal user, Boolean rememberMe)
        {
            if (user == null) { throw new ArgumentNullException(); }
            _rememberMe = rememberMe;
            UserEmail = ((AbstractUser)user).Email;
            HttpContext.Current.User = user;
            OnLogin(user);
        }

        public void Logout()
        {
            UserEmail = String.Empty;
            _user = null;
            HttpContext.Current.User = null;
            FormsAuthentication.SignOut();
            OnLogout();
        }

        #endregion public methods

        #region protected methods

        protected abstract IPrincipal GetUser(String email);

        protected virtual void OnLogin(IPrincipal user)
        {
        }

        protected virtual void OnLogout()
        {
        }

        #endregion protected methods

        #region protected properties

        protected HttpSessionStateBase HttpSession
        {
            get
            {
                return _session;
            }
        }

        #endregion protected properties

        #region private fields

        private readonly HttpSessionStateBase _session;
        private IPrincipal _user;
        private Boolean _rememberMe;

        #endregion private fields
    }
}

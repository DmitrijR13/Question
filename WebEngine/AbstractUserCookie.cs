//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Web;
//using System.Web.SessionState;
//using System.Security.Principal;

//using Sobits.DataEngine.BLL;


//namespace Sobits.WebEngine
//{
//    /// <summary>
//    /// Abstract user session
//    /// </summary>
//    public abstract class AbstractUserCookie : IUserSession
//    {
//        #region constructors

//        /// <summary>
//        /// Constructor
//        /// </summary>
//        /// <param name="expiresMin">expire cookies in min</param>
//        public AbstractUserCookie(Int32 expiresMin)
//        {
//            _expiresMin = expiresMin;
//        }

//        /// <summary>
//        /// Constructor
//        /// </summary>
//        /// <param name="expiresMin">expire cookies in min</param>
//        /// <param name="companyExpiresYear">expire cookies in year for company</param>
//        public AbstractUserCookie(Int32 expiresMin,Int32 companyExpiresYear)
//        {
//            _expiresMin = expiresMin;
//            _companyExpiresYear = companyExpiresYear;
//        }

//        #endregion constructors

//        #region public properties

//        /// <summary>
//        /// Id of user
//        /// </summary>
//        public Int32 UserID
//        {
//            get
//            {
//                HttpCookie cookie = GetCookie("UserID");

//                if (cookie == null)
//                {
//                    return 0;
//                }

//                return Int32.Parse(cookie.Value.ToString());
//            }
//            protected set
//            {
//                SetCookie("UserID", value.ToString());
//            }
//        }

//        /// <summary>
//        /// User
//        /// </summary>
//        public IPrincipal User
//        {
//            get
//            {
//                if (_user == null && UserID > 0)
//                {
//                    _user = GetUser(UserID);
//                }

//                return _user;
//            }
//        }

//        #endregion public properties

//        #region public methods

//        /// <summary>
//        /// User LogIn
//        /// </summary>
//        /// <param name="user"></param>
//        public virtual void DoLogin(IPrincipal user)
//        {
//            UserID = ((AbstractUser)user).ID;
//            HttpContext.Current.User = user;
//            OnLogin(user);
//        }

//        /// <summary>
//        /// User LogOut
//        /// </summary>
//        public void Logout()
//        {
//            UserID = 0;
//            HttpContext.Current.User = null;
//            OnLogout();
//        }

//        /// <summary>
//        /// Get cookie
//        /// </summary>
//        /// <param name="key">cookie name</param>
//        /// <returns></returns>
//        public HttpCookie GetCookie(String key)
//        {
//            return HttpContext.Current.Request.Cookies.Get(key);
//        }

//        /// <summary>
//        /// Set cookie 
//        /// </summary>
//        /// <param name="key">cookie name</param>
//        /// <param name="value">cookie value</param>
//        public void SetCookie(String key, String value)
//        {
//            if (!String.IsNullOrEmpty(value))
//            {
//                HttpCookie cookie = new HttpCookie(key);
//                cookie.Value = value.ToString();
//                cookie.Expires = DateTime.Now.AddMinutes(_expiresMin);
//                HttpContext.Current.Response.Cookies.Add(cookie);
//            }
//            else
//            {
//                HttpContext.Current.Response.Cookies.Remove(key);
//            }
//        }

//        /// <summary>
//        /// Set cookie 
//        /// </summary>
//        /// <param name="key">cookie name</param>
//        /// <param name="value">cookie value</param>
//        /// <remarks>Default expires is one year, if it's not overridden in conctructor</remarks>
//        public void SetCompanyCookie(String key, String value)
//        {
//            if (!String.IsNullOrEmpty(value))
//            {
//                HttpCookie cookie = new HttpCookie(key);
//                cookie.Value = value.ToString();
//                cookie.Expires = DateTime.Now.AddYears(_companyExpiresYear);
//                HttpContext.Current.Response.Cookies.Add(cookie);
//            }
//            else
//            {
//                HttpContext.Current.Response.Cookies.Remove(key);
//            }
//        }

//        #endregion public methods

//        #region protected methods

//        /// <summary>
//        /// get user
//        /// </summary>
//        /// <param name="id">user id</param>
//        /// <returns></returns>
//        protected abstract IPrincipal GetUser(Int32 id);

//        protected virtual void OnLogin(IPrincipal user)
//        {
//        }

//        protected virtual void OnLogout()
//        {
//        }

//        #endregion protected methods

//        #region private fields

//        private IPrincipal _user;

//        private Int32 _expiresMin;

//        private Int32 _companyExpiresYear=1;

//        #endregion private fields
//    }
//}

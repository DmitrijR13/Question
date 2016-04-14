using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate;

using Sobits.Story.Logic.BLL;
using System.Security.Principal;

namespace Sobits.Story.Logic
{
    internal class DataSession : DataEngine.DataSession, IDataSession
    {
        #region static members

        /// <summary>
        /// Current DataSession
        /// </summary>
        public static IDataSession Current
        {
            get
            {
                return SessionFactory.Instance.GetCurrentDataSession();
            }

        }

        #endregion

        #region constructors

        /// <summary>
        /// Authorized constructor
        /// </summary>
        /// <param name="user"></param>
        /// <param name="context"></param>
        protected internal DataSession(IPrincipal user, ISession context)
            :base(user, context)
        {
        }

        /// <summary>
        /// Anonymous constructor
        /// </summary>
        /// <param name="context"></param>
        protected internal DataSession(ISession context)
            :base(context)
        {
        }

        #endregion constructors

        #region public properties

        /////<summary>
        /////Current session user (null if anonymous session)
        /////</summary>
        //public new BLL.PersonWorker User
        //{
        //    get 
        //    {
        //        return (BLL.PersonWorker)base.User;
        //    }
        //}

        /////<summary>
        /////Session company  (null if anonymous session)
        /////</summary>
        //public BLL.Company Company
        //{
        //    get
        //    {
        //        if (User == null)
        //        {
        //            return null;
        //        }
        //        return User.Company;
        //    }
        //}

        #endregion public properties
    }
}

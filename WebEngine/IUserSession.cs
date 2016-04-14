using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Security.Principal;

namespace Sobits.WebEngine
{
    /// <summary>
    /// User session
    /// </summary>
    public interface IUserSession
    {
        /// <summary>
        /// EMail of current session user (String.Empty if anonymus)
        /// </summary>
        String UserEmail { get; }

        /// <summary>
        /// Current session user
        /// </summary>
        IPrincipal User { get; }

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="user"></param>
        void DoLogin(IPrincipal user, Boolean rememberMe);

        /// <summary>
        /// Logout user
        /// </summary>
        void Logout();
    }
}

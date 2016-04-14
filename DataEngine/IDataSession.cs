using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;

using Sobits.DataEngine.BLL;

namespace Sobits.DataEngine
{
    /// <summary>
    /// Data session
    /// </summary>
    public interface IDataSession : IDisposable
    {
        /// <summary>
        /// Logged user (null if anonymous)
        /// </summary>
        IPrincipal User { get; }

        /// <summary>
        /// True if personalized session
        /// </summary>
        Boolean IsLoggedUser { get; }
    }
}

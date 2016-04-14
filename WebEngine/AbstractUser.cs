using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;

namespace Sobits.WebEngine
{
    /// <summary>
    /// Abstract user
    /// </summary>
    public abstract class AbstractUser : AbstractEntity, IPrincipal
    {
        #region constructors

        /// <summary>
        /// Default constructor for ORM
        /// </summary>
        protected AbstractUser()
        { }

        /// <summary>
        /// Constructor for fake objects
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        public AbstractUser(Guid password, String email)
        {
            Email = email;
            Password = password;
        }

        #endregion constructors

        #region public properties
        
        /// <summary>
        /// User email
        /// </summary>
        public virtual String Email { get; set; }

        /// <summary>
        /// Current password
        /// </summary>
        public virtual Guid Password { get; protected set; }

        #endregion public properties

        #region public methods

        /// <summary>
        /// Check password
        /// </summary>
        /// <param name="password">entered password</param>
        /// <returns>true, if entered password is correct</returns>
        public virtual Boolean CheckPassword(String password)
        {
            return Password.ToString().Equals(password, StringComparison.InvariantCulture);
        }

        /// <summary>
        /// Checking role
        /// </summary>
        public virtual Boolean IsRole(String role)
        {
            return false;
        }

        #endregion public methods

        #region protected properties

        #endregion protected properties

        #region IPrincipal members

        /// <summary>
        /// User identity
        /// </summary>
        IIdentity IPrincipal.Identity
        {
            get
            {
                return new GenericIdentity(Email);
            }
        }

        /// <summary>
        /// Check user in role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        Boolean IPrincipal.IsInRole(String role)
        {
            return IsRole(role);
        }

        #endregion IPrincipal members
    }
}

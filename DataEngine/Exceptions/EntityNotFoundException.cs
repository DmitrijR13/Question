using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sobits.DataEngine.Exceptions
{
    /// <summary>
    /// Entity not found Exception (Throws when entity with ID={0} isn't in database)
    /// </summary>
    public class EntityNotFoundException : Exception
    {
        #region constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public EntityNotFoundException()
            : base()
        { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"></param>
        public EntityNotFoundException(String message)
            : base(message)
        { }

        #endregion constructors
    }
}

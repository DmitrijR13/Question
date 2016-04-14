using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sobits.Story.Logic.BLL;

namespace Sobits.Story.Logic
{
    /// <summary>
    /// Data session interface
    /// </summary>
    public interface IDataSession : DataEngine.IDataSession
    {
        /// <summary>
        /// Session user
        /// </summary>
        //new User User { get; }
    }
}

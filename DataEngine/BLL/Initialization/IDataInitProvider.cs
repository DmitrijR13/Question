using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sobits.DataEngine.BLL.Initialization
{
    /// <summary>
    /// Data initialization provider
    /// </summary>
    public interface IDataInitProvider
    {
        /// <summary>
        /// Initialize data
        /// </summary>
        void Init();
    }
}

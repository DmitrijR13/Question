using Sobits.DataEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sobits.Story.Logic.BLL
{
    /// <summary>
    /// IP адресы
    /// </summary>
    public class IPAddress : AbstractEntity
    {
        /// <summary>
        /// Значение IP адреса
        /// </summary>
        public virtual String Value { get; set; }

        /// <summary>
        /// Название организации
        /// </summary>
        public virtual String NameOrganization { get; set; }
    }
}

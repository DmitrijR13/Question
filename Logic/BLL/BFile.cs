using Sobits.WebEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sobits.Story.Logic.BLL
{
    /// <summary>
    /// Файлы
    /// </summary>
    public class BFile : AbstractEntity
    {
        /// <summary>
        /// Название файла
        /// </summary>
        public virtual String FileName { get; set; }

        /// <summary>
        /// GUID файла
        /// </summary>
        public virtual Guid Guid { get; set; }
    }
}

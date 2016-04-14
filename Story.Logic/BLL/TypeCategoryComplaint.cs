using Sobits.DataEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sobits.Story.Logic.BLL
{
    /// <summary>
    /// Категория
    /// </summary>
    public class TypeCategoryComplaint : AbstractEntity
    {
        /// <summary>
        /// Название
        /// </summary>
        public virtual String Name { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public virtual Decimal Price { get; set; }

        /// <summary>
        /// Удалена?
        /// </summary>
        public virtual Boolean IsDelete { get; set; }
    }
}

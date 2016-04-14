using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire.WebSite.Models
{
    /// <summary>
    /// Модель категорий жалоб
    /// </summary>
    public class TypeCategoryComplaintModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Int32 ID { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public Decimal Price { get; set; }

        /// <summary>
        /// Удалена?
        /// </summary>
        public Boolean IsDelete { get; set; }
    }
}
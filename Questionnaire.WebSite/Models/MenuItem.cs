using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire.WebSite.Models
{
    /// <summary>
    /// Модель пунктов дочернего меню
    /// </summary>
    public class MenuItem
    {
        /// <summary>
        /// Идентификатор html элемента
        /// </summary>
        public String ID { get; set; }

        /// <summary>
        /// Текст меню
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Url (например для img)
        /// </summary>
        public String Url { get; set; }

        /// <summary>
        /// Урл перехода
        /// </summary>
        public String UrlAction { get; set; }

        /// <summary>
        /// Событие OnClick
        /// </summary>
        public String OnClick { get; set; }

        /// <summary>
        /// Дочерние пункты меню
        /// </summary>
        public List<MenuItem> MenuItems { get; set; }
    }
}
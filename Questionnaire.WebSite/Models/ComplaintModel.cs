using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Questionnaire.WebSite.Models
{
    /// <summary>
    /// Модель жалобы заполненной посетителем
    /// </summary>
    public class ComplaintModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Int32 ID { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [Required(ErrorMessage = "*")]
        public String FirstName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public String SecondName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [Required(ErrorMessage = "*")]
        public String LastName { get; set; }

        /// <summary>
        /// Федеральный код МО
        /// </summary>
        [Required(ErrorMessage = "*")]
        public Int32 FederalCodeMO { get; set; }
        
        /// <summary>
        /// Список мед.организаций
        /// </summary>
        public IEnumerable<SelectListItem> MOs { get; set; }

        /// <summary>
        /// Телефон
        /// </summary>
        [Required(ErrorMessage = "*")]
        public String Phone { get; set; }

        /// <summary>
        /// Текст
        /// </summary>
        [Required(ErrorMessage = "*")]
        public String Text { get; set; }

        /// <summary>
        /// Характер обращения (идентификатор)
        /// </summary>
        [Required(ErrorMessage = "*")]
        public Int32 TypeDealID { get; set; }

        /// <summary>
        /// Характер обращения
        /// </summary>
        public String TypeDealValue { get; set; }

        /// <summary>
        /// Список обращений
        /// </summary>
        public IEnumerable<SelectListItem> Deals { get; set; }

        /// <summary>
        /// Состояние (идентификатор)
        /// </summary>
        public Int32? TypeStateID { get; set; }

        /// <summary>
        /// Состояние
        /// </summary>
        public String TypeStateValue { get; set; }

        /// <summary>
        /// Список состояний
        /// </summary>
        public IEnumerable<SelectListItem> States { get; set; }

        /// <summary>
        /// Категория (идентификатор)
        /// </summary>
        public Int32? TypeCategoryID { get; set; }

        /// <summary>
        /// Категория
        /// </summary>
        public String TypeCategoryValue { get; set; }

        /// <summary>
        /// Список категорий
        /// </summary>
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
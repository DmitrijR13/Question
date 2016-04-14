using Sobits.DataEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sobits.Story.Logic.BLL
{
    /// <summary>
    /// Жалобы
    /// </summary>
    public class Complaint : AbstractEntity
    {
        /// <summary>
        /// Имя
        /// </summary>
        public virtual String FirstName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public virtual String SecondName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public virtual String LastName { get; set; }

        /// <summary>
        /// Федеральный код МО
        /// </summary>
        public virtual Int32 FederalCodeMO { get; set; }

        /// <summary>
        /// Телефон
        /// </summary>
        public virtual String Phone { get; set; }

        /// <summary>
        /// Текст
        /// </summary>
        public virtual String Text { get; set; }

        /// <summary>
        /// Характер обращения
        /// </summary>
        public virtual TypeDealComplaint TypeDeal { get; set; }

        /// <summary>
        /// Состояние
        /// </summary>
        public virtual TypeStateComplaint TypeState { get; set; }

        /// <summary>
        /// Категория
        /// </summary>
        public virtual TypeCategoryComplaint TypeCategory { get; set; }
    }
}

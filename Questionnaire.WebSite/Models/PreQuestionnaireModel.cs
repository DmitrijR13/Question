using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Questionnaire.WebSite.Models
{
    /// <summary>
    /// Модель страницы перед началом прохождения анкеты
    /// </summary>
    public class PreQuestionnaireModel
    {
        /// <summary>
        /// Идентификатор раздела
        /// </summary>
        public Int32 CharterID { get; set; }

        /// <summary>
        /// Идентификатор МО организации
        /// </summary>
        [Required(ErrorMessage = "*")]
        public List<Int32> MOOrganizationID { get; set; }

        /// <summary>
        /// Идентификатор СМО организации
        /// </summary>
        [Required(ErrorMessage = "*")]
        public List<Int32> SMOOrganizationID { get; set; }

        /// <summary>
        /// МО организации
        /// </summary>
        public List<MOOrganizationModel> MOOrganizations { get; set; }

        /// <summary>
        /// CМО организации
        /// </summary>
        public List<SMOOrganizationModel> SMOOrganizations { get; set; }

        /// <summary>
        /// Тип открываемой анкеты
        /// </summary>
        public Int32 TypeTransition { get; set; }

        /// <summary>
        /// Работа от плагина
        /// </summary>
        public Boolean IsPlugin { get; set; }
    }
}
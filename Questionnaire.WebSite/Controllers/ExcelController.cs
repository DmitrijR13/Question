using Newtonsoft.Json;
using Questionnaire.WebSite.Core;
using Questionnaire.WebSite.Models;
using Sobits.Story.Logic.BLL;
using Sobits.Story.Logic.DataModel;
using Sobits.Story.Logic.Repo;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;

namespace Questionnaire.WebSite.Controllers
{
    /// <summary>
    /// Контроллер страницы статистики
    /// </summary>
    [Authorize(Roles = "Admin/Index")]
    public class ExcelController : AbstractController
    {
        /// <summary>
        /// Индексная страница
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("1");

            var charterData = RepoFactory.Instance.GetRepo<ICharterRepo>().GetWithoutDelete()
                                                                                           .Select(x => new CharterModel()
                                                                                           {
                                                                                               ID = x.ID,
                                                                                               Name = x.Name
                                                                                           })
                                                                                           .ToList();

            



            wb.SaveAs(@"C:\temp\EzhkhImport_.xlsx");

            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(@"C:\temp\EzhkhImport_.xlsx", contentType, Path.GetFileName(@"C:\temp\EzhkhImport_.xlsx"));
        }
    }

    public class ReportClass
    {
        private String Name { get; set; }

        private String Question { get; set; }

        private String Answer { get; set; }

        private Int32 Count { get; set; }
    }
}

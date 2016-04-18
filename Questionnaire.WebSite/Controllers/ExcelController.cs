using Newtonsoft.Json;
using Questionnaire.WebSite.Core;
using Questionnaire.WebSite.Models;
using Sobits.Story.Logic.BLL;
using Sobits.Story.Logic.DataModel;
using Sobits.Story.Logic.Repo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Oracle.DataAccess.Client;
using Alignment = DocumentFormat.OpenXml.Math.Alignment;

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

            //var charterData = RepoFactory.Instance.GetRepo<ICharterRepo>().GetWithoutDelete()
            //                                                                               .Select(x => new CharterModel()
            //                                                                               {
            //                                                                                   ID = x.ID,
            //                                                                                   Name = x.Name
            //                                                                               })
            //                                                                               .ToList();

            String connStr = ConfigurationManager.ConnectionStrings["LocalDBConnectionString"].ConnectionString;

            string cmdText = @"SELECT lc.str_name, tq.str_value, ta.str_value, count(tvq.id)
                                FROM TBL_Answer ta
                                LEFT JOIN tbl_voting_question tvq on ta.ID = tvq.INT_ANSWERID
                                LEFT JOIN TBL_QUESTION tq on tq.ID = tvq.INT_QUESTIONID or tq.id = ta.INT_QUESTIONID
                                LEFT JOIN LKP_CHARTER lc on tq.INT_CHARTERID = lc.id
                                WHERE bit_delete = 0 and tq.bit_isdelete = 0 and ta.bit_isdelete = 0
                                group by lc.str_name, tq.str_value, ta.str_value
                                order by lc.str_name, tq.str_value, ta.str_value";
            //string cmdText = "SELECT id FROM realty_object where mu_name LIKE '%Волжский р-н%'";
            //string cmdText = "SELECT * FROM employees";
            OracleConnection conn = new OracleConnection(connStr);
            OracleCommand cmd = new OracleCommand(cmdText, conn);
            //cmd.Parameters.Add("surname", surname);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);
            }
            catch (Exception e)
            {
                
            }

            string anketa = "";
            string question = "";
            ws.Column(1).Width = 30;
            ws.Column(2).Width = 70;
            ws.Column(3).Width = 40;
            ws.Column(4).Width = 15;
            ws.Cell(1, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(1, 1).Style.Border.TopBorder = XLBorderStyleValues.Thin;
            ws.Cell(1, 1).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            ws.Cell(1, 1).Style.Border.RightBorder = XLBorderStyleValues.Thin;
            ws.Cell(1, 2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(1, 2).Style.Border.TopBorder = XLBorderStyleValues.Thin;
            ws.Cell(1, 2).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            ws.Cell(1, 2).Style.Border.RightBorder = XLBorderStyleValues.Thin;
            ws.Cell(1, 3).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(1, 3).Style.Border.TopBorder = XLBorderStyleValues.Thin;
            ws.Cell(1, 3).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            ws.Cell(1, 3).Style.Border.RightBorder = XLBorderStyleValues.Thin;
            ws.Cell(1, 4).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Cell(1, 4).Style.Border.TopBorder = XLBorderStyleValues.Thin;
            ws.Cell(1, 4).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            ws.Cell(1, 4).Style.Border.RightBorder = XLBorderStyleValues.Thin;
            ws.Column(1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            ws.Column(2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            ws.Column(3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            ws.Column(4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            ws.Cell(1, 1).Value = "Наименование анкеты";
            ws.Cell(1, 2).Value = "Вопрос";
            ws.Cell(1, 3).Value = "Ответ";
            ws.Cell(1, 4).Value = @"Количество 
ответов";

            ws.Column(1).Style.Alignment.WrapText = true;
            ws.Column(2).Style.Alignment.WrapText = true;
            ws.Column(3).Style.Alignment.WrapText = true;

           
            ws.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Cell(1, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Cell(1, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Cell(1, 1).Style.Alignment.WrapText = true;
            ws.Cell(1, 2).Style.Alignment.WrapText = true;
            ws.Cell(1, 3).Style.Alignment.WrapText = true;

            int row = 2;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (anketa != dt.Rows[i][0].ToString())
                {
                    anketa = dt.Rows[i][0].ToString();
                    ws.Cell(row, 1).Value = anketa;
                    question = "";
                }
                if (question != dt.Rows[i][1].ToString())
                {
                    question = dt.Rows[i][1].ToString();
                    ws.Cell(row, 2).Value = question;
                }
                ws.Cell(row, 3).Value = dt.Rows[i][2].ToString();
                ws.Cell(row, 4).Value = dt.Rows[i][3].ToString();
                ws.Cell(row, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                ws.Cell(row, 1).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                ws.Cell(row, 1).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                ws.Cell(row, 1).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                ws.Cell(row, 2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                ws.Cell(row, 2).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                ws.Cell(row, 3).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                ws.Cell(row, 3).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                ws.Cell(row, 3).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                ws.Cell(row, 3).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                ws.Cell(row, 4).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                ws.Cell(row, 4).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                ws.Cell(row, 4).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                row++;
            }


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

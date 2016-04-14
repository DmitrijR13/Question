using Calabonga.Mvc.Extensions;
using Oracle.DataAccess.Client;
using Questionnaire.WebSite.Core;
using Questionnaire.WebSite.Core.Grid;
using Questionnaire.WebSite.Models;
using Sobits.Story.Logic.BLL;
using Sobits.Story.Logic.Repo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Questionnaire.WebSite.Controllers
{
    /// <summary>
    /// Жалобы
    /// </summary>
    public class ComplaintController : Controller
    {
        #region отправление жалобы гостем

        /// <summary>
        /// Страница заполнения жалобы
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult FillComplaint()
        {
            ComplaintModel model = new ComplaintModel()
            {
                ID = 0,
                Deals = GetForDDL(RepoFactory.Instance.GetRepo<ITypeDealComplaintRepo>().GetAllExists()),
                MOs = GetMOs()
            };

            return View(model);
        }

        /// <summary>
        /// Страница заполнения жалобы
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Captcher()]
        public ActionResult FillComplaint(ComplaintModel model)
        {
            if (ModelState.IsValid)
            {
                Complaint complaint = new Complaint();
                complaint.FirstName = model.FirstName;
                complaint.SecondName = model.SecondName;
                complaint.LastName = model.LastName;
                complaint.FederalCodeMO = model.FederalCodeMO;
                complaint.TypeDeal = RepoFactory.Instance.GetRepo<ITypeDealComplaintRepo>().Get(model.TypeDealID);
                complaint.TypeState = RepoFactory.Instance.GetRepo<ITypeStateComplaintRepo>().GetFirstExists();
                complaint.Phone = model.Phone;
                complaint.Text = model.Text;

                complaint = RepoFactory.Instance.GetRepo<IComplaintRepo>().Save(complaint);

                return RedirectToAction("ReadyComplaint", "Complaint", new { complaintID = complaint.ID });
            }
            else
            {
                //if (!ModelState.IsValidField("captcha"))
                if (1==2)
                {
                    ModelState.Remove("captcha");
                    ModelState.AddModelError("captcha", "Ошибочный ввод");
                }

                model.Deals = GetForDDL(RepoFactory.Instance.GetRepo<ITypeDealComplaintRepo>().GetAllExists());
                model.MOs = GetMOs();

                return View(model);
            }
        }

        /// <summary>
        /// Жалоба записана...
        /// </summary>
        /// <param name="complaintID"></param>
        /// <returns></returns>
        public ActionResult ReadyComplaint(Int32 complaintID)
        {
            return View(complaintID);
        }

        private List<SelectListItem> GetMOs()
        {
            String querySql = @"SELECT mcod, nam_mok FROM (SELECT mcod, nam_mok, ROW_NUMBER() OVER (PARTITION BY mcod ORDER BY mcod ASC) rn FROM tbl_f003) WHERE rn = 1 ORDER BY nam_mok";

            OracleConnection con = new OracleConnection(Configuration.DBExternalConnectionString);
            con.Open();

            OracleCommand command = new OracleCommand(querySql, con);
            command.CommandTimeout = 600000;

            DataTable dt = new DataTable();

            OracleDataAdapter da = new OracleDataAdapter(command);
            da.Fill(dt);

            con.Close();

            List<SelectListItem> data = new List<SelectListItem>();
            foreach (DataRow dr in dt.Rows)
            {
                SelectListItem entity = new SelectListItem();
                entity.Text = dr["nam_mok"] != DBNull.Value ? dr["nam_mok"].ToString() : String.Empty;
                entity.Value = dr["mcod"] != DBNull.Value ? dr["mcod"].ToString() : String.Empty;

                data.Add(entity);
            }

            return data;
        }

        /// <summary>
        /// Выводит на экран "каптчу"
        /// </summary>
        /// <returns></returns>
        public ActionResult Captcha()
        {
            return new CaptchaResult();
        }

        #endregion

        #region админка: список жалоб

        /// <summary>
        /// Страница жалоб
        /// </summary>
        /// <returns></returns>
        public ActionResult Complaint()
        {
            return View();
        }

        /// <summary>
        /// Данные для грида
        /// </summary>
        [HttpPost]
        public JsonResult LoadDataComplaint(GridOptions options)
        {
            IQueryable<Complaint> data = RepoFactory.Instance.GetRepo<IComplaintRepo>().GetAll();

            JsonResult result = new JsonResult
            {
                Data = options.GetResult(data, x => new ComplaintModel()
                {
                    ID = x.ID,
                    FirstName = x.FirstName,
                    SecondName = x.SecondName,
                    LastName = x.LastName,
                    FederalCodeMO = x.FederalCodeMO,
                    TypeDealValue = x.TypeDeal.Name,
                    TypeStateValue = x.TypeState != null ? x.TypeState.Name : String.Empty,
                    Phone = x.Phone
                })
            };

            return result;
        }

        /// <summary>
        /// Открывает окно редактирования жалоб
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditComplaint(Int32 complaintID)
        {
            ModelState.Clear();

            ViewBag.IsNew = true;

            Complaint complaint = RepoFactory.Instance.GetRepo<IComplaintRepo>().Get(complaintID);

            if (complaint != null)
            {
                ComplaintModel model = new ComplaintModel()
                {
                    ID = complaint.ID,
                    FirstName = complaint.FirstName,
                    SecondName = complaint.SecondName,
                    LastName = complaint.LastName,
                    FederalCodeMO = complaint.FederalCodeMO,
                    Text = complaint.Text,
                    Phone = complaint.Phone,
                    TypeCategoryID = complaint.TypeCategory != null ? complaint.TypeCategory.ID : 0,
                    TypeCategoryValue = complaint.TypeCategory != null ? complaint.TypeCategory.Name : String.Empty,
                    TypeDealID = complaint.TypeDeal.ID,
                    TypeDealValue = complaint.TypeDeal.Name,
                    TypeStateID = complaint.TypeState != null ? complaint.TypeState.ID : 0,
                    TypeStateValue = complaint.TypeState != null ? complaint.TypeState.Name : String.Empty,
                    Categories = GetForDDL(RepoFactory.Instance.GetRepo<ITypeCategoryComplaintRepo>().GetAllExists()),
                    Deals = GetForDDL(RepoFactory.Instance.GetRepo<ITypeDealComplaintRepo>().GetAllExists()),
                    States = GetForDDL(RepoFactory.Instance.GetRepo<ITypeStateComplaintRepo>().GetAllExists())
                };

                return PartialView("_EditComplaint", model);
            }
            else
            {
                ComplaintModel model = new ComplaintModel()
                {
                    ID = 0,
                    FirstName = String.Empty,
                    SecondName = String.Empty,
                    LastName = String.Empty,
                    FederalCodeMO = 0,
                    Text = String.Empty,
                    Phone = String.Empty,
                    TypeCategoryID = 0,
                    TypeCategoryValue = String.Empty,
                    TypeDealID = 0,
                    TypeDealValue = String.Empty,
                    TypeStateID = 0,
                    TypeStateValue = String.Empty,
                    Categories = GetForDDL(RepoFactory.Instance.GetRepo<ITypeCategoryComplaintRepo>().GetAllExists()),
                    Deals = GetForDDL(RepoFactory.Instance.GetRepo<ITypeDealComplaintRepo>().GetAllExists()),
                    States = GetForDDL(RepoFactory.Instance.GetRepo<ITypeStateComplaintRepo>().GetAllExists())
                };

                return PartialView("_EditComplaint", model);
            }
        }

        /// <summary>
        /// Получает данные редактирования жалоб
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditComplaint(ComplaintModel model)
        {
            if (ModelState.IsValid)
            {
                Complaint complaint = RepoFactory.Instance.GetRepo<IComplaintRepo>().Get(model.ID);
                if (complaint == null)
                {
                    throw new Exception("Чувак, а такого обращения не было!..");
                }

                complaint.TypeCategory = model.TypeCategoryID.HasValue ? RepoFactory.Instance.GetRepo<ITypeCategoryComplaintRepo>().Get(model.TypeCategoryID.Value) : null;
                complaint.TypeDeal = RepoFactory.Instance.GetRepo<ITypeDealComplaintRepo>().Get(model.TypeDealID);
                complaint.TypeState = model.TypeStateID.HasValue ? RepoFactory.Instance.GetRepo<ITypeStateComplaintRepo>().Get(model.TypeStateID.Value) : null;
                
                complaint = RepoFactory.Instance.GetRepo<IComplaintRepo>().Save(complaint);

                return RedirectToAction("EditComplaint", "Complaint", new { complaintID = complaint.ID });
            }
            else
            {
                ViewBag.IsNew = false;

                model.Categories = GetForDDL(RepoFactory.Instance.GetRepo<ITypeCategoryComplaintRepo>().GetAllExists());
                model.Deals = GetForDDL(RepoFactory.Instance.GetRepo<ITypeDealComplaintRepo>().GetAllExists());
                model.States = GetForDDL(RepoFactory.Instance.GetRepo<ITypeStateComplaintRepo>().GetAllExists());

                return PartialView("_EditComplaint", model);
            }
        }

        #region private methods

        private List<SelectListItem> GetForDDL(IQueryable<TypeCategoryComplaint> data)
        {
            return data.Select(x => new SelectListItem()
                        {
                            Text = x.Name,
                            Value = x.ID.ToString()
                        })
                        .ToList();
        }

        private List<SelectListItem> GetForDDL(IQueryable<TypeDealComplaint> data)
        {
            return data.Select(x => new SelectListItem()
                        {
                            Text = x.Name,
                            Value = x.ID.ToString()
                        })
                        .ToList();
        }

        private List<SelectListItem> GetForDDL(IQueryable<TypeStateComplaint> data)
        {
            return data.Select(x => new SelectListItem()
                        {
                            Text = x.Name,
                            Value = x.ID.ToString()
                        })
                        .ToList();
        }

        #endregion

        #endregion

        #region админка: характер обращений

        /// <summary>
        /// Страница справочника "Характер обращений"
        /// </summary>
        /// <returns></returns>
        public ActionResult Deal()
        {
            return View();
        }

        /// <summary>
        /// Данные для грида
        /// </summary>
        [HttpPost]
        public JsonResult LoadDataDeal(GridOptions options)
        {
            IQueryable<TypeDealComplaint> data = RepoFactory.Instance.GetRepo<ITypeDealComplaintRepo>().GetAll();

            JsonResult result = new JsonResult
            {
                Data = options.GetResult(data, x => new TypeDealComplaintModel()
                {
                    ID = x.ID,
                    Name = x.Name,
                    IsDelete = x.IsDelete
                })
            };

            return result;
        }

        /// <summary>
        /// Открывает окно редактирования характера обращения
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditDeal(Int32 dealID)
        {
            ModelState.Clear();

            ViewBag.IsNew = true;

            TypeDealComplaint deal = RepoFactory.Instance.GetRepo<ITypeDealComplaintRepo>().Get(dealID);

            if (deal != null)
            {
                TypeDealComplaintModel model = new TypeDealComplaintModel()
                {
                    ID = deal.ID,
                    Name = deal.Name,
                    IsDelete = deal.IsDelete
                };

                return PartialView("_EditDeal", model);
            }
            else
            {
                TypeDealComplaintModel model = new TypeDealComplaintModel()
                {
                    ID = 0,
                    Name = String.Empty,
                    IsDelete = false
                };

                return PartialView("_EditDeal", model);
            }
        }

        /// <summary>
        /// Получает данные редактирования характера обращения
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditDeal(TypeDealComplaintModel model)
        {
            if (ModelState.IsValid)
            {
                TypeDealComplaint typeDealComplaint = RepoFactory.Instance.GetRepo<ITypeDealComplaintRepo>().Get(model.ID);
                if (typeDealComplaint == null)
                {
                    typeDealComplaint = new TypeDealComplaint();
                }

                typeDealComplaint.ID = model.ID;
                typeDealComplaint.Name = model.Name;
                typeDealComplaint.IsDelete = model.IsDelete;

                typeDealComplaint = RepoFactory.Instance.GetRepo<ITypeDealComplaintRepo>().Save(typeDealComplaint);

                return RedirectToAction("EditDeal", "Complaint", new { dealID = typeDealComplaint.ID });
            }
            else
            {
                ViewBag.IsNew = false;
                
                return PartialView("_EditDeal", model);
            }
        }

        /// <summary>
        /// Удалить характер обращения
        /// </summary>
        /// <returns></returns>
        public JsonResult DeleteDeal(Int32 dealID)
        {
            TypeDealComplaint deal = RepoFactory.Instance.GetRepo<ITypeDealComplaintRepo>().Get(dealID);
            deal.IsDelete = true;

            RepoFactory.Instance.GetRepo<ITypeDealComplaintRepo>().Save(deal);

            return Json("success", JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region админка: состояние обращений

        /// <summary>
        /// Страница справочника "Состояние обращений"
        /// </summary>
        /// <returns></returns>
        public ActionResult State()
        {
            return View();
        }

        /// <summary>
        /// Данные для грида
        /// </summary>
        [HttpPost]
        public JsonResult LoadDataState(GridOptions options)
        {
            IQueryable<TypeStateComplaint> data = RepoFactory.Instance.GetRepo<ITypeStateComplaintRepo>().GetAll();

            JsonResult result = new JsonResult
            {
                Data = options.GetResult(data, x => new TypeStateComplaintModel()
                {
                    ID = x.ID,
                    Name = x.Name,
                    IsDelete = x.IsDelete
                })
            };

            return result;
        }

        /// <summary>
        /// Открывает окно редактирования состояния обращения
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditState(Int32 stateID)
        {
            ModelState.Clear();

            ViewBag.IsNew = true;

            TypeStateComplaint state = RepoFactory.Instance.GetRepo<ITypeStateComplaintRepo>().Get(stateID);

            if (state != null)
            {
                TypeStateComplaintModel model = new TypeStateComplaintModel()
                {
                    ID = state.ID,
                    Name = state.Name,
                    IsDelete = state.IsDelete
                };

                return PartialView("_EditState", model);
            }
            else
            {
                TypeStateComplaintModel model = new TypeStateComplaintModel()
                {
                    ID = 0,
                    Name = String.Empty,
                    IsDelete = false
                };

                return PartialView("_EditState", model);
            }
        }

        /// <summary>
        /// Получает данные редактирования состояния обращения
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditState(TypeStateComplaintModel model)
        {
            if (ModelState.IsValid)
            {
                TypeStateComplaint typeStateComplaint = RepoFactory.Instance.GetRepo<ITypeStateComplaintRepo>().Get(model.ID);
                if (typeStateComplaint == null)
                {
                    typeStateComplaint = new TypeStateComplaint();
                }

                typeStateComplaint.ID = model.ID;
                typeStateComplaint.Name = model.Name;
                typeStateComplaint.IsDelete = model.IsDelete;

                typeStateComplaint = RepoFactory.Instance.GetRepo<ITypeStateComplaintRepo>().Save(typeStateComplaint);

                return RedirectToAction("EditState", "Complaint", new { dealID = typeStateComplaint.ID });
            }
            else
            {
                ViewBag.IsNew = false;

                return PartialView("_EditState", model);
            }
        }

        /// <summary>
        /// Удалить состояние обращения
        /// </summary>
        /// <returns></returns>
        public JsonResult DeleteState(Int32 stateID)
        {
            TypeStateComplaint state = RepoFactory.Instance.GetRepo<ITypeStateComplaintRepo>().Get(stateID);
            state.IsDelete = true;

            RepoFactory.Instance.GetRepo<ITypeStateComplaintRepo>().Save(state);

            return Json("success", JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region админка: категории обращений

        /// <summary>
        /// Страница справочника "Категории обращений"
        /// </summary>
        /// <returns></returns>
        public ActionResult Category()
        {
            return View();
        }

        /// <summary>
        /// Данные для грида
        /// </summary>
        [HttpPost]
        public JsonResult LoadDataCategory(GridOptions options)
        {
            IQueryable<TypeCategoryComplaint> data = RepoFactory.Instance.GetRepo<ITypeCategoryComplaintRepo>().GetAll();

            JsonResult result = new JsonResult
            {
                Data = options.GetResult(data, x => new TypeCategoryComplaintModel()
                {
                    ID = x.ID,
                    Name = x.Name,
                    Price = x.Price,
                    IsDelete = x.IsDelete
                })
            };

            return result;
        }

        /// <summary>
        /// Открывает окно редактирования категории обращения
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditCategory(Int32 categoryID)
        {
            ModelState.Clear();

            ViewBag.IsNew = true;

            TypeCategoryComplaint category = RepoFactory.Instance.GetRepo<ITypeCategoryComplaintRepo>().Get(categoryID);

            if (category != null)
            {
                TypeCategoryComplaintModel model = new TypeCategoryComplaintModel()
                {
                    ID = category.ID,
                    Name = category.Name,
                    Price = category.Price,
                    IsDelete = category.IsDelete
                };

                return PartialView("_EditCategory", model);
            }
            else
            {
                TypeCategoryComplaintModel model = new TypeCategoryComplaintModel()
                {
                    ID = 0,
                    Name = String.Empty,
                    IsDelete = false
                };

                return PartialView("_EditCategory", model);
            }
        }

        /// <summary>
        /// Получает данные редактирования категории обращения
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditCategory(TypeCategoryComplaintModel model)
        {
            if (ModelState.IsValid)
            {
                TypeCategoryComplaint typeCategoryComplaint = RepoFactory.Instance.GetRepo<ITypeCategoryComplaintRepo>().Get(model.ID);
                if (typeCategoryComplaint == null)
                {
                    typeCategoryComplaint = new TypeCategoryComplaint();
                }

                typeCategoryComplaint.ID = model.ID;
                typeCategoryComplaint.Name = model.Name;
                typeCategoryComplaint.Price = model.Price;
                typeCategoryComplaint.IsDelete = model.IsDelete;

                typeCategoryComplaint = RepoFactory.Instance.GetRepo<ITypeCategoryComplaintRepo>().Save(typeCategoryComplaint);

                return RedirectToAction("EditCategory", "Complaint", new { dealID = typeCategoryComplaint.ID });
            }
            else
            {
                ViewBag.IsNew = false;

                return PartialView("_EditCategory", model);
            }
        }

        /// <summary>
        /// Удалить категорию обращения
        /// </summary>
        /// <returns></returns>
        public JsonResult DeleteCategory(Int32 categoryID)
        {
            TypeCategoryComplaint category = RepoFactory.Instance.GetRepo<ITypeCategoryComplaintRepo>().Get(categoryID);
            category.IsDelete = true;

            RepoFactory.Instance.GetRepo<ITypeCategoryComplaintRepo>().Save(category);

            return Json("success", JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}

using Questionnaire.WebSite.Core;
using Questionnaire.WebSite.Core.Grid;
using Questionnaire.WebSite.Models;
using Sobits.Story.Logic;
//using Sobits.DataEngine;
using Sobits.Story.Logic.BLL;
using Sobits.Story.Logic.Enumerable;
using Sobits.Story.Logic.Repo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Questionnaire.WebSite.Controllers
{
    /// <summary>
    /// Контроллер админки
    /// </summary>
    [Authorize(Roles = "Admin/Index")]
    public class AdminController : AbstractController
    {
        /// <summary>
        /// Индексная страница
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Строит меню в админке
        /// </summary>
        /// <returns></returns>
        public ActionResult MenuItems()
        {
            List<Charter> charters = RepoFactory.Instance.GetRepo<ICharterRepo>().GetWithoutDelete().ToList();

            List<MenuItem> menu = new List<MenuItem>();

            List<MenuItem> menuDaughter = new List<MenuItem>();

            foreach (Charter item in charters)
            {
                MenuItem itemMenu = new MenuItem()
                {
                    ID = "questions_" + item.ID.ToString(),
                    Name = item.Name,
                    UrlAction = "#",
                    OnClick = "getQuestion(" + item.ID.ToString() + ", '" + item.Name + "')",
                    MenuItems = null
                };

                menuDaughter.Add(itemMenu);
            }

            // Вопросы
            MenuItem questions = new MenuItem()
            {
                ID = "questions",
                Name = "Вопросы",
                UrlAction = "#",
                OnClick = "",
                Url = Url.Content("~/Content/images/statistic_menuadminka.png"),
                MenuItems = menuDaughter
            };

            menu.Add(questions);

            // Пользователи
            MenuItem users = new MenuItem()
            {
                ID = "users",
                Name = "Пользователи",
                UrlAction = "#",
                Url = Url.Content("~/Content/images/statistic_menuadminka.png"),
                OnClick = "getUser()",
                MenuItems = null
            };

            menu.Add(users);

            // Категории
            MenuItem categories = new MenuItem()
            {
                ID = "categories",
                Name = "Категории",
                UrlAction = "#",
                OnClick = "getCategory()",
                MenuItems = null
            };

            menu.Add(categories);

            // IP адреса
            MenuItem ipAddresses = new MenuItem()
            {
                ID = "ipAddress",
                Name = "IP организаций",
                UrlAction = "#",
                OnClick = "getIPAddress()",
                MenuItems = null
            };

            menu.Add(ipAddresses);

            // Роли
            MenuItem roles = new MenuItem()
            {
                ID = "role",
                Name = "Роли",
                UrlAction = "#",
                OnClick = "getRole()",
                MenuItems = null
            };

            menu.Add(roles);

            // МО организации
            MenuItem moOrganizations = new MenuItem()
            {
                ID = "moOrganizations",
                Name = "Мед.организации",
                UrlAction = "#",
                OnClick = "getMOOrganization()",
                MenuItems = null
            };

            menu.Add(moOrganizations);

            // СМО организации
            MenuItem smoOrganizations = new MenuItem()
            {
                ID = "smoOrganizations",
                Name = "Страх. мед.организации",
                UrlAction = "#",
                OnClick = "getSMOOrganization()",
                MenuItems = null
            };

            menu.Add(smoOrganizations);

            return PartialView("_MenuItems", menu);
        }

        #region методы меню Вопросы

        /// <summary>
        /// Данные для грида
        /// </summary>
        [HttpPost]
        public JsonResult LoadDataQuestion(GridOptions options, Int32 charterID)
        {
            IQueryable<Question> data = RepoFactory.Instance.GetRepo<IQuestionRepo>().GetByCharter(charterID);

            JsonResult result = new JsonResult
            {
                Data = options.GetResult(data, x => new QuestionModel()
                {
                    ID = x.ID,
                    TextQuestion = x.Value,
                    TypeQuestionName = x.TypeQuestion == TypeQuestion.DropDownList ? "Выпадающий список" :
                                       x.TypeQuestion == TypeQuestion.FromTo ? "От... до..." :
                                       x.TypeQuestion == TypeQuestion.Multiple ? "Множественный выбор" :
                                       x.TypeQuestion == TypeQuestion.RadioButton ? "Одиночный выбор" :
                                       x.TypeQuestion == TypeQuestion.Text ? "Текст" :
                                       String.Empty
                })
            };

            return result;
        }

        /// <summary>
        /// Открывает окно редактирования вопроса
        /// </summary>
        /// <param name="questionID">ID вопроса</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditQuestion(Int32 questionID, Int32 charterID)
        {
            ModelState.Clear();

            ViewBag.IsNew = true;
            GetQuestions(charterID);

            Question question = RepoFactory.Instance.GetRepo<IQuestionRepo>().Get(questionID);

            if (question != null)
            {
                QuestionModel model = new QuestionModel()
                {
                    ID = question.ID,
                    CharterID = question.Charter.ID,
                    NameCharter = question.Charter.Name,
                    NumberSequence = question.NumberSequence,
                    TextQuestion = question.Value,
                    TypeQuestion = (Int32)question.TypeQuestion,
                    Answers = question
                                .Answers
                                .Where(x => !x.IsDelete)
                                .Select(x => new AnswerModel()
                                {
                                    ID = x.ID,
                                    TextAnswer = x.Value,
                                    TextAnswerAdditional1 = x.ValueAdditional_1,
                                    TextAnswerAdditional2 = x.ValueAdditional_2,
                                    NumberSequence = x.NumberSequence,
                                    Score = x.Score,
                                    NextQuestionID = x.NextQuestion != null ? x.NextQuestion.ID : 0
                                })
                                .OrderBy(x => x.NumberSequence)
                                .ToList()
                };

                if (model.Answers == null || (model.Answers != null && model.Answers.Count == 0))
                {
                    model.Answers = new List<AnswerModel>();

                    model.Answers.Add(new AnswerModel()
                    {
                        ID = 0,
                        Score = 0,
                        NumberSequence = 0,
                        TextAnswer = String.Empty,
                        TextAnswerAdditional1 = String.Empty,
                        TextAnswerAdditional2 = String.Empty
                    });
                }

                return PartialView("_EditQuestion", model);
            }
            else
            {
                QuestionModel model = new QuestionModel()
                {
                    ID = 0,
                    TextQuestion = String.Empty,
                    NumberSequence = 0,
                    TypeQuestion = 2,
                    CharterID = charterID,
                    NameCharter = String.Empty,
                    Answers = new List<AnswerModel>()
                };

                AnswerModel emptyAnswer = new AnswerModel()
                {
                    ID = 0,
                    Score = 0,
                    NumberSequence = 0,
                    TextAnswer = String.Empty,
                    TextAnswerAdditional1 = String.Empty,
                    TextAnswerAdditional2 = String.Empty
                };

                model.Answers.Add(emptyAnswer);
                
                return PartialView("_EditQuestion", model);
            }
        }

        /// <summary>
        /// Получает данные редактирования вопроса
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditQuestion(QuestionModel model)
        {
            if (ModelState.IsValid)
            {
                model.Answers = model.Answers.Where(x => !String.IsNullOrEmpty(x.TextAnswer)).ToList();

                Question question = RepoFactory.Instance.GetRepo<IQuestionRepo>().Get(model.ID);
                if (question == null)
                {
                    question = new Question();
                    question.DateCreate = DateTime.Now;
                }

                question.ID = model.ID;
                question.NumberSequence = model.NumberSequence;
                question.TypeQuestion = (TypeQuestion)model.TypeQuestion;
                question.Charter = RepoFactory.Instance.GetRepo<ICharterRepo>().Get(model.CharterID);
                question.DateUpdate = DateTime.Now;
                question.Description = String.Empty;
                question.Value = model.TextQuestion;

                question = RepoFactory.Instance.GetRepo<IQuestionRepo>().Save(question);

                List<Answer> answers = RepoFactory.Instance.GetRepo<IAnswerRepo>().GetByQuestion(question.ID).ToList();
                                
                List<Answer> answersAddUpdate = 
                                    model.Answers
                                         .Join(
                                            answers,
                                            itemModel => itemModel.ID,
                                            itemEntity => itemEntity.ID,
                                            (itemModel, itemEntity) => new Answer()
                                            {
                                                ID = itemModel.ID,
                                                DateCreate = itemEntity.DateCreate,
                                                DateUpdate = DateTime.Now,
                                                Description = String.Empty,
                                                NumberSequence = itemModel.NumberSequence,
                                                Question = question,
                                                Score = itemModel.Score,
                                                Value = itemModel.TextAnswer,
                                                ValueAdditional_1 = itemModel.TextAnswerAdditional1,
                                                ValueAdditional_2 = itemModel.TextAnswerAdditional2,
                                                IsDelete = false,
                                                NextQuestion = itemModel.NextQuestionID.HasValue && itemModel.NextQuestionID.Value != 0 ? RepoFactory.Instance.GetRepo<IQuestionRepo>().Get(itemModel.NextQuestionID.Value) : null
                                            }
                                        )
                                        .Union(
                                            model.Answers.Where(x => x.ID == 0)
                                                         .Select(x => new Answer()
                                                         {
                                                             ID = x.ID,
                                                             DateCreate = DateTime.Now,
                                                             DateUpdate = DateTime.Now,
                                                             Description = String.Empty,
                                                             NumberSequence = x.NumberSequence,
                                                             Question = question,
                                                             Score = x.Score,
                                                             Value = x.TextAnswer,
                                                             ValueAdditional_1 = x.TextAnswerAdditional1,
                                                             ValueAdditional_2 = x.TextAnswerAdditional2,
                                                             IsDelete = false,
                                                             NextQuestion = x.NextQuestionID.HasValue && x.NextQuestionID.Value != 0 ? RepoFactory.Instance.GetRepo<IQuestionRepo>().Get(x.NextQuestionID.Value) : null
                                                         })
                                        )
                                        .ToList();

                List<Answer> answersDelete = answers.Where(x => !model.Answers.Select(t => t.ID).Contains(x.ID)).ToList();
                
                answersAddUpdate = RepoFactory.Instance.GetRepo<IAnswerRepo>().SaveList(answersAddUpdate);

                RepoFactory.Instance.GetRepo<IAnswerRepo>().SaveDeleteList(answersDelete);

                return RedirectToAction("EditQuestion", "Admin", new { questionID = question.ID, charterID = model.CharterID });
            }
            else
            {
                GetQuestions(model.CharterID);
                ViewBag.IsNew = false;
                if (model.Answers == null) 
                {
                    model.Answers = new List<AnswerModel>();
                }

                return PartialView("_EditQuestion", model);
            }
        }
        
        /// <summary>
        /// Удалить вопрос
        /// </summary>
        /// <returns></returns>
        public JsonResult DeleteQuestion(Int32 questionID, Int32 charterID)
        {
            Question question = RepoFactory.Instance.GetRepo<IQuestionRepo>().Get(questionID);
            question.IsDelete = true;

            RepoFactory.Instance.GetRepo<IQuestionRepo>().Save(question);

            return Json("success", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Получить страницу ответов из справочников
        /// </summary>
        /// <param name="type">Тип справочника</param>
        /// <returns></returns>
        public ActionResult GetAnswersFromLkp(Int32 type, Int32 charterID)
        {
            GetQuestions(charterID);

            QuestionModel model = new QuestionModel();

            List<AnswerModel> data = new List<AnswerModel>();
            if (type == 0)
            {
                data = RepoFactory.Instance.GetRepo<IMOOrganizationRepo>().GetAll()
                                                                          .Select(x => new AnswerModel()
                                                                          {
                                                                              ID = 0,
                                                                              TextAnswer = x.Name,
                                                                              TextAnswerAdditional1 = x.Code.ToString()
                                                                          })
                                                                          .ToList();
            }
            else
            {
                data = RepoFactory.Instance.GetRepo<ISMOOrganizationRepo>().GetAll()
                                                                          .Select(x => new AnswerModel()
                                                                          {
                                                                              ID = 0,
                                                                              TextAnswer = x.Name,
                                                                              TextAnswerAdditional1 = x.Code.ToString()
                                                                          })
                                                                          .ToList();
            }

            for (Int32 i = 0; i < data.Count; i++)
            {
                data[i].NumberSequence = i + 1;
            }

            model.Answers = data;

            return PartialView("_ItemAnswers", model);
        }

        #endregion методы меню Вопросы

        #region методы меню Пользователи

        /// <summary>
        /// Данные для грида пользователей
        /// </summary>
        [HttpPost]
        public JsonResult LoadDataUser(GridOptions options)
        {
            IQueryable<User> data = RepoFactory.Instance.GetRepo<IUserRepo>().GetAll();

            JsonResult result = new JsonResult
            {
                Data = options.GetResult(data, x => new UserModel()
                {
                    ID = x.ID,
                    FirstName = x.FirstName,
                    SecondName = x.SecondName,
                    LastName = x.LastName,
                    Email = x.Email,
                    IsOneQuestion = x.IsOneQuestion,
                    NameRole = x.UserRoles != null && 
                               x.UserRoles.FirstOrDefault() != null ?
                                    x.UserRoles.FirstOrDefault().Role.Name : String.Empty
                })
            };

            return result;
        }

        /// <summary>
        /// Открывает окно редактирования пользователя
        /// </summary>
        /// <param name="questionID">ID пользователя</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditUser(Int32 userID)
        {
            GetRoles();
            ViewBag.IsNew = true;

            User user = RepoFactory.Instance.GetRepo<IUserRepo>().Get(userID);

            if (user != null)
            {
                UserModel model = new UserModel()
                {
                    ID = user.ID,
                    FirstName = user.FirstName,
                    SecondName = user.SecondName,
                    LastName = user.LastName,
                    Email = user.Email,
                    IsOneQuestion = user.IsOneQuestion,
                    RoleID = user.UserRoles != null &&
                             user.UserRoles.FirstOrDefault() != null ?
                                user.UserRoles.FirstOrDefault().Role.ID : 0
                };

                return PartialView("_EditUser", model);
            }
            else
            {
                UserModel model = new UserModel();

                return PartialView("_EditUser", model);
            }
        }

        /// <summary>
        /// Получает данные редактирования пользователя
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditUser(UserModel model)
        {
            if (ModelState.IsValid)
            {
                User user = RepoFactory.Instance.GetRepo<IUserRepo>().Get(model.ID);
                if (user == null)
                {
                    user = new User();
                    user.DateCreate = DateTime.Now;
                }

                user.ID = model.ID;
                user.FirstName = model.FirstName;
                user.SecondName = model.SecondName;
                user.LastName = model.LastName;
                user.IsOneQuestion = model.IsOneQuestion;
                user.Email = model.Email; 
                user.DateUpdate = DateTime.Now;
                if (!String.IsNullOrEmpty(model.Password)) 
                { 
                    user.ChangePassword(Crypt.GetHashString(model.Password)); 
                }

                user = RepoFactory.Instance.GetRepo<IUserRepo>().Save(user);

                Role role = RepoFactory.Instance.GetRepo<IRoleRepo>().Get(model.RoleID);
                UserRole userRole = RepoFactory.Instance.GetRepo<IUserRoleRepo>().GetByUser(user.ID);

                if (userRole == null)
                {
                    userRole = new UserRole();
                }

                userRole.Role = role;
                userRole.User = user;

                userRole = RepoFactory.Instance.GetRepo<IUserRoleRepo>().Save(userRole);

                ViewBag.IsNew = true;
            }
            else
            {
                ViewBag.IsNew = false;
            }

            GetRoles();
            return PartialView("_EditUser", model);
        }
    
        #endregion методы меню Пользователи

        #region методы меню Категории

        /// <summary>
        /// Данные для грида категории
        /// </summary>
        [HttpPost]
        public JsonResult LoadDataCategory(GridOptions options)
        {
            IQueryable<Charter> data = RepoFactory.Instance.GetRepo<ICharterRepo>().GetWithoutDelete();

            JsonResult result = new JsonResult
            {
                Data = options.GetResult(data, x => new CharterModel()
                {
                    ID = x.ID,
                    Name = x.Name
                })
            };

            return result;
        }

        /// <summary>
        /// Открывает окно редактирования категории
        /// </summary>
        /// <param name="categoryID">ID категории</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditCategory(Int32 categoryID)
        {
            ViewBag.IsNew = true;

            Charter category = RepoFactory.Instance.GetRepo<ICharterRepo>().Get(categoryID);

            if (category != null)
            {
                CharterModel model = new CharterModel()
                {
                    ID = category.ID,
                    Name = category.Name,
                    ImageID = category.Image != null ? category.Image.ID : 0,
                    ImageName = category.Image != null ? category.Image.FileName : String.Empty
                };

                return PartialView("_EditCategory", model);
            }
            else
            {
                CharterModel model = new CharterModel();

                return PartialView("_EditCategory", model);
            }
        }

        /// <summary>
        /// Получает данные редактирования категории
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditCategory(CharterModel model)
        {
            if (ModelState.IsValid)
            {
                Charter category = RepoFactory.Instance.GetRepo<ICharterRepo>().Get(model.ID);
                if (category == null)
                {
                    category = new Charter();
                }

                category.ID = model.ID;
                category.Name = model.Name;

                String fileName = model.Image != null ? model.Image.FileName : String.Empty;
                if (fileName != String.Empty)
                {
                    if (Path.HasExtension(fileName) && (Path.GetExtension(fileName).ToLower() == ".png" || Path.GetExtension(fileName).ToLower() == ".jpg"))
                    {
                        String extension = Path.GetExtension(fileName);

                        Guid guid = Guid.NewGuid();
                        String newFileName = guid + extension;
                        //String serverFilePath = Server.MapPath(Url.Content(Configuration.PathFileFolder + newFileName));
                        String serverFilePath = Questionnaire.WebSite.Core.Configuration.PathFileFolder + newFileName;

                        HttpPostedFileBase file = model.Image;
                        file.SaveAs(serverFilePath);

                        BFile bFile = new BFile();
                        bFile.FileName = file.FileName;
                        bFile.Guid = guid;

                        bFile = RepoFactory.Instance.GetRepo<IBFileRepo>().Save(bFile);

                        category.Image = bFile;
                    }
                }

                category = RepoFactory.Instance.GetRepo<ICharterRepo>().Save(category);

                ViewBag.IsNew = true;
            }
            else
            {
                ViewBag.IsNew = false;
            }

            return PartialView("_EditCategory", model);
        }

        /// <summary>
        /// Удаление категории
        /// </summary>
        /// <param name="categoryID">ID категории</param>
        /// <returns></returns>
        public JsonResult DeleteCategory(Int32 categoryID)
        {
            Charter charter = RepoFactory.Instance.GetRepo<ICharterRepo>().Get(categoryID);
            charter.IsDelete = true;

            RepoFactory.Instance.GetRepo<ICharterRepo>().Save(charter);

            return Json("success", JsonRequestBehavior.AllowGet);
        }

        #endregion методы меню Категории

        #region методы меню IP организации

        /// <summary>
        /// Данные для грида ip адреса
        /// </summary>
        [HttpPost]
        public JsonResult LoadDataIPAddress(GridOptions options)
        {
            IQueryable<IPAddress> data = RepoFactory.Instance.GetRepo<IIPAddressRepo>().GetAll();

            JsonResult result = new JsonResult
            {
                Data = options.GetResult(data, x => new IPAddressModel()
                {
                    ID = x.ID,
                    NameOrganization = x.NameOrganization,
                    IPAddress = x.Value
                })
            };

            return result;
        }

        /// <summary>
        /// Открывает окно редактирования ip адреса организации
        /// </summary>
        /// <param name="ipAddressID">ID ip адреса</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditIPAddress(Int32 ipAddressID)
        {
            ViewBag.IsNew = true;

            IPAddress ipAddress = RepoFactory.Instance.GetRepo<IIPAddressRepo>().Get(ipAddressID);

            if (ipAddress != null)
            {
                IPAddressModel model = new IPAddressModel()
                {
                    ID = ipAddress.ID,
                    NameOrganization = ipAddress.NameOrganization,
                    IPAddress = ipAddress.Value
                };

                return PartialView("_EditIPAddress", model);
            }
            else
            {
                IPAddressModel model = new IPAddressModel();

                return PartialView("_EditIPAddress", model);
            }
        }

        /// <summary>
        /// Получает данные редактирования ip адреса
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditIPAddress(IPAddressModel model)
        {
            if (ModelState.IsValid)
            {
                IPAddress ipAddress = RepoFactory.Instance.GetRepo<IIPAddressRepo>().Get(model.ID);
                if (ipAddress == null)
                {
                    ipAddress = new IPAddress();
                }

                ipAddress.ID = model.ID;
                ipAddress.NameOrganization = model.NameOrganization;
                ipAddress.Value = model.IPAddress;

                ipAddress = RepoFactory.Instance.GetRepo<IIPAddressRepo>().Save(ipAddress);

                ViewBag.IsNew = true;
            }
            else
            {
                ViewBag.IsNew = false;
            }

            return PartialView("_EditIPAddress", model);
        }

        #endregion методы меню IP организации

        #region методы меню Роли

        /// <summary>
        /// Данные для грида
        /// </summary>
        [HttpPost]
        public JsonResult LoadDataRole(GridOptions options)
        {
            IQueryable<Role> data = RepoFactory.Instance.GetRepo<IRoleRepo>().GetAll();

            JsonResult result = new JsonResult
            {
                Data = options.GetResult(data, x => new RoleModel()
                {
                    ID = x.ID,
                    Name = x.Name
                })
            };

            return result;
        }

        /// <summary>
        /// Открывает окно редактирования роли
        /// </summary>
        /// <param name="roleID">ID роли</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditRole(Int32 roleID)
        {
            ModelState.Clear();

            ViewBag.IsNew = true;

            Role role = RepoFactory.Instance.GetRepo<IRoleRepo>().Get(roleID);
            
            if (role != null)
            {
                role.PermissionRoles = RepoFactory.Instance.GetRepo<IPermissionRoleRepo>().GetByRole(role.ID).ToList();

                RoleModel model = new RoleModel()
                {
                    ID = role.ID,
                    Name = role.Name,
                    Permissions = RepoFactory.Instance.GetRepo<IPermissionRepo>().GetAll()
                                                                                 .ToList()
                                                                                 .Select(x => new PermissionModel()
                                                                                 {
                                                                                     ID = x.ID,
                                                                                     Value = x.Name,
                                                                                     IsChecked = role.PermissionRoles.Select(t => t.Permission.ID).Contains(x.ID) ? true : false
                                                                                 })
                                                                                 .ToList()
                };

                //List<PermissionModel> permissionModels = new List<PermissionModel>();
                //List<Permission> permissions = RepoFactory.Instance.GetRepo<IPermissionRepo>().GetAll().ToList();
                //foreach (Permission item in permissions)
                //{
                //    PermissionModel permissonModel = new PermissionModel();
                //    permissonModel.ID = item.ID;
                //    permissonModel.Value = item.Name;
                //    permissonModel.IsChecked = role.PermissionRoles.Select(t => t.Permission.ID).Contains(item.ID) ? true : false;

                //    permissionModels.Add(permissonModel);
                //}

                //model.Permissions = permissionModels;

                if (model.Permissions == null)
                {
                    model.Permissions = new List<PermissionModel>();
                }

                return PartialView("_EditRole", model);
            }
            else
            {
                RoleModel model = new RoleModel()
                {
                    ID = 0,
                    Name = String.Empty,
                    Permissions = new List<PermissionModel>()
                };

                return PartialView("_EditRole", model);
            }
        }

        /// <summary>
        /// Получает данные редактирования роли
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditRole(RoleModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Permissions == null)
                {
                    model.Permissions = new List<PermissionModel>();
                }
                else
                {
                    model.Permissions = model.Permissions.Where(x => x.IsChecked).ToList();
                }

                Role role = RepoFactory.Instance.GetRepo<IRoleRepo>().Get(model.ID);
                if (role == null)
                {
                    role = new Role();
                }

                role.ID = model.ID;
                role.Name = model.Name;
                role.ServiceName = model.Name;

                role = RepoFactory.Instance.GetRepo<IRoleRepo>().Save(role);

                List<PermissionRole> deleteData = new List<PermissionRole>();
                List<PermissionRole> addData = new List<PermissionRole>();

                // Удаляем связи
                List<PermissionRole> permissionRoles = RepoFactory.Instance.GetRepo<IPermissionRoleRepo>().GetByRole(role.ID).ToList();

                foreach (PermissionRole pr in permissionRoles)
                {
                    if (!model.Permissions.Select(x => x.ID).Contains(pr.Permission.ID))
                    {
                        deleteData.Add(pr);
                    }
                }

                // Добавляем связи
                foreach (PermissionModel item in model.Permissions)
                {
                    PermissionRole pr = RepoFactory.Instance.GetRepo<IPermissionRoleRepo>().GetAll().Where(x => x.Role.ID == role.ID && x.Permission.ID == item.ID).FirstOrDefault();

                    if (pr == null)
                    {
                        PermissionRole prData = new PermissionRole()
                        {
                            Permission = RepoFactory.Instance.GetRepo<IPermissionRepo>().Get(item.ID),
                            Role = role
                        };

                        addData.Add(prData);
                    }
                }

                RepoFactory.Instance.GetRepo<IPermissionRoleRepo>().DeleteList(deleteData);
                RepoFactory.Instance.GetRepo<IPermissionRoleRepo>().SaveList(addData);

                return RedirectToAction("EditRole", "Admin", new { roleID = role.ID });
            }
            else
            {
                ViewBag.IsNew = false;
                if (model.Permissions == null)
                {
                    model.Permissions = new List<PermissionModel>();
                }

                return PartialView("_EditRole", model);
            }
        }

        #endregion методы меню Роли

        #region методы меню МО организаций

        /// <summary>
        /// Данные для грида
        /// </summary>
        [HttpPost]
        public JsonResult LoadDataMOOrganization(GridOptions options)
        {
            IQueryable<MOOrganization> data = RepoFactory.Instance.GetRepo<IMOOrganizationRepo>().GetAll();

            JsonResult result = new JsonResult
            {
                Data = options.GetResult(data, x => new MOOrganizationModel()
                {
                    ID = x.ID,
                    Code = x.Code,
                    Name = x.Name
                })
            };

            return result;
        }

        /// <summary>
        /// Открывает окно редактирования мо организации
        /// </summary>
        /// <param name="moOrganizationID">ID мо организации</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditMOOrganization(Int32 moOrganizationID)
        {
            ModelState.Clear();

            ViewBag.IsNew = true;

            MOOrganization moOrganization = RepoFactory.Instance.GetRepo<IMOOrganizationRepo>().Get(moOrganizationID);

            if (moOrganization != null)
            {
                MOOrganizationModel model = new MOOrganizationModel()
                {
                    ID = moOrganization.ID,
                    Code = moOrganization.Code,
                    Name = moOrganization.Name
                };

                return PartialView("_EditMOOrganization", model);
            }
            else
            {
                MOOrganizationModel model = new MOOrganizationModel()
                {
                    ID = 0,
                    Code = 0,
                    Name = String.Empty
                };

                return PartialView("_EditMOOrganization", model);
            }
        }

        /// <summary>
        /// Получает данные редактирования мо организации
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditMOOrganization(MOOrganizationModel model)
        {
            if (ModelState.IsValid)
            {
                MOOrganization moOrganization = RepoFactory.Instance.GetRepo<IMOOrganizationRepo>().Get(model.ID);
                if (moOrganization == null)
                {
                    moOrganization = new MOOrganization();
                }

                moOrganization.ID = model.ID;
                moOrganization.Code = model.Code;
                moOrganization.Name = model.Name;
                
                moOrganization = RepoFactory.Instance.GetRepo<IMOOrganizationRepo>().Save(moOrganization);

                return RedirectToAction("EditMOOrganization", "Admin", new { moOrganizationID = moOrganization.ID });
            }
            else
            {
                ViewBag.IsNew = false;

                return PartialView("_EditMOOrganization", model);
            }
        }

        #endregion методы меню МО организаций

        #region методы меню СМО организаций

        /// <summary>
        /// Данные для грида
        /// </summary>
        [HttpPost]
        public JsonResult LoadDataSMOOrganization(GridOptions options)
        {
            IQueryable<SMOOrganization> data = RepoFactory.Instance.GetRepo<ISMOOrganizationRepo>().GetAll();

            JsonResult result = new JsonResult
            {
                Data = options.GetResult(data, x => new SMOOrganizationModel()
                {
                    ID = x.ID,
                    Code = x.Code,
                    Name = x.Name
                })
            };

            return result;
        }

        /// <summary>
        /// Открывает окно редактирования смо организации
        /// </summary>
        /// <param name="smoOrganizationID">ID смо организации</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditSMOOrganization(Int32 smoOrganizationID)
        {
            ModelState.Clear();

            ViewBag.IsNew = true;

            SMOOrganization smoOrganization = RepoFactory.Instance.GetRepo<ISMOOrganizationRepo>().Get(smoOrganizationID);

            if (smoOrganization != null)
            {
                SMOOrganizationModel model = new SMOOrganizationModel()
                {
                    ID = smoOrganization.ID,
                    Code = smoOrganization.Code,
                    Name = smoOrganization.Name
                };

                return PartialView("_EditSMOOrganization", model);
            }
            else
            {
                SMOOrganizationModel model = new SMOOrganizationModel()
                {
                    ID = 0,
                    Code = 0,
                    Name = String.Empty
                };

                return PartialView("_EditSMOOrganization", model);
            }
        }

        /// <summary>
        /// Получает данные редактирования смо организации
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditSMOOrganization(SMOOrganizationModel model)
        {
            if (ModelState.IsValid)
            {
                SMOOrganization smoOrganization = RepoFactory.Instance.GetRepo<ISMOOrganizationRepo>().Get(model.ID);
                if (smoOrganization == null)
                {
                    smoOrganization = new SMOOrganization();
                }

                smoOrganization.ID = model.ID;
                smoOrganization.Code = model.Code;
                smoOrganization.Name = model.Name;

                smoOrganization = RepoFactory.Instance.GetRepo<ISMOOrganizationRepo>().Save(smoOrganization);

                return RedirectToAction("EditSMOOrganization", "Admin", new { smoOrganizationID = smoOrganization.ID });
            }
            else
            {
                ViewBag.IsNew = false;

                return PartialView("_EditSMOOrganization", model);
            }
        }

        #endregion методы меню СМО организаций
    }
}

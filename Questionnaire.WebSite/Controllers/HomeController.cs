using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Questionnaire.WebSite.Core;

using Sobits.Story.Logic.BLL;
using Sobits.Story.Logic.Repo;
//using Sobits.DataEngine;
using Questionnaire.WebSite.Models;
using Sobits.Story.Logic;

namespace Questionnaire.WebSite.Controllers
{
    /// <summary>
    /// Контроллер главной страницы
    /// </summary>
    public class HomeController : AbstractController
    {
        /// <summary>
        /// Индексная страница
        /// </summary>
        public ActionResult Index()
        {

            //Charter charter = new Charter();
            //charter.Name = "Раздел 0";

            //RepoFactory.Instance.GetRepo<ICharterRepo>().Save(charter);

            //Role role = new Role();
            //role.Name = "Статист";
            //role.ServiceName = "Statist";

            //RepoFactory.Instance.GetRepo<IRoleRepo>().Save(role);

            //role = new Role();
            //role.Name = "Администратор";
            //role.ServiceName = "Admin";

            //role = RepoFactory.Instance.GetRepo<IRoleRepo>().Save(role);

            //Permission permission = new Permission();
            //permission.Name = "Статистика";
            //permission.Value = "Statistic/Index";

            //RepoFactory.Instance.GetRepo<IPermissionRepo>().Save(permission);

            //permission = new Permission();
            //permission.Name = "Администрирование";
            //permission.Value = "Admin/Index";

            //permission = RepoFactory.Instance.GetRepo<IPermissionRepo>().Save(permission);

            //PermissionRole permissionRole = new PermissionRole();
            //permissionRole.Permission = permission;
            //permissionRole.Role = role;

            //RepoFactory.Instance.GetRepo<IPermissionRoleRepo>().Save(permissionRole);

            //User user = new User();
            //user.FirstName = "admin";
            //user.SecondName = "admin";
            //user.LastName = "admin";
            //user.Email = "admin";
            //user.ChangePassword(Crypt.GetHashString("admin"));
            //user.DateCreate = DateTime.Now;
            //user.DateUpdate = DateTime.Now;

            //user = RepoFactory.Instance.GetRepo<IUserRepo>().Save(user);

            //UserRole userRole = new UserRole();
            //userRole.Role = role;
            //userRole.User = user;

            //RepoFactory.Instance.GetRepo<IUserRoleRepo>().Save(userRole);

            


            List<Charter> charters = RepoFactory.Instance.GetRepo<ICharterRepo>().GetWithoutDelete().ToList();
            List<CharterModel> result = new List<CharterModel>();

            foreach (Charter item in charters)
            {
                CharterModel model = new CharterModel();
                model.ID = item.ID;
                model.Name = item.Name;
                model.ImageID = item.Image != null ? item.Image.ID : 0;
                model.ImageName = item.Image != null ? item.Image.FileName : String.Empty;

                result.Add(model);
            }

            return View(result);
        }

        /// <summary>
        /// Создает разметку главного меню
        /// </summary>
        public ActionResult CreateMenuItems()
        {
            List<MenuItem> menu = new List<MenuItem>();

            List<Charter> charters = RepoFactory.Instance.GetRepo<ICharterRepo>().GetWithoutDelete().ToList();

            foreach (Charter item in charters)
            {
                MenuItem menuItem = new MenuItem()
                {
                    ID = "questionnaire_" + item.ID.ToString(),
                    Name = item.Name,
                    UrlAction = Url.Action("PreQuestionnaire", "Home", new { charterID = item.ID }),
                    MenuItems = null
                };

                menu.Add(menuItem);
            }

            return PartialView("_CreateMenuItems", menu);
        }

        /// <summary>
        /// Информация перед анкетированием
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PreQuestionnaire(Int32 charterID)
        {
            PreQuestionnaireModel model = new PreQuestionnaireModel()
            {
                IsPlugin = false,
                CharterID = charterID,
                MOOrganizationID = new List<Int32>(),
                SMOOrganizationID = new List<Int32>(),
                TypeTransition = 0,
                MOOrganizations = RepoFactory.Instance.GetRepo<IMOOrganizationRepo>().GetAll()
                                                                                     .Select(x => new MOOrganizationModel()
                                                                                     {
                                                                                         ID = x.ID,
                                                                                         Code = x.Code,
                                                                                         Name = x.Name
                                                                                     })
                                                                                     .ToList(),
                SMOOrganizations = RepoFactory.Instance.GetRepo<ISMOOrganizationRepo>().GetAll()
                                                                                     .Select(x => new SMOOrganizationModel()
                                                                                     {
                                                                                         ID = x.ID,
                                                                                         Code = x.Code,
                                                                                         Name = x.Name
                                                                                     })
                                                                                     .ToList()
            };

            if (model.MOOrganizations == null) { model.MOOrganizations = new List<MOOrganizationModel>(); }
            if (model.SMOOrganizations == null) { model.SMOOrganizations = new List<SMOOrganizationModel>(); }

            return View("PreQuestionnaire", model);
        }

        /// <summary>
        /// Информация перед анкетированием
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PreQuestionnaire(PreQuestionnaireModel model)
        {
            //if (ModelState.IsValid)
            //{
                IPAddress ipAddress = RepoFactory.Instance.GetRepo<IIPAddressRepo>().GetByIPAddress(HttpContext.Request.UserHostAddress);

            //MOOrganization MOOrganization =
            //    RepoFactory.Instance.GetRepo<IMOOrganizationRepo>()
            //        .GetByCodeAndName("Общие вопросы").FirstOrDefault();

            //SMOOrganization SMOOrganization =
            //    RepoFactory.Instance.GetRepo<ISMOOrganizationRepo>()
            //        .GetByCodeAndName("Общие вопросы").FirstOrDefault();

            MOOrganization MOOrganization =
               RepoFactory.Instance.GetRepo<IMOOrganizationRepo>()
                   .GetByCodeAndName("ГБУЗ СО \"Кинельская ЦБ города и района\"").FirstOrDefault();

            SMOOrganization SMOOrganization =
                RepoFactory.Instance.GetRepo<ISMOOrganizationRepo>()
                    .GetByCodeAndName("АО СК \"АСКОМЕД\"").FirstOrDefault();

            Voting voting = new Voting()
                {
                    ID = 0,
                    FirstName = String.Empty,
                    SecondName = String.Empty,
                    LastName = String.Empty,
                    IsAnonymous = true,
                    IPAddress = ipAddress,
                    DateVote = DateTime.Now,
                    MOOrganization = MOOrganization,
                    SMOOrganization = SMOOrganization,
                };

                voting = RepoFactory.Instance.GetRepo<IVotingRepo>().Save(voting);

                if (model.TypeTransition == 0)
                {
                    return RedirectToAction("Index", "QuestionnaireFull", new { charterID = model.CharterID, votingID = voting.ID, isPlugin = model.IsPlugin });
                }
                else
                {
                    return RedirectToAction("Index", "Questionnaire", new { charterID = model.CharterID, votingID = voting.ID, isPlugin = model.IsPlugin });
                }
            //}
            //else
            //{
            //    if (model.MOOrganizations == null) { model.MOOrganizations = new List<MOOrganizationModel>(); }
            //    if (model.SMOOrganizations == null) { model.SMOOrganizations = new List<SMOOrganizationModel>(); }

            //    return View("PreQuestionnaire", model);
            //}
        }

        /// <summary>
        /// Найти МО организацию
        /// </summary>
        /// <returns></returns>
        public JsonResult FindMOOrganization(String text)
        {
            List<MOOrganizationModel> data = RepoFactory.Instance.GetRepo<IMOOrganizationRepo>()
                                                                                   .GetByCodeAndName(text)
                                                                                   .Select(x => new MOOrganizationModel()
                                                                                   {
                                                                                       ID = x.ID,
                                                                                       Code = x.Code,
                                                                                       Name = x.Name
                                                                                   })
                                                                                   .ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Найти СМО организацию
        /// </summary>
        /// <returns></returns>
        public JsonResult FindSMOOrganization(String text)
        {
            List<SMOOrganizationModel> data = RepoFactory.Instance.GetRepo<ISMOOrganizationRepo>()
                                                                                   .GetByCodeAndName(text)
                                                                                   .Select(x => new SMOOrganizationModel()
                                                                                   {
                                                                                       ID = x.ID,
                                                                                       Code = x.Code,
                                                                                       Name = x.Name
                                                                                   })
                                                                                   .ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
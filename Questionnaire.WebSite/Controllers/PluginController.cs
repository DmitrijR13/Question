using Questionnaire.WebSite.Core;
using Questionnaire.WebSite.Models;
using Sobits.Story.Logic.BLL;
using Sobits.Story.Logic.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Questionnaire.WebSite.Controllers
{
    /// <summary>
    /// Контроллер эмулирующий анкетирование для встраивания
    /// </summary>
    public class PluginController : AbstractController
    {
        /// <summary>
        /// Индексная страница
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
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
        /// Начать анкету в плагине
        /// </summary>
        /// <returns></returns>
        public ActionResult Questionnaire(Int32 charterID)
        {
            PluginModel model = new PluginModel()
            {
                CharterID = charterID
            };

            return View(model);
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
                IsPlugin = true,
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
            if (ModelState.IsValid)
            {
                IPAddress ipAddress = RepoFactory.Instance.GetRepo<IIPAddressRepo>().GetByIPAddress(HttpContext.Request.UserHostAddress);

                Voting voting = new Voting()
                {
                    ID = 0,
                    FirstName = String.Empty,
                    SecondName = String.Empty,
                    LastName = String.Empty,
                    IsAnonymous = true,
                    IPAddress = ipAddress,
                    DateVote = DateTime.Now,
                    MOOrganization = RepoFactory.Instance.GetRepo<IMOOrganizationRepo>().Get(model.MOOrganizationID.FirstOrDefault()),
                    SMOOrganization = RepoFactory.Instance.GetRepo<ISMOOrganizationRepo>().Get(model.SMOOrganizationID.FirstOrDefault()),
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
            }
            else
            {
                if (model.MOOrganizations == null) { model.MOOrganizations = new List<MOOrganizationModel>(); }
                if (model.SMOOrganizations == null) { model.SMOOrganizations = new List<SMOOrganizationModel>(); }

                return View("PreQuestionnaire", model);
            }
        }
    }
}

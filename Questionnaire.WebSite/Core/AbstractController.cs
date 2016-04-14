using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Sobits.Story.Logic.Repo;
using Sobits.Story.Logic.BLL;
using Questionnaire.WebSite.Models;

namespace Questionnaire.WebSite.Core
{
    public abstract class AbstractController : Controller
    {
        #region public properties

        public new UserSession Session
        {
            get
            {
                if (_sesion == null)
                {
                    _sesion = new UserSession(base.Session);
                }

                return _sesion;
            }
        }

        public new FileSession SessionFile
        {
            get
            {
                if (_fileSession == null)
                {
                    _fileSession = new FileSession(base.Session);
                }

                return _fileSession;
            }
        }

        #endregion public properties

        #region справочники и данные для DDL

        /// <summary>
        /// Получить все вопросы данного раздела
        /// </summary>
        public void GetQuestions(Int32 charterID)
        {
            IEnumerable<Question> data = RepoFactory.Instance.GetRepo<IQuestionRepo>().GetByCharter(charterID)
                                                                                      .ToList();

            IEnumerable<SelectListItem> list = from area in data
                                               select new SelectListItem
                                               {
                                                   Text = area.Value,
                                                   Value = area.ID.ToString(),
                                                   Selected = false
                                               };

            ViewBag.Questions = list;
        }

        /// <summary>
        /// Получить все разделы
        /// </summary>
        public void GetCharters()
        {
            IEnumerable<Charter> data = RepoFactory.Instance.GetRepo<ICharterRepo>().GetWithoutDelete()
                                                                                    .ToList();

            IEnumerable<SelectListItem> list = from area in data
                                               select new SelectListItem
                                               {
                                                   Text = area.Name,
                                                   Value = area.ID.ToString(),
                                                   Selected = false
                                               };

            ViewBag.Charters = list;
        }

        /// <summary>
        /// Получить все роли
        /// </summary>
        public void GetRoles()
        {
            IEnumerable<Role> data = RepoFactory.Instance.GetRepo<IRoleRepo>().GetAll()
                                                                              .ToList();

            IEnumerable<SelectListItem> list = from area in data
                                               select new SelectListItem
                                               {
                                                   Text = area.Name,
                                                   Value = area.ID.ToString(),
                                                   Selected = false
                                               };

            ViewBag.Roles = list;
        }

        /// <summary>
        /// Получить все разрешения
        /// </summary>
        public void GetPermissions()
        {
            IEnumerable<Permission> data = RepoFactory.Instance.GetRepo<IPermissionRepo>().GetAll()
                                                                                          .ToList();

            IEnumerable<SelectListItem> list = from area in data
                                               select new SelectListItem
                                               {
                                                   Text = area.Name,
                                                   Value = area.ID.ToString(),
                                                   Selected = false
                                               };

            ViewBag.Permissions = list;
        }


        #endregion справочники и данные для DDL

        #region private properites

        private UserSession _sesion;
        private FileSession _fileSession;

        #endregion private properites
    }
}
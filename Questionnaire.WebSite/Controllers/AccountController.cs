using Questionnaire.WebSite.Core;
using Questionnaire.WebSite.Models;
using Sobits.Story.Logic;
//using Sobits.DataEngine;
using Sobits.Story.Logic.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace Questionnaire.WebSite.Controllers
{
    public class AccountController : AbstractController
    {
        /// <summary>
        /// Log on
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult LogOn()
        {
            return View();
        }

        /// <summary>
        /// Log on
        /// </summary>
        [HttpPost]
        public ActionResult LogOn(LogOnModel model, String returnUrl)
        {
            if (ModelState.IsValid)
            {
                IUserRepo repo = RepoFactory.Instance.GetRepo<IUserRepo>();
                var account = repo.GetByEmail(model.Email, Crypt.GetHashString(model.Password));

                if (account != null)
                {
                    Session.DoLogin(account, model.RememberMe);

                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    //ModelState.AddModelError("", "");
                }
            }

            // Появление этого сообщения означает наличие ошибки; повторное отображение формы
            return View(model);
        }

        /// <summary>
        /// Log off
        /// </summary>
        /// <returns></returns>
        public ActionResult LogOff()
        {
            Session.Logout();

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Изменение статуса слепого/не слепого
        /// </summary>
        /// <returns></returns>
        public JsonResult ChangeIsBlind()
        {
            if (Session.IsBlind)
            {
                Session.IsBlind = false;
            }
            else
            {
                Session.IsBlind = true;
            }

            return Json("success", JsonRequestBehavior.AllowGet);
        }

        #region Status Codes

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // Полный список кодов состояния см. по адресу http://go.microsoft.com/fwlink/?LinkID=177550
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Имя пользователя уже существует. Введите другое имя пользователя.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "Имя пользователя для данного адреса электронной почты уже существует. Введите другой адрес электронной почты.";

                case MembershipCreateStatus.InvalidPassword:
                    return "Указан недопустимый пароль. Введите допустимое значение пароля.";

                case MembershipCreateStatus.InvalidEmail:
                    return "Указан недопустимый адрес электронной почты. Проверьте значение и повторите попытку.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "Указан недопустимый ответ на вопрос для восстановления пароля. Проверьте значение и повторите попытку.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "Указан недопустимый вопрос для восстановления пароля. Проверьте значение и повторите попытку.";

                case MembershipCreateStatus.InvalidUserName:
                    return "Указано недопустимое имя пользователя. Проверьте значение и повторите попытку.";

                case MembershipCreateStatus.ProviderError:
                    return "Поставщик проверки подлинности вернул ошибку. Проверьте введенное значение и повторите попытку. Если проблему устранить не удастся, обратитесь к системному администратору.";

                case MembershipCreateStatus.UserRejected:
                    return "Запрос создания пользователя был отменен. Проверьте введенное значение и повторите попытку. Если проблему устранить не удастся, обратитесь к системному администратору.";

                default:
                    return "Произошла неизвестная ошибка. Проверьте введенное значение и повторите попытку. Если проблему устранить не удастся, обратитесь к системному администратору.";
            }
        }

        #endregion
    }
}

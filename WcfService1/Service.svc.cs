using Questionnaire.Service.Models;
using Questionnaire.Service.Setting.Grid;
using Sobits.Story.Logic;
using Sobits.Story.Logic.BLL;
using Sobits.Story.Logic.Enumerable;
using Sobits.Story.Logic.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace Questionnaire.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    public class Service : IService
    {
        #region методы меню IP организации

        /// <summary>
        /// Получить все ip адреса
        /// </summary>
        public List<IPAddressSM> GetAllIPAddress(GridOptions options)
        {
            IQueryable<IPAddress> data = RepoFactory.Instance.GetRepo<IIPAddressRepo>().GetAll();

            List<IPAddressSM> result = options.GetResult(data, x => new IPAddressSM()
            {
                ID = x.ID,
                NameOrganization = x.NameOrganization,
                IPAddress = x.Value
            })
                .rows;

            return result;
        }

        /// <summary>
        /// Получить количество ip адресов
        /// </summary>
        public Int32 GetCountIPAddress(GridOptions options)
        {
            Int32 count = RepoFactory.Instance.GetRepo<IIPAddressRepo>().GetAll().Count();

            return count;
        }

        /// <summary>
        /// Открывает окно редактирования ip адреса организации
        /// </summary>
        /// <param name="ipAddressID">ID ip адреса</param>
        /// <returns></returns>
        public IPAddressSM GetIPAddress(Int32 ipAddressID)
        {
            IPAddress ipAddress = RepoFactory.Instance.GetRepo<IIPAddressRepo>().Get(ipAddressID);

            if (ipAddress != null)
            {
                IPAddressSM model = new IPAddressSM()
                {
                    ID = ipAddress.ID,
                    NameOrganization = ipAddress.NameOrganization,
                    IPAddress = ipAddress.Value
                };

                return model;
            }
            else
            {
                IPAddressSM model = new IPAddressSM();

                return model;
            }
        }

        /// <summary>
        /// Сохраняет данные редактирования ip адреса
        /// </summary>
        /// <returns></returns>
        public void SetIPAddress(IPAddressSM model)
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
        }

        /// <summary>
        /// Удаляет ip адрес
        /// </summary>
        /// <param name="ipAddressID">ID IP адрес компании</param>
        public void DeleteIPAddress(Int32 ipAddressID)
        {
            IPAddress ipAddress = RepoFactory.Instance.GetRepo<IIPAddressRepo>().Get(ipAddressID);

            if (ipAddress != null)
            {
                RepoFactory.Instance.GetRepo<IIPAddressRepo>().Delete(ipAddress);
            }
        }

        #endregion методы меню IP организации

        #region методы меню Роли

        /// <summary>
        /// Получить все роли в проекте
        /// </summary>
        public List<RoleSM> GetAllRole(GridOptions options)
        {
            IQueryable<Role> data = RepoFactory.Instance.GetRepo<IRoleRepo>().GetAll();

            return options.GetResult(data, x => new RoleSM()
            {
                ID = x.ID,
                Name = x.Name
            })
                .rows;
        }

        /// <summary>
        /// Получить количество всех ролей в проекте
        /// </summary>
        public Int32 GetCountRole(GridOptions options)
        {
            Int32 count = RepoFactory.Instance.GetRepo<IRoleRepo>().GetAll().Count();

            return count;
        }

        /// <summary>
        /// Получить все роли в проекте
        /// </summary>
        public List<RoleSM> GetSimpleAllRole()
        {
            List<RoleSM> data = RepoFactory.Instance.GetRepo<IRoleRepo>().GetAll()
                                                                         .Select(x => new RoleSM()
                                                                         {
                                                                             ID = x.ID,
                                                                             Name = x.Name
                                                                         })
                                                                         .ToList();

            return data;
        }

        /// <summary>
        /// Получить количество всех ролей в проекте
        /// </summary>
        public Int32 GetSimpleCountRole()
        {
            Int32 count = RepoFactory.Instance.GetRepo<IRoleRepo>().GetAll().Count();

            return count;
        }

        /// <summary>
        /// Открывает окно редактирования роли
        /// </summary>
        /// <param name="roleID">ID роли</param>
        /// <returns></returns>
        public RoleSM GetRole(Int32 roleID)
        {
            Role role = RepoFactory.Instance.GetRepo<IRoleRepo>().Get(roleID);

            if (role != null)
            {
                role.PermissionRoles = RepoFactory.Instance.GetRepo<IPermissionRoleRepo>().GetByRole(role.ID).ToList();

                RoleSM model = new RoleSM()
                {
                    ID = role.ID,
                    Name = role.Name,
                    Permissions = RepoFactory.Instance.GetRepo<IPermissionRepo>().GetAll()
                                                                                 .ToList()
                                                                                 .Select(x => new PermissionSM()
                                                                                 {
                                                                                     ID = x.ID,
                                                                                     Value = x.Name,
                                                                                     IsChecked = role.PermissionRoles.Select(t => t.Permission.ID).Contains(x.ID) ? true : false
                                                                                 })
                                                                                 .ToList()
                };

                if (model.Permissions == null)
                {
                    model.Permissions = new List<PermissionSM>();
                }

                return model;
            }
            else
            {
                RoleSM model = new RoleSM()
                {
                    ID = 0,
                    Name = String.Empty,
                    Permissions = new List<PermissionSM>()
                };

                return model;
            }
        }

        /// <summary>
        /// Сохраняет роль
        /// </summary>
        /// <returns></returns>
        public void SetRole(RoleSM model)
        {
            if (model.Permissions == null)
            {
                model.Permissions = new List<PermissionSM>();
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

            model.ID = role.ID;

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
            foreach (PermissionSM item in model.Permissions)
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
        }

        #endregion методы меню Роли

        #region методы меню Пользователи

        /// <summary>
        /// Получить всех пользователей
        /// </summary>
        public List<UserSM> GetAllUser(GridOptions options)
        {
            IQueryable<User> data = RepoFactory.Instance.GetRepo<IUserRepo>().GetAll();

            return options.GetResult(data, x => new UserSM()
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
                .rows;
        }

        /// <summary>
        /// Получить количество всех пользователей в проекте
        /// </summary>
        public Int32 GetCountUser(GridOptions options)
        {
            Int32 count = RepoFactory.Instance.GetRepo<IUserRepo>().GetAll().Count();

            return count;
        }

        /// <summary>
        /// Открывает окно редактирования пользователя
        /// </summary>
        /// <param name="questionID">ID пользователя</param>
        /// <returns></returns>
        public UserSM GetUser(Int32 userID)
        {
            User user = RepoFactory.Instance.GetRepo<IUserRepo>().Get(userID);

            if (user != null)
            {
                UserSM model = new UserSM()
                {
                    ID = user.ID,
                    FirstName = user.FirstName,
                    SecondName = user.SecondName,
                    LastName = user.LastName,
                    Email = user.Email,
                    IsOneQuestion = user.IsOneQuestion,
                    RoleID = user.UserRoles != null &&
                             user.UserRoles.FirstOrDefault() != null ?
                                user.UserRoles.FirstOrDefault().Role.ID : 0,
                    NameRole = user.UserRoles != null &&
                             user.UserRoles.FirstOrDefault() != null ?
                                user.UserRoles.FirstOrDefault().Role.Name : String.Empty
                };

                return model;
            }
            else
            {
                UserSM model = new UserSM();

                return model;
            }
        }

        /// <summary>
        /// Получает данные редактирования пользователя
        /// </summary>
        /// <returns></returns>
        public void SetUser(UserSM model)
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
        }

        #endregion методы меню Пользователи

        #region методы меню Паспорта МО

        /// <summary>
        /// Получить все паспорта МО
        /// </summary>
        public List<PassportMOSM> GetAllPassportMO(GridOptions options)
        {
            IQueryable<MOOrganization> data = RepoFactory.Instance.GetRepo<IMOOrganizationRepo>().GetAll(); ;

            return options.GetResult(data, x => new PassportMOSM()
            {
                ID = x.ID,
                Code = x.Code,
                Name = x.Name
            })
            .rows;
        }

        /// <summary>
        /// Получить количество всех паспортов МО в проекте
        /// </summary>
        public Int32 GetCountPassportMO(GridOptions options)
        {
            Int32 count = RepoFactory.Instance.GetRepo<IMOOrganizationRepo>().GetAll().Count();

            return count;
        }

        #endregion методы меню Паспорта МО

        #region методы меню Паспорта СМО

        /// <summary>
        /// Получить все паспорта СМО
        /// </summary>
        public List<PassportSMOSM> GetAllPassportSMO(GridOptions options)
        {
            IQueryable<SMOOrganization> data = RepoFactory.Instance.GetRepo<ISMOOrganizationRepo>().GetAll(); ;

            return options.GetResult(data, x => new PassportSMOSM()
            {
                ID = x.ID,
                Code = x.Code,
                Name = x.Name
            })
            .rows;
        }

        /// <summary>
        /// Получить количество всех паспортов СМО в проекте
        /// </summary>
        public Int32 GetCountPassportSMO(GridOptions options)
        {
            Int32 count = RepoFactory.Instance.GetRepo<ISMOOrganizationRepo>().GetAll().Count();

            return count;
        }

        #endregion методы меню Паспорта СМО

        #region Вопросы

        /// <summary>
        /// Получить все вопросы данной категории
        /// </summary>
        public List<QuestionSM> GetAllQuestionByCharter(GridOptions options, Int32 charterID)
        {
            IQueryable<Question> data = RepoFactory.Instance.GetRepo<IQuestionRepo>().GetByCharter(charterID);

            return options.GetResult(data, x => new QuestionSM()
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
            .rows;
        }

        /// <summary>
        /// Открывает окно редактирования вопроса
        /// </summary>
        /// <param name="questionID">ID вопроса</param>
        /// <returns></returns>
        public QuestionSM GetQuestion(Int32 questionID, Int32 charterID)
        {
            Question question = RepoFactory.Instance.GetRepo<IQuestionRepo>().Get(questionID);

            if (question != null)
            {
                QuestionSM model = new QuestionSM()
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
                                .Select(x => new AnswerSM()
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
                    model.Answers = new List<AnswerSM>();

                    model.Answers.Add(new AnswerSM()
                    {
                        ID = 0,
                        Score = 0,
                        NumberSequence = 0,
                        TextAnswer = String.Empty,
                        TextAnswerAdditional1 = String.Empty,
                        TextAnswerAdditional2 = String.Empty
                    });
                }

                return model;
            }
            else
            {
                QuestionSM model = new QuestionSM()
                {
                    ID = 0,
                    TextQuestion = String.Empty,
                    NumberSequence = 0,
                    TypeQuestion = 2,
                    CharterID = charterID,
                    NameCharter = String.Empty,
                    Answers = new List<AnswerSM>()
                };

                AnswerSM emptyAnswer = new AnswerSM()
                {
                    ID = 0,
                    Score = 0,
                    NumberSequence = 0,
                    TextAnswer = String.Empty,
                    TextAnswerAdditional1 = String.Empty,
                    TextAnswerAdditional2 = String.Empty
                };

                model.Answers.Add(emptyAnswer);

                return model;
            }
        }

        /// <summary>
        /// Получает данные редактирования вопроса
        /// </summary>
        /// <returns></returns>
        public void SetQuestion(QuestionSM model)
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
        }

        /// <summary>
        /// Удалить вопрос
        /// </summary>
        /// <returns></returns>
        public void DeleteQuestion(Int32 questionID, Int32 charterID)
        {
            Question question = RepoFactory.Instance.GetRepo<IQuestionRepo>().Get(questionID);
            question.IsDelete = true;

            RepoFactory.Instance.GetRepo<IQuestionRepo>().Save(question);
        }

        ///// <summary>
        ///// Получить страницу ответов из справочников
        ///// </summary>
        ///// <param name="type">Тип справочника</param>
        ///// <returns></returns>
        //public QuestionSM GetAnswersFromLkp(Int32 type, Int32 charterID)
        //{
        //    QuestionSM model = new QuestionSM();

        //    List<AnswerSM> data = new List<AnswerSM>();
        //    if (type == 0)
        //    {
        //        data = RepoFactory.Instance.GetRepo<IMOOrganizationRepo>().GetAll()
        //                                                                  .Select(x => new AnswerSM()
        //                                                                  {
        //                                                                      ID = 0,
        //                                                                      TextAnswer = x.Name,
        //                                                                      TextAnswerAdditional1 = x.Code.ToString()
        //                                                                  })
        //                                                                  .ToList();
        //    }
        //    else
        //    {
        //        data = RepoFactory.Instance.GetRepo<ISMOOrganizationRepo>().GetAll()
        //                                                                  .Select(x => new AnswerSM()
        //                                                                  {
        //                                                                      ID = 0,
        //                                                                      TextAnswer = x.Name,
        //                                                                      TextAnswerAdditional1 = x.Code.ToString()
        //                                                                  })
        //                                                                  .ToList();
        //    }

        //    for (Int32 i = 0; i < data.Count; i++)
        //    {
        //        data[i].NumberSequence = i + 1;
        //    }

        //    model.Answers = data;

        //    return model;
        //}

        #endregion
    }
}

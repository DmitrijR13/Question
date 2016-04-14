using Newtonsoft.Json;
using Questionnaire.WebSite.Core;
using Questionnaire.WebSite.Models;
using Sobits.Story.Logic.BLL;
using Sobits.Story.Logic.DataModel;
using Sobits.Story.Logic.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Questionnaire.WebSite.Controllers
{
    /// <summary>
    /// Контроллер страницы статистики
    /// </summary>
    [Authorize(Roles = "Admin/Index")]
    public class StatisticsController : AbstractController
    {
        /// <summary>
        /// Индексная страница
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<CharterModel> charterData = RepoFactory.Instance.GetRepo<ICharterRepo>().GetWithoutDelete()
                                                                                         .Select(x => new CharterModel()
                                                                                         {
                                                                                             ID = x.ID,
                                                                                             Name = x.Name
                                                                                         })
                                                                                         .ToList();

            List<MenuItem> result = new List<MenuItem>();

            MenuItem menuItem = new MenuItem();
            menuItem.ID = "charter";
            menuItem.Name = "Выборка";
            menuItem.OnClick = "";
            menuItem.UrlAction = "#";
            menuItem.MenuItems = new List<MenuItem>();

            foreach (CharterModel item in charterData)
            {
                MenuItem menuDaughterItem = new MenuItem();
                menuDaughterItem.ID = "charter_" + item.ID.ToString();
                menuDaughterItem.Name = item.Name;
                menuDaughterItem.OnClick = "getEnumeration(" + item.ID.ToString() + ")";
                menuDaughterItem.UrlAction = "#";
                menuDaughterItem.MenuItems = null;

                menuItem.MenuItems.Add(menuDaughterItem);
            }

            result.Add(menuItem);

            menuItem = new MenuItem();
            menuItem.ID = "journal";
            menuItem.Name = "Журнал";
            menuItem.OnClick = "";
            menuItem.UrlAction = "#";
            menuItem.MenuItems = new List<MenuItem>();

            foreach (CharterModel item in charterData)
            {
                MenuItem menuDaughterItem = new MenuItem();
                menuDaughterItem.ID = "journal_" + item.ID.ToString();
                menuDaughterItem.Name = item.Name;
                menuDaughterItem.OnClick = "getJournalEmpty(" + item.ID.ToString() + ", 1)";
                menuDaughterItem.UrlAction = "#";
                menuDaughterItem.MenuItems = null;

                menuItem.MenuItems.Add(menuDaughterItem);
            }

            result.Add(menuItem);

            menuItem = new MenuItem();
            menuItem.ID = "journalFull";
            menuItem.Name = "МО/СМО";
            menuItem.OnClick = "";
            menuItem.UrlAction = "#";
            menuItem.MenuItems = new List<MenuItem>();

            foreach (CharterModel item in charterData)
            {
                MenuItem menuDaughterItem = new MenuItem();
                menuDaughterItem.ID = "journalFull_" + item.ID.ToString();
                menuDaughterItem.Name = item.Name;
                menuDaughterItem.OnClick = "getJournalMarkCommon(" + item.ID.ToString() + ")";
                menuDaughterItem.UrlAction = "#";
                menuDaughterItem.MenuItems = null;

                menuItem.MenuItems.Add(menuDaughterItem);
            }

            result.Add(menuItem);

            menuItem = new MenuItem();
            menuItem.ID = "journalMO";
            menuItem.Name = "MO";
            menuItem.OnClick = "";
            menuItem.UrlAction = "#";
            menuItem.MenuItems = new List<MenuItem>();

            foreach (CharterModel item in charterData)
            {
                MenuItem menuDaughterItem = new MenuItem();
                menuDaughterItem.ID = "journalMO_" + item.ID.ToString();
                menuDaughterItem.Name = item.Name;
                menuDaughterItem.OnClick = "getJournalMarkMO(" + item.ID.ToString() + ")";
                menuDaughterItem.UrlAction = "#";
                menuDaughterItem.MenuItems = null;

                menuItem.MenuItems.Add(menuDaughterItem);
            }

            result.Add(menuItem);

            menuItem = new MenuItem();
            menuItem.ID = "journalSMO";
            menuItem.Name = "СMO";
            menuItem.OnClick = "";
            menuItem.UrlAction = "#";
            menuItem.MenuItems = new List<MenuItem>();

            foreach (CharterModel item in charterData)
            {
                MenuItem menuDaughterItem = new MenuItem();
                menuDaughterItem.ID = "journalSMO_" + item.ID.ToString();
                menuDaughterItem.Name = item.Name;
                menuDaughterItem.OnClick = "getJournalMarkSMO(" + item.ID.ToString() + ")";
                menuDaughterItem.UrlAction = "#";
                menuDaughterItem.MenuItems = null;

                menuItem.MenuItems.Add(menuDaughterItem);
            }

            result.Add(menuItem);

            return View(result);
        }

        #region страница для выборки

        /// <summary>
        /// Страница полного перечня вопросов/ответов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Enumeration(Int32 charterID)
        {
            List<QuestionAnswerDM> data = RepoFactory.Instance.GetRepo<IQuestionRepo>().GetUnionQuestionAnswer(charterID).ToList();

            StatisticsModel model = new StatisticsModel()
            {
                CharterID = charterID,
                Questions = data.GroupBy(x => new
                                {
                                    QuestionID = x.QuestionID,
                                    QuestionCharterID = x.QuestionCharterID,
                                    QuestionDateCreate = x.QuestionDateCreate,
                                    QuestionDateUpdate = x.QuestionDateUpdate,
                                    QuestionDescription = x.QuestionDescription,
                                    QuestionNumberSequence = x.QuestionNumberSequence,
                                    QuestionValue = x.QuestionValue
                                })
                                .Select(x => new QuestionModel()
                                {
                                    ID = x.Key.QuestionID,
                                    NumberSequence = x.Key.QuestionNumberSequence,
                                    TextQuestion = x.Key.QuestionValue,
                                    CharterID = x.Key.QuestionCharterID
                                })
                                .OrderBy(x => x.NumberSequence)
                                .ToList()
            };

            foreach (QuestionModel item in model.Questions)
            {
                item.Answers = data.Where(x => x.QuestionID == item.ID)
                                   .Select(x => new AnswerModel()
                                   {
                                       ID = x.AnswerID,
                                       Description = x.AnswerDescription,
                                       TextAnswer = x.AnswerValue,
                                       Score = x.AnswerScore,
                                       NumberSequence = x.AnswerNumberSequence
                                   })
                                   .OrderBy(x => x.NumberSequence)
                                   .ToList();
            }

            return PartialView("_Enumeration", model);
        }

        /// <summary>
        /// Страница полного перечня вопросов/ответов
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Enumeration(StatisticsModel model)
        {
            List<Int32> answerIDs = new List<Int32>();

            foreach (QuestionModel item in model.Questions)
            {
                // Если какой-то выбран, то добавляем его (их)
                if (item.Answers.Select(x => x.IsCheck).Contains(true))
                {
                    answerIDs.AddRange(item.Answers.Where(x => x.IsCheck).Select(x => Convert.ToInt32(x.ID)));
                }
                else // а если не выбран, то выбираем все.
                {
                    //answerIDs.AddRange(item.Answers.Select(x => Convert.ToInt32(x.ID)));
                }
            }

            return JournalEnumeration(model.CharterID, 1, answerIDs, String.Empty, String.Empty, String.Empty);
        }

        /// <summary>
        /// Показывает журнал после перечисления
        /// </summary>
        /// <returns></returns>
        public ActionResult JournalEnumeration(
            Int32 charterID,
            Int32 numberPage,
            List<Int32> answerIDs,
            String fltDate,
            String fltNameOrganization,
            String fltNumberVoting
        )
        {
            List<StatisticDM> result = RepoFactory.Instance.GetRepo<IVotingQuestionRepo>().BuildStatistic(answerIDs, charterID);

            List<VotingDM> dataVoting = result.GroupBy(x => new
            {
                VotingID = x.VotingID,
                DateVote = x.DateVote,
                NameOrganization = x.NameOrganization
            })
                                              .Select(m => new VotingDM()
                                              {
                                                  ID = m.Key.VotingID,
                                                  DateVote = m.Key.DateVote,
                                                  NameOrganization = m.Key.NameOrganization
                                              })
                                              .ToList();

            JournalModel journalModel = new JournalModel();
            journalModel.FltDateVote = fltDate;
            journalModel.FltNameOrganization = fltNameOrganization;
            journalModel.FltVotingID = fltNumberVoting;
            journalModel.JournalVoting = new List<JournalVotingModel>();
            journalModel.Questions = RepoFactory.Instance.GetRepo<IQuestionRepo>()
                                                                           .GetByCharter(charterID)
                                                                           .Select(x => new QuestionModel()
                                                                           {
                                                                               ID = x.ID,
                                                                               NumberSequence = x.NumberSequence,
                                                                               TextQuestion = x.Value
                                                                           })
                                                                           .ToList();

            journalModel.AnswerIDs = answerIDs;

            foreach (VotingDM item in dataVoting)
            {
                JournalVotingModel journalVotingModel = new JournalVotingModel()
                {
                    ID = item.ID,
                    DateVote = item.DateVote != new DateTime() ? item.DateVote.ToShortDateString() : String.Empty,
                    NameOrganization = item.NameOrganization,
                    QuestionAnswers = new List<QuestionAnswerModel>()
                };

                List<StatisticDM> tempVotingQuestion = result.Where(x => x.VotingID == item.ID).ToList();

                foreach (QuestionModel itemQuestion in journalModel.Questions)
                {
                    QuestionAnswerModel qaModel = new QuestionAnswerModel()
                    {
                        NumberQuestion = itemQuestion.NumberSequence.ToString(),
                        TextQuestion = itemQuestion.TextQuestion,
                        NumberAnswer = tempVotingQuestion.Where(t => t.QuestionID == itemQuestion.ID).FirstOrDefault() != null ?
                                            String.Join(", ", tempVotingQuestion.Where(t => t.QuestionID == itemQuestion.ID).Select(x => x.NumberSequenceAnswer.ToString())) :
                                            String.Empty,
                        TextAnswer = tempVotingQuestion.Where(t => t.QuestionID == itemQuestion.ID).FirstOrDefault() != null ?
                                            String.Join(", ", tempVotingQuestion.Where(t => t.QuestionID == itemQuestion.ID).Select(x => x.TextAnswer)) :
                                            String.Empty
                    };

                    journalVotingModel.QuestionAnswers.Add(qaModel);
                }

                journalModel.JournalVoting.Add(journalVotingModel);
            }

            journalModel.JournalVoting = ApplyFilter(journalModel.JournalVoting, fltDate, fltNameOrganization, fltNumberVoting);

            journalModel.CountPages = Convert.ToInt32(journalModel.JournalVoting.Count() / 10) + 1;
            journalModel.NumberPage = numberPage;
            journalModel.CharterID = charterID;

            journalModel.JournalVoting = journalModel.JournalVoting.Skip((journalModel.NumberPage - 1) * 10).Take(10).ToList();

            return PartialView("_Journal", journalModel);
        }

        #endregion страница для выборки

        #region журнал анкет

        /// <summary>
        /// Журнал анкет по данному разделу
        /// </summary>
        /// <returns></returns>
        public ActionResult Journal(
            Int32 charterID,
            Int32 numberPage,
            String answers,
            String fltDate,
            String fltNameOrganization,
            String fltNumberVoting
        )
        {
            // TODO: сделать норм передачу answers!!!
            if (answers != null && answers != String.Empty)
            {
                String[] strAnswerIDs = answers.Split('|');
                List<Int32> answerIDs = new List<Int32>();

                foreach (String strAnswerID in strAnswerIDs)
                {
                    answerIDs.Add(Convert.ToInt32(strAnswerID));
                }

                return JournalEnumeration(charterID, numberPage, answerIDs.ToList(), fltDate, fltNameOrganization, fltNumberVoting);
            }

            List<VotingDM> dataVoting = RepoFactory.Instance.GetRepo<IVotingRepo>().GetActualVoting(charterID);
            List<QuestionAnswerDM> dataQuestionAnswer = RepoFactory.Instance.GetRepo<IVotingQuestionRepo>().GetQuestionAnswerByCharter(charterID).ToList();
            
            JournalModel model = new JournalModel();
            model.FltDateVote = fltDate;
            model.FltNameOrganization = fltNameOrganization;
            model.FltVotingID = fltNumberVoting;
            model.JournalVoting = new List<JournalVotingModel>();
            model.Questions = RepoFactory.Instance.GetRepo<IQuestionRepo>().GetByCharter(charterID)
                                                                           .Select(x => new QuestionModel()
                                                                           {
                                                                               ID = x.ID,
                                                                               NumberSequence = x.NumberSequence,
                                                                               TextQuestion = x.Value
                                                                           })
                                                                           .ToList();

            foreach (VotingDM item in dataVoting)
            {
                bool isNew = true;
                foreach (JournalVotingModel j in model.JournalVoting)
                {
                    if (j.ID == item.ID)
                        isNew = false;
                }
                if (isNew)
                {
                    JournalVotingModel journalVotingModel = new JournalVotingModel()
                    {
                        ID = item.ID,
                        DateVote = item.DateVote != new DateTime() ? item.DateVote.ToShortDateString() : String.Empty,
                        NameOrganization = item.NameOrganization,
                        QuestionAnswers = new List<QuestionAnswerModel>()
                    };

                    List<QuestionAnswerDM> tempVotingQuestion = dataQuestionAnswer.Where(x => x.VotingID == item.ID).ToList();

                    foreach (QuestionModel itemQuestion in model.Questions)
                    {
                        QuestionAnswerModel qaModel = new QuestionAnswerModel()
                        {
                            NumberQuestion = itemQuestion.NumberSequence.ToString(),
                            TextQuestion = itemQuestion.TextQuestion,
                            NumberAnswer = tempVotingQuestion.Where(t => t.QuestionID == itemQuestion.ID).FirstOrDefault() != null ?
                                                String.Join(", ", tempVotingQuestion.Where(t => t.QuestionID == itemQuestion.ID).Select(x => x.AnswerNumberSequence.ToString())) :
                                                String.Empty,
                            TextAnswer = tempVotingQuestion.Where(t => t.QuestionID == itemQuestion.ID).FirstOrDefault() != null ?
                                                String.Join(", ", tempVotingQuestion.Where(t => t.QuestionID == itemQuestion.ID).Select(x => x.AnswerValue)) :
                                                String.Empty,
                        };

                        journalVotingModel.QuestionAnswers.Add(qaModel);
                    }

                    model.JournalVoting.Add(journalVotingModel);
                }
                
            }

            model.JournalVoting = ApplyFilter(model.JournalVoting, fltDate, fltNameOrganization, fltNumberVoting);

            model.CountPages = Convert.ToInt32(model.JournalVoting.Count() / 10) + 1;
            model.NumberPage = numberPage;
            model.CharterID = charterID;
            model.AnswerIDs = new List<Int32>();

            model.JournalVoting = model.JournalVoting.Skip((numberPage - 1) * 10).Take(10).ToList();

            return PartialView("_Journal", model);
        }

        private List<JournalVotingModel> ApplyFilter(List<JournalVotingModel> data, String fltDate, String fltNameOrganization, String fltNumberVoting)
        {
            Int32 fltVotingID = 0;
            if (!String.IsNullOrEmpty(fltNumberVoting) && Int32.TryParse(fltNumberVoting, out fltVotingID))
            {
                data = data.Where(x => x.ID == fltVotingID)
                           .ToList();
            }

            DateTime fltDateTime = new DateTime();
            if (!String.IsNullOrEmpty(fltDate) && DateTime.TryParse(fltDate, out fltDateTime))
            {
                data = data.Where(x => x.DateVote != String.Empty &&
                                       Convert.ToDateTime(x.DateVote) == fltDateTime)
                           .ToList();
            }

            if (!String.IsNullOrEmpty(fltNameOrganization))
            {
                data = data.Where(x => x.NameOrganization != String.Empty &&
                                       x.NameOrganization.Contains(fltNameOrganization))
                           .ToList();
            }

            return data;
        }

        #endregion журнал анкет

        #region журнал с бальными соотношениями

        /// <summary>
        /// Журнал по СМО/МО и бальными соотношениями
        /// </summary>
        /// <returns></returns>
        public ActionResult JournalMarkCommon(Int32 charterID)
        {
            ModelState.Clear();

            List<SMOOrganization> smoOrganizations = RepoFactory.Instance.GetRepo<ISMOOrganizationRepo>().GetAll().ToList();
            List<MOOrganization> moOrganizations = RepoFactory.Instance.GetRepo<IMOOrganizationRepo>().GetAll().ToList();
            List<Question> questions = RepoFactory.Instance.GetRepo<IQuestionRepo>().GetByCharter(charterID).ToList();
            List<VotingDM> votingDMs = RepoFactory.Instance.GetRepo<IVotingRepo>().GetActualVoting(charterID).ToList();
            List<QuestionAnswerDM> votingQuestions = RepoFactory.Instance.GetRepo<IVotingQuestionRepo>().GetQuestionAnswerByCharter(charterID).ToList();

            votingQuestions = votingQuestions.GroupBy(x => new 
                                             { 
                                                 x.QuestionID,
                                                 x.QuestionNumberSequence, 
                                                 x.QuestionValue,
                                                 x.VotingID
                                             })
                                             .Select(x => new QuestionAnswerDM()
                                             {
                                                 QuestionID = x.Key.QuestionID,
                                                 QuestionNumberSequence = x.Key.QuestionNumberSequence,
                                                 QuestionValue = x.Key.QuestionValue,
                                                 AnswerID = x.FirstOrDefault().AnswerID,
                                                 AnswerIDs = String.Join(", ", x.Select(t => t.AnswerID.ToString()).ToArray()),
                                                 AnswerNumberSequence = x.FirstOrDefault().AnswerNumberSequence,
                                                 AnswerValue = String.Join(", ", x.Select(t => t.AnswerValue).ToArray()),
                                                 VotingID = x.Key.VotingID
                                             })
                                             .ToList();
                                             

            JournalMarkModel model = new JournalMarkModel()
            {
                SMOOrganizations = smoOrganizations
                                            .Select(x => new JournalMarkSMOModel()
                                            {
                                                ID = x.ID,
                                                Code = x.Code,
                                                Name = x.Name,
                                                MOs = new List<JournalMarkMOModel>(),
                                                Answers = new List<JournalMarkAnswerModel>()
                                            })
                                            .ToList(),
                Questions = questions
                                .Select(x => new JournalMarkQuestionModel()
                                {
                                    ID = x.ID,
                                    Name = x.Value,
                                    NumberSequence = x.NumberSequence
                                })
                                .ToList()
            };

            foreach (JournalMarkSMOModel itemSmo in model.SMOOrganizations)
            {
                foreach (MOOrganization item in moOrganizations)
                {
                    itemSmo.MOs.Add(new JournalMarkMOModel()
                    {
                        ID = item.ID,
                        Code = item.Code,
                        Name = item.Name,
                        Votings = votingDMs.Where(x => x.MOOrganizationID == item.ID && 
                                                       x.SMOOrganizationID == itemSmo.ID) != null ?
                                  votingDMs.Where(x => x.MOOrganizationID == item.ID &&
                                                       x.SMOOrganizationID == itemSmo.ID)
                                           .Select(x => new JournalMarkVotingModel()
                                           {
                                               ID = x.ID,
                                               Answers = votingQuestions.Where(y => y.VotingID == x.ID)
                                                                        .Select(y => new JournalMarkAnswerModel()
                                                                        {
                                                                            AnswerID = y.AnswerID,
                                                                            AnswerValue = y.AnswerValue,
                                                                            AnswerIDs = y.AnswerIDs,
                                                                            QuestionNumberSequence = y.QuestionNumberSequence,
                                                                            QuestionValue = y.QuestionValue
                                                                        })
                                                                        .OrderBy(y => y.QuestionNumberSequence)
                                                                        .ToList()
                                           })
                                           .Distinct()
                                           .ToList() : new List<JournalMarkVotingModel>(),
                        Answers = new List<JournalMarkAnswerModel>()
                    });
                }
            }
            List<Int32> isNew = new List<int>();
            foreach (var t in model.SMOOrganizations)
            {
                foreach (var q in t.MOs)
                {
                    List<int> del = new List<int>();
                    for (int i = 0 ; i < q.Votings.Count; i++)
                    {
                        if (isNew.Contains(q.Votings[i].ID))
                        {
                            q.Votings.RemoveAt(i);
                            i--;
                        }
                        else
                        {
                            isNew.Add(q.Votings[i].ID);
                        }
                    }           
                }
            }
            return PartialView("_JournalMarkCommon", model);
        }

        /// <summary>
        /// Журнал по МО и бальными соотношениями
        /// </summary>
        /// <returns></returns>
        public ActionResult JournalMarkMO(Int32 charterID)
        {
            ModelState.Clear();

            List<MOOrganization> moOrganizations = RepoFactory.Instance.GetRepo<IMOOrganizationRepo>().GetAll().ToList();
            List<Question> questions = RepoFactory.Instance.GetRepo<IQuestionRepo>().GetByCharter(charterID).ToList();
            List<VotingDM> votingDMs = RepoFactory.Instance.GetRepo<IVotingRepo>().GetActualVoting(charterID).ToList();
            List<QuestionAnswerDM> votingQuestions = RepoFactory.Instance.GetRepo<IVotingQuestionRepo>().GetQuestionAnswerByCharter(charterID).ToList();

            votingQuestions = votingQuestions.GroupBy(x => new
                                             {
                                                x.QuestionID,
                                                x.QuestionNumberSequence,
                                                x.QuestionValue,
                                                x.VotingID
                                             })
                                             .Select(x => new QuestionAnswerDM()
                                             {
                                                 QuestionID = x.Key.QuestionID,
                                                 QuestionNumberSequence = x.Key.QuestionNumberSequence,
                                                 QuestionValue = x.Key.QuestionValue,
                                                 AnswerID = x.FirstOrDefault().AnswerID,
                                                 AnswerIDs = String.Join(", ", x.Select(t => t.AnswerID.ToString()).ToArray()),
                                                 AnswerNumberSequence = x.FirstOrDefault().AnswerNumberSequence,
                                                 AnswerValue = String.Join(", ", x.Select(t => t.AnswerValue).ToArray()),
                                                 VotingID = x.Key.VotingID
                                             })
                                             .ToList();


            JournalMarkModel model = new JournalMarkModel()
            {
                MOOrganizations = moOrganizations
                                            .Select(x => new JournalMarkMOModel()
                                            {
                                                ID = x.ID,
                                                Code = x.Code,
                                                Name = x.Name,
                                                Votings = new List<JournalMarkVotingModel>(),
                                                Answers = new List<JournalMarkAnswerModel>()
                                            })
                                            .ToList(),
                Questions = questions
                                .Select(x => new JournalMarkQuestionModel()
                                {
                                    ID = x.ID,
                                    Name = x.Value,
                                    NumberSequence = x.NumberSequence
                                })
                                .ToList()
            };

            foreach (JournalMarkMOModel itemMo in model.MOOrganizations)
            {
                itemMo.Votings = 
                                votingDMs.Where(x => x.MOOrganizationID == itemMo.ID) != null ?
                                votingDMs.Where(x => x.MOOrganizationID == itemMo.ID)
                                        .Select(x => new JournalMarkVotingModel()
                                        {
                                            ID = x.ID,
                                            Answers = votingQuestions.Where(y => y.VotingID == x.ID)
                                                                    .Select(y => new JournalMarkAnswerModel()
                                                                    {
                                                                        AnswerID = y.AnswerID,
                                                                        AnswerValue = y.AnswerValue,
                                                                        AnswerIDs = y.AnswerIDs,
                                                                        QuestionNumberSequence = y.QuestionNumberSequence,
                                                                        QuestionValue = y.QuestionValue
                                                                    })
                                                                    .OrderBy(y => y.QuestionNumberSequence)
                                                                    .ToList()
                                        })
                                        .ToList() : new List<JournalMarkVotingModel>();
                itemMo.Answers = new List<JournalMarkAnswerModel>();
            }

            List<Int32> isNew = new List<int>();
            foreach (var t in model.MOOrganizations)
            {
                List<int> del = new List<int>();
                for (int i = 0; i < t.Votings.Count; i++)
                {
                    if (isNew.Contains(t.Votings[i].ID))
                    {
                        t.Votings.RemoveAt(i);
                        i--;
                    }
                    else
                    {
                        isNew.Add(t.Votings[i].ID);
                    }
                }
            }

            return PartialView("_JournalMarkMO", model);
        }

        /// <summary>
        /// Журнал по СМО и бальными соотношениями
        /// </summary>
        /// <returns></returns>
        public ActionResult JournalMarkSMO(Int32 charterID)
        {
            ModelState.Clear();

            List<SMOOrganization> smoOrganizations = RepoFactory.Instance.GetRepo<ISMOOrganizationRepo>().GetAll().ToList();
            List<Question> questions = RepoFactory.Instance.GetRepo<IQuestionRepo>().GetByCharter(charterID).ToList();
            List<VotingDM> votingDMs = RepoFactory.Instance.GetRepo<IVotingRepo>().GetActualVoting(charterID).ToList();
            List<QuestionAnswerDM> votingQuestions = RepoFactory.Instance.GetRepo<IVotingQuestionRepo>().GetQuestionAnswerByCharter(charterID).ToList();

            votingQuestions = votingQuestions.GroupBy(x => new
                                             {
                                                 x.QuestionID,
                                                 x.QuestionNumberSequence,
                                                 x.QuestionValue,
                                                 x.VotingID
                                             })
                                             .Select(x => new QuestionAnswerDM()
                                             {
                                                 QuestionID = x.Key.QuestionID,
                                                 QuestionNumberSequence = x.Key.QuestionNumberSequence,
                                                 QuestionValue = x.Key.QuestionValue,
                                                 AnswerID = x.FirstOrDefault().AnswerID,
                                                 AnswerIDs = String.Join(", ", x.Select(t => t.AnswerID.ToString()).ToArray()),
                                                 AnswerNumberSequence = x.FirstOrDefault().AnswerNumberSequence,
                                                 AnswerValue = String.Join(", ", x.Select(t => t.AnswerValue).ToArray()),
                                                 VotingID = x.Key.VotingID
                                             })
                                             .ToList();


            JournalMarkModel model = new JournalMarkModel()
            {
                SMOOrganizations = smoOrganizations
                                            .Select(x => new JournalMarkSMOModel()
                                            {
                                                ID = x.ID,
                                                Code = x.Code,
                                                Name = x.Name,
                                                Votings = new List<JournalMarkVotingModel>(),
                                                Answers = new List<JournalMarkAnswerModel>()
                                            })
                                            .ToList(),
                Questions = questions
                                .Select(x => new JournalMarkQuestionModel()
                                {
                                    ID = x.ID,
                                    Name = x.Value,
                                    NumberSequence = x.NumberSequence
                                })
                                .ToList()
            };

            foreach (JournalMarkSMOModel itemSmo in model.SMOOrganizations)
            {
                itemSmo.Votings =
                                votingDMs.Where(x => x.SMOOrganizationID == itemSmo.ID) != null ?
                                votingDMs.Where(x => x.SMOOrganizationID == itemSmo.ID)
                                        .Select(x => new JournalMarkVotingModel()
                                        {
                                            ID = x.ID,
                                            Answers = votingQuestions.Where(y => y.VotingID == x.ID)
                                                                    .Select(y => new JournalMarkAnswerModel()
                                                                    {
                                                                        AnswerID = y.AnswerID,
                                                                        AnswerValue = y.AnswerValue,
                                                                        AnswerIDs = y.AnswerIDs,
                                                                        QuestionNumberSequence = y.QuestionNumberSequence,
                                                                        QuestionValue = y.QuestionValue
                                                                    })
                                                                    .OrderBy(y => y.QuestionNumberSequence)
                                                                    .ToList()
                                        })
                                        .ToList() : new List<JournalMarkVotingModel>();
                itemSmo.Answers = new List<JournalMarkAnswerModel>();
            }

            List<Int32> isNew = new List<int>();
            foreach (var t in model.SMOOrganizations)
            {
                List<int> del = new List<int>();
                for (int i = 0; i < t.Votings.Count; i++)
                {
                    if (isNew.Contains(t.Votings[i].ID))
                    {
                        t.Votings.RemoveAt(i);
                        i--;
                    }
                    else
                    {
                        isNew.Add(t.Votings[i].ID);
                    }
                }
            }

            return PartialView("_JournalMarkSMO", model);
        }

        #endregion журнал с бальными соотношениями
    }
}

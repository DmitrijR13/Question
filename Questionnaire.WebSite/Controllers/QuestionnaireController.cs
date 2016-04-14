using Calabonga.Mvc.Extensions;
using Questionnaire.WebSite.Core;
using Questionnaire.WebSite.Models;
using Sobits.Story.Logic.BLL;
using Sobits.Story.Logic.Enumerable;
using Sobits.Story.Logic.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Questionnaire.WebSite.Controllers
{
    /// <summary>
    /// Контроллер страницы анкетирование
    /// </summary>
    public class QuestionnaireController : AbstractController
    {
        /// <summary>
        /// Выводит индексную страницу
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(Int32 charterID, Int32 votingID, Boolean isPlugin)
        {
            QuestionnaireModel model = new QuestionnaireModel()
            {
                CharterID = charterID,
                VotingID = votingID,
                IsPlugin = isPlugin
            };

            return View(model);
        }

        /// <summary>
        /// Следующий вопрос
        /// </summary>
        /// <param name="questionID">ID вопроса который необходимо вывести</param>
        /// <param name="charterID">ID категории</param>
        /// <param name="votingID">ID голосующего</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult NextQuestion(Int32 questionID, Int32 charterID, Int32 votingID, Int32? isFirstQuery)
        {
            ModelState.Clear();

            Boolean isFirst = false;
            Boolean isLast = false;

            Charter charter = RepoFactory.Instance.GetRepo<ICharterRepo>().Get(charterID);

            if (charter == null) { throw new Exception("Такого раздела не существует!"); }
            
            Voting voting = RepoFactory.Instance.GetRepo<IVotingRepo>().Get(votingID);

            if (voting == null) { throw new Exception("Такого голосующего не существует"); }
            
            Question data;
            List<Question> datas = RepoFactory.Instance.GetRepo<IQuestionRepo>().GetByCharter(charterID).ToList();

            Int32 countTempVoting = RepoFactory.Instance.GetRepo<ITempVotingQuestionRepo>().GetByVoting(votingID).Count();
            Int32 number = 0;

            // Если вопрос первый
            if (questionID == 0)
            {
                data = datas.FirstOrDefault();
                isFirst = true;
                number = 1;
            }
            else
            {
                data = RepoFactory.Instance.GetRepo<IQuestionRepo>().Get(questionID);

                // Проверка на принадлежность вопроса к данному разделу
                if (data.Charter.ID != charter.ID) { throw new Exception("Такого вопроса не существует!"); }
            }

            if (data == null) { throw new Exception("Такого вопроса не существует!"); }

            if (data.ID == datas.LastOrDefault().ID)
            {
                isLast = true;
            }

            List<TempVotingQuestion> tempVoting = RepoFactory.Instance.GetRepo<ITempVotingQuestionRepo>().GetByVoting(votingID).ToList();
            TempVotingQuestion tempVotingQuestion = tempVoting.Where(x => x.Question.ID == questionID)
                                                              .FirstOrDefault();

            List<Answer> checkAnswers = tempVoting.Where(x => x.Question.ID == questionID)
                                                  .Select(x => x.Answer)
                                                  .ToList();

            VotingQuestionModel model = new VotingQuestionModel();
            model.QuestionID = data.ID;
            model.TypeQuestion = (Int32)data.TypeQuestion;
            model.VotingID = voting.ID;
            model.TextQuestion = data.Value;
            model.IsFirst = isFirst;
            model.IsLast = isLast;
            model.IDCheck = tempVotingQuestion != null ? tempVotingQuestion.Answer.ID : 
                                                         data.TypeQuestion == TypeQuestion.Text ? 0 : 0;
            model.IDChecks = new List<Int32>() { model.IDCheck };
            model.TextAnswer = tempVotingQuestion != null ? tempVotingQuestion.Answer.Value :
                data.TypeQuestion == TypeQuestion.DropDownList || 
                data.TypeQuestion == TypeQuestion.RadioButton ||
                data.TypeQuestion == TypeQuestion.FromTo ||
                data.TypeQuestion == TypeQuestion.Multiple ? "000" : String.Empty;
            model.DateStart = tempVotingQuestion != null && tempVotingQuestion.Answer.DateStart.HasValue ? tempVotingQuestion.Answer.DateStart.Value.ToShortDateString() :
                data.TypeQuestion == TypeQuestion.DropDownList || 
                data.TypeQuestion == TypeQuestion.RadioButton ||
                data.TypeQuestion == TypeQuestion.RadioButtonText ||
                data.TypeQuestion == TypeQuestion.Text ||
                data.TypeQuestion == TypeQuestion.Multiple ? (new DateTime()).ToString("MM yy") : String.Empty;
            model.DateEnd = tempVotingQuestion != null && tempVotingQuestion.Answer.DateEnd.HasValue ? tempVotingQuestion.Answer.DateEnd.Value.ToShortDateString() :
                data.TypeQuestion == TypeQuestion.DropDownList ||
                data.TypeQuestion == TypeQuestion.RadioButton ||
                data.TypeQuestion == TypeQuestion.RadioButtonText ||
                data.TypeQuestion == TypeQuestion.Text ||
                data.TypeQuestion == TypeQuestion.Multiple ? (new DateTime()).ToString("MM yy") : String.Empty;
            model.Number = tempVotingQuestion == null ? (countTempVoting + 1).ToString() : (tempVoting.FindIndex(x => x.ID == tempVotingQuestion.ID) + 1).ToString();
            model.CountQuestions = datas.Count();
            model.IndexQuestion = (isFirstQuery.HasValue && isFirstQuery.Value == 1) ? 0 : datas.FindIndex(x => x.ID == data.ID);
            model.Answers = data.Answers
                                .Where(x => !x.IsDelete)
                                .OrderBy(t => t.NumberSequence)
                                .Select(t => new VotingAnswerModel()
                                {
                                    AnswerID = t.ID,
                                    IsCheck = checkAnswers.Select(m => m.ID).Contains(t.ID),
                                    TextAnswer = t.Value
                                })
                                .ToList();

            return PartialView("_Question", model);
        }

        /// <summary>
        /// Следующий вопрос
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult NextQuestion(VotingQuestionModel model)
        {
            if (model.TypeQuestion == (Int32)TypeQuestion.RadioButton && model.IDCheck == 0)
            {
                ModelState.AddModelError("IDCheck", "Not 0");
            }

            if (model.TypeQuestion == (Int32)TypeQuestion.RadioButtonText)
            {
                if (model.IDCheck == 0 && String.IsNullOrEmpty(model.TextAnswer))
                {
                    ModelState.AddModelError("IDCheck", "Not 0");
                }
                else
                {
                    ModelState.Clear();
                }
            }

            if (ModelState.IsValid)
            {
                Voting voting = RepoFactory.Instance.GetRepo<IVotingRepo>().Get(model.VotingID);

                if (voting == null) { throw new Exception("Такого голосующего нет!"); }

                Question question = RepoFactory.Instance.GetRepo<IQuestionRepo>().Get(model.QuestionID);

                if (question == null) { throw new Exception("Такого вопроса не существует!"); }

                Answer answer = null;

                if (question.TypeQuestion == TypeQuestion.RadioButton)
                {
                    answer = RepoFactory.Instance.GetRepo<IAnswerRepo>().Get(model.IDCheck);
                    
                    if (answer == null) { throw new Exception("Такого ответа не существует!"); }
                }
                else if (question.TypeQuestion == TypeQuestion.RadioButtonText)
                {
                    answer = RepoFactory.Instance.GetRepo<IAnswerRepo>().Get(model.IDCheck);

                    if (answer == null) 
                    {
                        answer = new Answer()
                        {
                            Description = String.Empty,
                            Value = model.TextAnswer,
                            DateStart = null,
                            DateEnd = null,
                            Score = 0,
                            NumberSequence = 0,
                            Question = null,
                            NextQuestion = null,
                            IsDelete = false,
                            DateCreate = DateTime.Now,
                            DateUpdate = DateTime.Now
                        };

                        answer = RepoFactory.Instance.GetRepo<IAnswerRepo>().Save(answer);
                    }
                }
                else if (question.TypeQuestion == TypeQuestion.DropDownList)
                {
                    answer = RepoFactory.Instance.GetRepo<IAnswerRepo>().Get(model.IDChecks.FirstOrDefault());

                    if (answer == null) { throw new Exception("Такого ответа не существует!"); }
                }
                else if (question.TypeQuestion == TypeQuestion.Text)
                {
                    answer = new Answer()
                    {
                        Description = String.Empty,
                        Value = model.TextAnswer,
                        DateStart = null,
                        DateEnd = null,
                        Score = 0,
                        NumberSequence = 0,
                        Question = null,
                        NextQuestion = null,
                        IsDelete = false,
                        DateCreate = DateTime.Now,
                        DateUpdate = DateTime.Now
                    };

                    answer = RepoFactory.Instance.GetRepo<IAnswerRepo>().Save(answer);
                }
                else if (question.TypeQuestion == TypeQuestion.FromTo)
                {
                    answer = new Answer()
                    {
                        Description = String.Empty,
                        Value = "Not empty",
                        DateStart = Convert.ToDateTime(model.DateStart),
                        DateEnd = Convert.ToDateTime(model.DateEnd),
                        Score = 0,
                        NumberSequence = 0,
                        Question = null,
                        NextQuestion = null,
                        IsDelete = false,
                        DateCreate = DateTime.Now,
                        DateUpdate = DateTime.Now
                    };

                    answer = RepoFactory.Instance.GetRepo<IAnswerRepo>().Save(answer);
                }
                else if (question.TypeQuestion == TypeQuestion.Multiple)
                {
                    List<VotingAnswerModel> newAnswers = model.Answers.Where(x => x.IsCheck).ToList(); 
                    List<TempVotingQuestion> oldAnswers = RepoFactory.Instance.GetRepo<ITempVotingQuestionRepo>().GetByQuestionVoting(model.QuestionID, model.VotingID).ToList();

                    List<TempVotingQuestion> deleteAnswers = oldAnswers.Where(x => !newAnswers.Select(t => t.AnswerID).Contains(x.Answer.ID)).ToList();
                    List<TempVotingQuestion> addAnswers = newAnswers.Where(x => !oldAnswers.Select(t => t.Answer.ID).Contains(x.AnswerID))
                                                                    .Select(x => new TempVotingQuestion()
                                                                    {
                                                                        ID = 0,
                                                                        Answer = RepoFactory.Instance.GetRepo<IAnswerRepo>().Get(x.AnswerID),
                                                                        Question = question,
                                                                        Voting = voting
                                                                    })
                                                                    .ToList();

                    RepoFactory.Instance.GetRepo<ITempVotingQuestionRepo>().DeleteList(deleteAnswers);
                    RepoFactory.Instance.GetRepo<ITempVotingQuestionRepo>().SaveList(addAnswers);
                }

                if (model.TypeQuestion != (Int32)TypeQuestion.Multiple)
                {
                    VotingQuestion votingQuestion = RepoFactory.Instance.GetRepo<IVotingQuestionRepo>().GetByQuestionVoting(model.QuestionID, model.VotingID).FirstOrDefault();

                    if (votingQuestion != null) { throw new Exception("Такой ответ уже был у этого же опрашиваемого!"); }

                    TempVotingQuestion tempVotingQuestion = RepoFactory.Instance.GetRepo<ITempVotingQuestionRepo>().GetByQuestionVoting(model.QuestionID, model.VotingID).FirstOrDefault();

                    if (tempVotingQuestion == null)
                    {
                        tempVotingQuestion = new TempVotingQuestion();
                    }
                    else
                    {
                        // Если последовательность ответов-вопросов нарушенна, то удаляем прежнюю ветку ответов 
                        if (tempVotingQuestion.Answer.NextQuestion != answer.NextQuestion)
                        {
                            List<TempVotingQuestion> tmpVotingQuestion = RepoFactory.Instance.GetRepo<ITempVotingQuestionRepo>().GetByVoting(model.VotingID).ToList();
                            Int32 curIndex = tmpVotingQuestion.FindIndex(x => x.ID == tempVotingQuestion.ID);

                            for (Int32 i = curIndex + 1; i < tmpVotingQuestion.Count; i++)
                            {
                                RepoFactory.Instance.GetRepo<ITempVotingQuestionRepo>().Delete(tmpVotingQuestion[i]);
                            }
                        }
                    }

                    tempVotingQuestion.Voting = voting;
                    tempVotingQuestion.Question = question;
                    tempVotingQuestion.Answer = answer;

                    tempVotingQuestion = RepoFactory.Instance.GetRepo<ITempVotingQuestionRepo>().Save(tempVotingQuestion);
                }

                // Следующий вопрос (получаем из ответа или из последовательности)
                Int32 questionID = 0;
                if (answer != null && answer.NextQuestion != null)
                {
                    questionID = answer.NextQuestion.ID;
                }
                else
                {
                    List<Question> questions = RepoFactory.Instance.GetRepo<IQuestionRepo>().GetByCharter(question.Charter.ID).ToList();
                    
                    Int32 curIndex = questions.FindIndex(x => x.ID == question.ID);

                    // если элемент последний
                    if (curIndex + 1 == questions.Count)
                    {
                        if (Session.User != null)
                        {
                            SaveQuestionnaire(model.VotingID);
                            return RedirectToAction("Ready", "Questionnaire", new { charterID = question.Charter.ID, votingID = voting.ID });
                        }
                        else
                        {
                            //return RedirectToAction("VotingInfoCheck", "Questionnaire", new { charterID = question.Charter.ID, votingID = voting.ID });
                            SaveQuestionnaire(model.VotingID);
                            return RedirectToAction("Ready", "Questionnaire", new { charterID = question.Charter.ID, votingID = voting.ID });
                        }
                    }
                    else
                    {
                        // берем следующий вопрос из анкеты
                        questionID = questions[curIndex + 1].ID;
                    }
                }

                // Выводим следующий вопрос
                return RedirectToAction("NextQuestion", "Questionnaire", new { questionID = questionID, charterID = question.Charter.ID, votingID = voting.ID });
            }
            else
            {
                return PartialView("_Question", model);
            }
        }

        /// <summary>
        /// Предыдущий вопрос
        /// </summary>
        /// <param name="questionID">ID текущего вопроса</param>
        /// <param name="charterID">ID категории</param>
        /// <param name="votingID">ID голосующего</param>
        /// <returns></returns>
        public ActionResult PrevQuestion(Int32 questionID, Int32 charterID, Int32 votingID)
        {
            ModelState.Clear();

            Boolean isFirst = false;
            
            List<Question> questions = RepoFactory.Instance.GetRepo<IQuestionRepo>().GetByCharter(charterID).ToList();

            //Charter charter = RepoFactory.Instance.GetRepo<ICharterRepo>().Get(charterID);

            //if (charter == null) { throw new Exception("Такого раздела не существует!"); }
            
            Voting voting = RepoFactory.Instance.GetRepo<IVotingRepo>().Get(votingID);

            if (voting == null) { throw new Exception("Такого голосующего не существует!"); }

            //Question question = RepoFactory.Instance.GetRepo<IQuestionRepo>().Get(questionID);
            Question question = questions.Find(x => x.ID == questionID);

            if (question == null) { throw new Exception("Такого вопроса не существует!"); }

            // Все предыдущие анкеты голосующего
            List<TempVotingQuestion> tmpVotingQuestion = RepoFactory.Instance.GetRepo<ITempVotingQuestionRepo>().GetByVoting(votingID).ToList();

            //// Получаем предыдущий вопрос относительно текущего
            //List<Question> questions = RepoFactory.Instance.GetRepo<IQuestionRepo>().GetByCharter(charterID).ToList();
            
            //Int32 curId = questions.FindIndex(x => x.ID == question.ID);
            Int32 curId = tmpVotingQuestion.FindIndex(x => x.Question.ID == question.ID);
            Int32 prevQuestionID = 0;

            // Если элемент еще не сохранен берем последний в temp
            if (curId == -1)
            {
                prevQuestionID = tmpVotingQuestion.LastOrDefault().Question.ID;

                if (tmpVotingQuestion.Count - 1 == 0)
                {
                    isFirst = true;
                }
            } // Если элемент первый...
            else if (curId == 0)
            {
                // ... то выдаем исключение
                throw new Exception("Более раннего вопроса в последовательности нет!");
            }
            else
            {
                // берем предыдущий вопрос из анкеты
                prevQuestionID = tmpVotingQuestion[curId - 1].Question.ID;

                if (curId - 1 == 0)
                {
                    isFirst = true;
                }
            }

            Question prevQuestion = RepoFactory.Instance.GetRepo<IQuestionRepo>().Get(prevQuestionID);

            List<TempVotingQuestion> lTempVotingQuestion = RepoFactory.Instance.GetRepo<ITempVotingQuestionRepo>().GetByQuestionVoting(prevQuestionID, votingID).ToList();

            if (lTempVotingQuestion == null || lTempVotingQuestion.Count == 0)
            {
                throw new Exception("На такой вопрос ответа не было!");
            }

            TempVotingQuestion tempVotingQuestion = lTempVotingQuestion.FirstOrDefault();

            List<Answer> checkAnswers = lTempVotingQuestion 
                                                          .Select(x => x.Answer)
                                                          .ToList();

            VotingQuestionModel model = new VotingQuestionModel();
            model.QuestionID = prevQuestion.ID;
            model.VotingID = votingID;
            model.TextQuestion = prevQuestion.Value;
            model.IsFirst = isFirst;
            model.IsLast = false;
            model.IDCheck = tempVotingQuestion.Answer.ID;
            model.IDChecks = new List<Int32>() { tempVotingQuestion.Answer.ID };
            model.DateStart = tempVotingQuestion.Answer.DateStart.HasValue ? tempVotingQuestion.Answer.DateStart.Value.ToString("y") : (new DateTime()).ToString("y");
            model.DateEnd = tempVotingQuestion.Answer.DateEnd.HasValue ? tempVotingQuestion.Answer.DateEnd.Value.ToString("y") : (new DateTime()).ToString("y");
            model.TypeQuestion = (Int32)prevQuestion.TypeQuestion;
            model.Number = (tmpVotingQuestion.FindIndex(x => x.ID == tempVotingQuestion.ID) + 1).ToString();
            model.CountQuestions = questions.Count();
            model.IndexQuestion = questions.FindIndex(x => x.ID == prevQuestion.ID);
            model.Answers = prevQuestion.Answers
                                        .Where(x => !x.IsDelete)
                                        .OrderBy(t => t.NumberSequence)
                                        .Select(t => new VotingAnswerModel()
                                        {
                                            AnswerID = t.ID,
                                            IsCheck = checkAnswers.Select(m => m.ID).Contains(t.ID),
                                            TextAnswer = t.Value
                                        })
                                        .ToList();
            model.TextAnswer = prevQuestion.TypeQuestion == TypeQuestion.RadioButtonText &&
                model.Answers.Select(x => x.AnswerID).Contains(tempVotingQuestion.Answer.ID) ? String.Empty : tempVotingQuestion.Answer.Value;

            return PartialView("_Question", model);
        }

        /// <summary>
        /// Выводит предложение о вводе каптчи
        /// </summary>
        [HttpGet]
        public ActionResult VotingInfoCheck(Int32 votingID, Int32 charterID)
        {
            VotingInfoCheckModel model = new VotingInfoCheckModel()
            {
                VotingID = votingID,
                CharterID = charterID
            };

            return PartialView("_VotingInfoCheck", model);
        }

        /// <summary>
        /// Проверка на каптчу
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Captcher()]
        public ActionResult VotingInfoCheck(VotingInfoCheckModel model)
        {
            if (ModelState.IsValid)
            {
                SaveQuestionnaire(model.VotingID);
                return RedirectToAction("Ready", "Questionnaire", new { charterID = model.CharterID, votingID = model.VotingID });
            }

            if (!ModelState.IsValidField("captcha"))
            {
                ModelState.Remove("captcha");
                ModelState.AddModelError("captcha", "Ошибочный ввод");
            }

            return PartialView("_VotingInfoCheck", model);
        }

        /// <summary>
        /// Выводит на экран "каптчу"
        /// </summary>
        /// <returns></returns>
        public ActionResult Captcha()
        {
            return new CaptchaResult();
        }

        /// <summary>
        /// Выводит сообщение об окончании опроса
        /// </summary>
        /// <returns></returns>
        public ActionResult Ready(Int32 charterID, Int32 votingID)
        {
            Charter endCharter = RepoFactory.Instance.GetRepo<ICharterRepo>().Get(charterID);

            ReadyModel model = new ReadyModel()
            {
                ThemeQuestionnaire = endCharter.Name,
                NumberVoting = votingID.ToString(),
                Charters = new List<CharterModel>()
            };

            List<Charter> charters = RepoFactory.Instance.GetRepo<ICharterRepo>().GetWithoutDelete().ToList();

            foreach (Charter item in charters)
            {
                CharterModel charterModel = new CharterModel();
                charterModel.ID = item.ID;
                charterModel.Name = item.Name;
                charterModel.ImageID = item.Image != null ? item.Image.ID : 0;
                charterModel.ImageName = item.Image != null ? item.Image.FileName : String.Empty;

                model.Charters.Add(charterModel);
            }

            return PartialView("_Ready", model);
        }

        /// <summary>
        /// Организация поиска в ListBoxFor варианта ответа
        /// </summary>
        /// <param name="text">Текст для поиска</param>
        /// <returns></returns>
        public JsonResult FindDataList(Int32 questionID, String text)
        {
            List<AnswerModel> answers = RepoFactory.Instance.GetRepo<IAnswerRepo>().GetByQuestionAndText(questionID, text)
                                                                                   .Select(x => new AnswerModel()
                                                                                   {
                                                                                       ID = x.ID,
                                                                                       TextAnswer = x.Value
                                                                                   })
                                                                                   .ToList();

            return Json(answers, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Сохранить прохождение анкеты
        /// </summary>
        private void SaveQuestionnaire(Int32 votingID)
        {
            // обрабатываем конец анкетирования
            // сохраняем все данные в действующую таблицу...
            RepoFactory.Instance.GetRepo<IVotingQuestionRepo>().CopyFromTemp(votingID);

            // ... и уничтожаем временные данные
            RepoFactory.Instance.GetRepo<ITempVotingQuestionRepo>().DeleteByVoting(votingID);
        }
    }
}

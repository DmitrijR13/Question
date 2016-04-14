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
    /// Прохождение анкеты целиком
    /// </summary>
    public class QuestionnaireFullController : AbstractController
    {
        /// <summary>
        /// Индексная страница
        /// </summary>
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
        /// Показать все вопросы
        /// </summary>
        /// <param name="charterID">ID категории</param>
        /// <param name="votingID">ID голосующего</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Question(Int32 charterID, Int32 votingID)
        {
            ModelState.Clear();

            Charter charter = RepoFactory.Instance.GetRepo<ICharterRepo>().Get(charterID);

            if (charter == null) { throw new Exception("Такого раздела не существует!"); }

            Voting voting = RepoFactory.Instance.GetRepo<IVotingRepo>().Get(votingID);

            if (voting == null) { throw new Exception("Такого голосующего не существует!"); }

            // Модель на вывод
            QuestionFullModel model = new QuestionFullModel()
            {
                CharterID = charter.ID,
                CharterName = charter.Name,
                VotingID = voting.ID,
                Questions = new List<VotingQuestionModel>()
            };

            // Получаем все вопросы/ответы для этой анкеты
            List<Question> questions = RepoFactory.Instance.GetRepo<IQuestionRepo>().GetByCharter(charterID).ToList();

            for (Int32 i = 0; i < questions.Count; i++)
            {
                Question item = questions[i];

                VotingQuestionModel tmp = new VotingQuestionModel();
                tmp.QuestionID = item.ID;
                tmp.TypeQuestion = (Int32)item.TypeQuestion;
                tmp.VotingID = voting.ID;
                tmp.TextQuestion = item.Value;
                tmp.IDCheck = 0;
                tmp.IDChecks = new List<Int32>();
                tmp.IDChecks.Add(0);
                tmp.TextAnswer = 
                    item.TypeQuestion == TypeQuestion.DropDownList ||
                    item.TypeQuestion == TypeQuestion.RadioButton ||
                    item.TypeQuestion == TypeQuestion.FromTo ||
                    item.TypeQuestion == TypeQuestion.Multiple ? "000" : String.Empty;
                tmp.DateStart = 
                    item.TypeQuestion == TypeQuestion.DropDownList ||
                    item.TypeQuestion == TypeQuestion.RadioButton ||
                    item.TypeQuestion == TypeQuestion.RadioButtonText ||
                    item.TypeQuestion == TypeQuestion.Text ||
                    item.TypeQuestion == TypeQuestion.Multiple ? (new DateTime()).ToString("MM yy") : String.Empty;
                tmp.DateEnd = 
                    item.TypeQuestion == TypeQuestion.DropDownList ||
                    item.TypeQuestion == TypeQuestion.RadioButton ||
                    item.TypeQuestion == TypeQuestion.RadioButtonText ||
                    item.TypeQuestion == TypeQuestion.Text ||
                    item.TypeQuestion == TypeQuestion.Multiple ? (new DateTime()).ToString("MM yy") : String.Empty;
                tmp.Number = (i + 1).ToString();
                tmp.Answers = item.Answers
                                    .Where(x => !x.IsDelete)
                                    .OrderBy(t => t.NumberSequence)
                                    .Select(t => new VotingAnswerModel()
                                    {
                                        AnswerID = t.ID,
                                        IsCheck = false,
                                        TextAnswer = t.Value
                                    })
                                    .ToList();

                model.Questions.Add(tmp);
            }

            return PartialView("_Question", model);
        }

        /// <summary>
        /// Сохраняет ответы на анкету
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Question(QuestionFullModel model)
        {
            foreach (VotingQuestionModel item in model.Questions)
            {
                if (item.TypeQuestion == (Int32)TypeQuestion.RadioButton && item.IDCheck == 0)
                {
                    ModelState.AddModelError("IDCheck", "Not 0");
                }

                if (item.TypeQuestion == (Int32)TypeQuestion.RadioButtonText)
                {
                    if (item.IDCheck == 0 && String.IsNullOrEmpty(item.TextAnswer))
                    {
                        ModelState.AddModelError("IDCheck", "Not 0");
                    }
                    else
                    {
                        ModelState.Clear();
                    }
                }

                if (item.IDChecks == null) 
                {
                    item.IDChecks = new List<Int32>();
                    item.IDChecks.Add(0);
                }
            }

            if (ModelState.IsValid)
            {
                Voting voting = RepoFactory.Instance.GetRepo<IVotingRepo>().Get(model.VotingID);

                if (voting == null) { throw new Exception("Такого голосующего нет!"); }

                // Перебираем все вопросы
                foreach (VotingQuestionModel item in model.Questions)
                {
                    Question question = RepoFactory.Instance.GetRepo<IQuestionRepo>().Get(item.QuestionID);

                    if (question == null) { throw new Exception("Такого вопроса не существует!"); }

                    Answer answer = null;

                    if (question.TypeQuestion == TypeQuestion.RadioButton)
                    {
                        answer = RepoFactory.Instance.GetRepo<IAnswerRepo>().Get(item.IDCheck);

                        if (answer == null) { throw new Exception("Такого ответа не существует!"); }
                    }
                    else if (question.TypeQuestion == TypeQuestion.RadioButtonText)
                    {
                        answer = RepoFactory.Instance.GetRepo<IAnswerRepo>().Get(item.IDCheck);

                        if (answer == null) 
                        {
                            answer = new Answer()
                            {
                                Description = String.Empty,
                                Value = item.TextAnswer,
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
                        answer = RepoFactory.Instance.GetRepo<IAnswerRepo>().Get(item.IDChecks.FirstOrDefault());

                        if (answer == null) { throw new Exception("Такого ответа не существует!"); }
                    }
                    else if (question.TypeQuestion == TypeQuestion.Text)
                    {
                        answer = new Answer()
                        {
                            Description = String.Empty,
                            Value = item.TextAnswer,
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
                            DateStart = Convert.ToDateTime(item.DateStart),
                            DateEnd = Convert.ToDateTime(item.DateEnd),
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
                        List<VotingAnswerModel> newAnswers = item.Answers.Where(x => x.IsCheck).ToList();
                        List<TempVotingQuestion> oldAnswers = RepoFactory.Instance.GetRepo<ITempVotingQuestionRepo>().GetByQuestionVoting(item.QuestionID, model.VotingID).ToList();

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

                    if (question.TypeQuestion != TypeQuestion.Multiple)
                    {
                        TempVotingQuestion tempVotingQuestion = new TempVotingQuestion();
                        
                        tempVotingQuestion.Voting = voting;
                        tempVotingQuestion.Question = question;
                        tempVotingQuestion.Answer = answer;

                        tempVotingQuestion = RepoFactory.Instance.GetRepo<ITempVotingQuestionRepo>().Save(tempVotingQuestion);
                    }
                }
                                
                if (Session.User != null)
                {
                    SaveQuestionnaire(model.VotingID);
                    return RedirectToAction("Ready", "Questionnaire", new { charterID = model.CharterID, votingID = voting.ID });
                }
                else
                {
                    //return RedirectToAction("VotingInfoCheck", "Questionnaire", new { charterID = model.CharterID, votingID = voting.ID });
                    SaveQuestionnaire(model.VotingID);
                    return RedirectToAction("Ready", "Questionnaire", new { charterID = model.CharterID, votingID = voting.ID });
                }
            }
            else
            {
                return PartialView("_Question", model);
            }
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

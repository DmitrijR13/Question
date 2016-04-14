using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Oracle.DataAccess.Client;

using NHibernate;
using Sobits.Story.Logic.BLL;
using Sobits.Story.Logic.DataModel;

namespace Sobits.Story.Logic.Repo.Impl
{
    internal class QuestionRepo : BaseRepo<Question>, IQuestionRepo
    {
        public QuestionRepo(ISession session)
            : base(session)
        { }

        /// <summary>
        /// Получить все вопросы данного раздела
        /// </summary>
        /// <param name="charterID">Идентификатор раздела</param>
        /// <returns></returns>
        public IQueryable<Question> GetByCharter(Int32 charterID)
        {
            return GetAll().Where(x => !x.IsDelete &&
                                       x.Charter.ID == charterID)
                           .OrderBy(x => x.NumberSequence);
        }

        /// <summary>
        /// Получить первый вопрос данного раздела
        /// </summary>
        /// <param name="charterID">Идентификатор раздела</param>
        /// <returns></returns>
        public Question GetFirstByCharter(Int32 charterID)
        {
            return GetAll().Where(x => !x.IsDelete &&
                                       x.Charter.ID == charterID)
                           .OrderBy(x => x.NumberSequence)
                           .Join(
                                RepoFactory.Instance.GetRepo<IAnswerRepo>().GetAll(),
                                itemQuestion => itemQuestion.ID,
                                itemAnswer => itemAnswer.Question.ID,
                                (itemQuestion, itemAnswer) => new Question()
                                {
                                    ID = itemQuestion.ID,
                                    DateCreate = itemQuestion.DateCreate,
                                    DateUpdate = itemQuestion.DateUpdate,
                                    Description = itemQuestion.Description,
                                    NumberSequence = itemQuestion.NumberSequence,
                                    Charter = itemQuestion.Charter,
                                    UserCreate = itemQuestion.UserCreate,
                                    UserUpdate = itemQuestion.UserUpdate,
                                }
                            )
                           .FirstOrDefault();
        }

        /// <summary>
        /// Получить совокупность вопросов/ответов
        /// </summary>
        /// <param name="charterID">Идентификатор раздела</param>
        /// <returns></returns>
        public IQueryable<QuestionAnswerDM> GetUnionQuestionAnswer(Int32 charterID)
        {
            return GetAll().Where(x => !x.IsDelete &&
                                       x.Charter.ID == charterID)
                           .OrderBy(x => x.NumberSequence)
                           .Join(
                                RepoFactory.Instance.GetRepo<IAnswerRepo>().GetAll(),
                                itemQuestion => itemQuestion.ID,
                                itemAnswer => itemAnswer.Question.ID,
                                (itemQuestion, itemAnswer) => new QuestionAnswerDM()
                                {
                                    QuestionID = itemQuestion.ID,
                                    QuestionDateCreate = itemQuestion.DateCreate,
                                    QuestionDateUpdate = itemQuestion.DateUpdate,
                                    QuestionDescription = itemQuestion.Description,
                                    QuestionNumberSequence = itemQuestion.NumberSequence,
                                    QuestionCharterID = itemQuestion.Charter.ID,
                                    QuestionValue = itemQuestion.Value,
                                    AnswerID = itemAnswer.ID,
                                    AnswerDateCreate = itemAnswer.DateCreate,
                                    AnswerDateUpdate = itemAnswer.DateUpdate,
                                    AnswerDescription = itemAnswer.Description,
                                    AnswerNumberSequence = itemAnswer.NumberSequence,
                                    AnswerScore = itemAnswer.Score,
                                    AnswerValue = itemAnswer.Value,
                                    AnswerIsDelete = itemAnswer.IsDelete
                                }
                            )
                            .Where(x => !x.AnswerIsDelete);
        }
    }
}

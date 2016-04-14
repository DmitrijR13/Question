using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Oracle.DataAccess.Client;

using Sobits.Story.Logic.BLL;
using NHibernate;

namespace Sobits.Story.Logic.Repo.Impl
{
    internal class AnswerRepo : BaseRepo<Answer>, IAnswerRepo
    {
        public AnswerRepo(ISession session)
            : base(session)
        { }

        public IQueryable<Answer> GetByQuestion(Int32 questionID)
        {
            return GetAll().Where(x => x.Question.ID == questionID && !x.IsDelete);
        }

        public IQueryable<Answer> GetByQuestionAndText(Int32 questionID, String text)
        {
            return GetAll().Where(x => x.Question.ID == questionID && 
                                       !x.IsDelete &&
                                       (x.Value.ToLower().Contains(text.ToLower()) ||
                                       x.ValueAdditional_1 == text ||
                                       x.ValueAdditional_2 == text ||
                                       text == String.Empty))
                           .OrderBy(x => x.NumberSequence);
        }

        public void SaveDeleteList(List<Answer> answersDelete)
        {
            answersDelete = answersDelete.Select(x => new Answer()
                                          {
                                              ID = x.ID,
                                              DateCreate = x.DateCreate,
                                              DateUpdate = x.DateUpdate,
                                              Description = x.Description,
                                              IsDelete = true,
                                              NextQuestion = x.NextQuestion,
                                              NumberSequence = x.NumberSequence,
                                              Question = x.Question,
                                              Score = x.Score,
                                              UserCreate = x.UserCreate,
                                              UserUpdate = x.UserUpdate,
                                              Value = x.Value
                                          })
                                          .ToList();

            SaveList(answersDelete);
        }
    }
}

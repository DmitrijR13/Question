using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sobits.Story.Logic.BLL;
using Sobits.Story.Logic.DataModel;

namespace Sobits.Story.Logic.Repo
{
    /// <summary>
    /// Интерфейс репозитория сущности "Question"
    /// </summary>
    public interface IQuestionRepo : IRepo<Question>
    {
        /// <summary>
        /// Получить все вопросы данного раздела
        /// </summary>
        /// <param name="charterID">Идентификатор раздела</param>
        /// <returns></returns>
        IQueryable<Question> GetByCharter(Int32 charterID);

        /// <summary>
        /// Получить первый вопрос данного раздела
        /// </summary>
        /// <param name="charterID">Идентификатор раздела</param>
        /// <returns></returns>
        Question GetFirstByCharter(Int32 charterID);

        /// <summary>
        /// Получить объединение вопрос-ответы данного раздела
        /// </summary>
        /// <param name="charterID">Идентификатор раздела</param>
        /// <returns></returns>
        IQueryable<QuestionAnswerDM> GetUnionQuestionAnswer(Int32 charterID);
    }
}
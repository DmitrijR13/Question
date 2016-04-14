using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sobits.DataEngine.Repo;
using Sobits.Story.Logic.BLL;

namespace Sobits.Story.Logic.Repo
{
    /// <summary>
    /// Интерфейс репозитория сущности "Answer"
    /// </summary>
    public interface IAnswerRepo : IRepo<Answer>
    {
        /// <summary>
        /// Получить ответы на соответствующий вопрос
        /// </summary>
        /// <param name="questionID">ID вопроса</param>
        /// <returns></returns>
        IQueryable<Answer> GetByQuestion(Int32 questionID);

        /// <summary>
        /// Получить ответы на соответствующий вопрос с фильтром
        /// </summary>
        /// <param name="questionID">ID вопроса</param>
        /// <param name="text">Фраза фильтра</param>
        /// <returns></returns>
        IQueryable<Answer> GetByQuestionAndText(Int32 questionID, String text);

        /// <summary>
        /// Отметить ответы как удаленные
        /// </summary>
        /// <param name="answersDelete">Список удаляемых ответов</param>
        void SaveDeleteList(List<Answer> answersDelete);
    }
}
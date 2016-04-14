using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sobits.Story.Logic.BLL;

namespace Sobits.Story.Logic.Repo
{
    /// <summary>
    /// Интерфейс репозитория сущности "TempVotingQuestion"
    /// </summary>
    public interface ITempVotingQuestionRepo : IRepo<TempVotingQuestion>
    {
        /// <summary>
        /// Получить ответ голосующего на данный вопрос
        /// </summary>
        /// <param name="questionID">ID вопроса</param>
        /// <param name="votingID">ID голосующего</param>
        /// <returns></returns>
        IQueryable<TempVotingQuestion> GetByQuestionVoting(Int32 questionID, Int32 votingID);

        /// <summary>
        /// Получить ответы голосующего текущей анкеты
        /// </summary>
        /// <param name="votingID">ID голосующего</param>
        /// <returns></returns>
        IQueryable<TempVotingQuestion> GetByVoting(Int32 votingID);

        /// <summary>
        /// Удалить данные опроса голосующего
        /// </summary>
        /// <param name="votingID">ID голосующего</param>
        void DeleteByVoting(Int32 votingID);
    }
}
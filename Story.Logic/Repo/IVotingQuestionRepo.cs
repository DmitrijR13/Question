using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sobits.DataEngine.Repo;
using Sobits.Story.Logic.BLL;
using Sobits.Story.Logic.DataModel;

namespace Sobits.Story.Logic.Repo
{
    /// <summary>
    /// Интерфейс репозитория сущности "VotingQuestion"
    /// </summary>
    public interface IVotingQuestionRepo : IRepo<VotingQuestion>
    {
        /// <summary>
        /// Получить ответ голосующего на данный вопрос
        /// </summary>
        /// <param name="questionID">ID вопроса</param>
        /// <param name="votingID">ID голосующего</param>
        /// <returns></returns>
        IQueryable<VotingQuestion> GetByQuestionVoting(Int32 questionID, Int32 votingID);

        /// <summary>
        /// Копируем данные из временной таблицы
        /// </summary>
        /// <param name="votingID">ID голосующего</param>
        void CopyFromTemp(Int32 votingID);

        /// <summary>
        /// Построение статистики на основе выбранных ответов
        /// </summary>
        /// <param name="answerIDs">Массив ID ответов</param>
        /// <param name="charterID">ID категории</param>
        List<StatisticDM> BuildStatistic(List<Int32> answerIDs, Int32 charterID);
        
        /// <summary>
        /// Получить все вопросы-ответы по конкретному разделу
        /// </summary>
        /// <returns></returns>
        List<QuestionAnswerDM> GetQuestionAnswerByCharter(Int32 charterID);
    }
}
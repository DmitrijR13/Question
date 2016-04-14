using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sobits.Story.Logic.BLL;
using Sobits.Story.Logic.DataModel;

namespace Sobits.Story.Logic.Repo
{
    /// <summary>
    /// Интерфейс репозитория сущности "Voting"
    /// </summary>
    public interface IVotingRepo : IRepo<Voting>
    {
        /// <summary>
        /// Получить реальные голосования
        /// </summary>
        /// <param name="charterID">ID раздела</param>
        /// <returns></returns>
        List<VotingDM> GetActualVoting(Int32 charterID);
    }
}
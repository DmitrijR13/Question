using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Oracle.DataAccess.Client;

using Sobits.DataEngine.Repo.Impl;
using Sobits.Story.Logic.BLL;
using System.Collections;

namespace Sobits.Story.Logic.Repo.Impl
{
    internal class TempVotingQuestionRepo : AbstractRepo<TempVotingQuestion>, ITempVotingQuestionRepo
    {
        public TempVotingQuestionRepo(DataEngine.IDataSession session)
            : base(session)
        { }

        public IQueryable<TempVotingQuestion> GetByQuestionVoting(Int32 questionID, Int32 votingID)
        {
            return GetAll().Where(x => !x.Question.IsDelete &&
                                       x.Question.ID == questionID &&
                                       x.Voting.ID == votingID);
        }

        public IQueryable<TempVotingQuestion> GetByVoting(Int32 votingID)
        {
            return GetAll().Where(x => !x.Question.IsDelete &&
                                       x.Voting.ID == votingID)
                           .OrderBy(x => x.ID);
        }

        public void DeleteByVoting(Int32 votingID)
        {
            Context.CreateSQLQuery("begin DBQ.PCD_DELETE_TEMP_VOTING(:votingid); end;")
                .SetInt32("votingid", votingID)
                .ExecuteUpdate();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentNHibernate.Mapping;

using Sobits.Story.Logic.BLL;

namespace Sobits.Story.Logic.Mappings
{
    /// <summary>
    /// Маппинг сущности
    /// </summary>
    public class TempVotingQuestionMap : ClassMap<TempVotingQuestion>
    {
        /// <summary>
        /// Конструктор по-умолчанию
        /// </summary>
        public TempVotingQuestionMap()
        {
            Table("tmp_Voting_Question");
            Id(x => x.ID).GeneratedBy.Native("sqnTmpVotingQuestion").Column("ID");
            References(x => x.Question).Column("int_QuestionID").Not.Nullable();
            References(x => x.Answer).Column("int_AnswerID").Not.Nullable();
            References(x => x.Voting).Column("int_VotingID").Not.Nullable();
        }
    }
}
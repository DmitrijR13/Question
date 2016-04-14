using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentNHibernate.Mapping;

using Sobits.DataEngine;
using Sobits.Story.Logic.BLL;

namespace Sobits.Story.Logic.Mappings
{
    /// <summary>
    /// Маппинг сущности
    /// </summary>
    public class AnswerNextQuestionMap : ClassMap<AnswerNextQuestion>
    {
        /// <summary>
        /// Конструктор по-умолчанию
        /// </summary>
        public AnswerNextQuestionMap()
        {
            Table("tbl_Answer_Next_Question");
            Id(x => x.ID).GeneratedBy.Native("sqnTblAnswerNextQuestion").Column("ID");
            References(x => x.Answer).Column("int_AnswerID").Not.Nullable();
            References(x => x.Question).Column("int_QuestionID").Not.Nullable();
        }
    }
}
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
    public class AnswerMap : ClassMap<Answer>
    {
        /// <summary>
        /// Конструктор по-умолчанию
        /// </summary>
        public AnswerMap()
        {
            Table("tbl_Answer");
            Id(x => x.ID).GeneratedBy.Native("sqnTblAnswer").Column("ID");
            Map(x => x.Value).Not.Nullable().Column("str_Value").Length(750);
            Map(x => x.ValueAdditional_1).Nullable().Column("str_ValueAdditional_1").Length(750);
            Map(x => x.ValueAdditional_2).Nullable().Column("str_ValueAdditional_2").Length(750);
            Map(x => x.DateStart).Nullable().Column("dtm_DateStart");
            Map(x => x.DateEnd).Nullable().Column("dtm_DateEnd");
            Map(x => x.Score).Not.Nullable().Column("dml_Score");
            Map(x => x.NumberSequence).Not.Nullable().Column("int_NumberSequence");
            Map(x => x.Description).Nullable().Column("str_Description").Length(750);
            Map(x => x.IsDelete).Not.Nullable().Column("bit_IsDelete");
            Map(x => x.DateCreate).Not.Nullable().Column("dtm_DateCreate");
            Map(x => x.DateUpdate).Not.Nullable().Column("dtm_DateUpdate");
            References(x => x.UserCreate).Column("int_UserCreateID").Nullable();
            References(x => x.UserUpdate).Column("int_UserUpdateID").Nullable();
            References(x => x.Question).Column("int_QuestionID").Nullable();
            References(x => x.NextQuestion).Column("int_NextQuestionID").Nullable();
        }
    }
}
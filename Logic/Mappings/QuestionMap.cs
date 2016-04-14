using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentNHibernate.Mapping;

using Sobits.Story.Logic.BLL;
using Sobits.Story.Logic.Enumerable;

namespace Sobits.Story.Logic.Mappings
{
    /// <summary>
    /// Маппинг сущности
    /// </summary>
    public class QuestionMap : ClassMap<Question>
    {
        /// <summary>
        /// Конструктор по-умолчанию
        /// </summary>
        public QuestionMap()
        {
            Table("tbl_Question");
            Id(x => x.ID).GeneratedBy.Native("sqnTblQuestion").Column("ID");
            Map(x => x.Value).Not.Nullable().Column("str_Value").Length(750);
            Map(x => x.NumberSequence).Not.Nullable().Column("int_NumberSequence");
            Map(x => x.Description).Nullable().Column("str_Description").Length(750);
            Map(x => x.TypeQuestion).Not.Nullable().Column("int_TypeQuestion").CustomType<Int32>();
            Map(x => x.IsDelete).Nullable().Column("bit_IsDelete");
            Map(x => x.DateCreate).Not.Nullable().Column("dtm_DateCreate").Length(750);
            Map(x => x.DateUpdate).Not.Nullable().Column("dtm_DateUpdate").Length(750);
            References(x => x.UserCreate).Column("int_UserCreateID").Nullable();
            References(x => x.UserUpdate).Column("int_UserUpdateID").Nullable();
            References(x => x.Charter).Column("int_CharterID").Not.Nullable();
            HasMany(x => x.Answers).Table("tbl_Answer").KeyColumn("int_QuestionID").KeyNullable().LazyLoad();
        }
    }
}
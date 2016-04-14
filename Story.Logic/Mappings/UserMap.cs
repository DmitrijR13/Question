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
    public class UserMap : ClassMap<User>
    {
        /// <summary>
        /// Конструктор по-умолчанию
        /// </summary>
        public UserMap()
        {
            Table("tbl_User");
            Id(x => x.ID).GeneratedBy.Native("sqnTblUser").Column("ID");
            Map(x => x.Email).Not.Nullable().Column("str_Email").Length(150);
            Map(x => x.Password).Not.Nullable().Column("unq_Password").Length(750);
            Map(x => x.FirstName).Not.Nullable().Column("str_FirstName").Length(750);
            Map(x => x.SecondName).Not.Nullable().Column("str_SecondName").Length(750);
            Map(x => x.LastName).Not.Nullable().Column("str_LastName").Length(750);
            Map(x => x.IsOneQuestion).Not.Nullable().Column("bit_IsOneQuestion").Length(750);
            Map(x => x.DateCreate).Not.Nullable().Column("dtm_DateCreate").Length(750);
            Map(x => x.DateUpdate).Not.Nullable().Column("dtm_DateUpdate").Length(750);
            References(x => x.UserCreate).Column("int_UserCreateID").Nullable();
            References(x => x.UserUpdate).Column("int_UserUpdateID").Nullable();
            HasMany(x => x.UserRoles).Table("tbl_UserRole").KeyColumn("int_UserID").KeyNullable().LazyLoad();
        }
    }
}
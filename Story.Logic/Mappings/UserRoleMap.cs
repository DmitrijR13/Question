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
    public class UserRoleMap : ClassMap<UserRole>
    {
        /// <summary>
        /// Конструктор по-умолчанию
        /// </summary>
        public UserRoleMap()
        {
            Table("tbl_User_Role");
            Id(x => x.ID).GeneratedBy.Native("sqnTblUserRole").Column("ID");
            References(x => x.User).Column("int_UserID").Not.Nullable();
            References(x => x.Role).Column("int_RoleID").Not.Nullable();
        }
    }
}
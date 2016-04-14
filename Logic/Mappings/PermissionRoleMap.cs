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
    public class PermissionRoleMap : ClassMap<PermissionRole>
    {
        /// <summary>
        /// Конструктор по-умолчанию
        /// </summary>
        public PermissionRoleMap()
        {
            Table("tbl_PermissionRole");
            Id(x => x.ID).GeneratedBy.Native("sqnTblPermissionRole").Column("ID");
            References(x => x.Role).Column("int_RoleID").Not.Nullable();
            References(x => x.Permission).Column("int_PermissionID").Not.Nullable();
        }
    }
}
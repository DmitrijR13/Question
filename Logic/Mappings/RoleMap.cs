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
    public class RoleMap : ClassMap<Role>
    {
        /// <summary>
        /// Конструктор по-умолчанию
        /// </summary>
        public RoleMap()
        {
            Table("tbl_Role");
            Id(x => x.ID).GeneratedBy.Native("sqnTblRole").Column("ID");
            Map(x => x.Name).Not.Nullable().Column("str_Name").Length(750);
            Map(x => x.ServiceName).Not.Nullable().Column("str_ServiceName").Length(750);
            HasMany(x => x.PermissionRoles).Table("tbl_PermissionRole").KeyColumn("int_PermissionID").KeyNullable().LazyLoad();
        }
    }
}
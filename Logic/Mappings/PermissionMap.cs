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
    public class PermissionMap : ClassMap<Permission>
    {
        /// <summary>
        /// Конструктор по-умолчанию
        /// </summary>
        public PermissionMap()
        {
            Table("tbl_Permission");
            Id(x => x.ID).GeneratedBy.Native("sqnTblPermission").Column("ID");
            Map(x => x.Name).Not.Nullable().Column("str_Name").Length(750);
            Map(x => x.Value).Not.Nullable().Column("str_Value").Length(750);
        }
    }
}
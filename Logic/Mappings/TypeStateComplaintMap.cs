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
    public class TypeStateComplaintMap : ClassMap<TypeStateComplaint>
    {
        /// <summary>
        /// Конструктор по-умолчанию
        /// </summary>
        public TypeStateComplaintMap()
        {
            Table("lkp_TypeStateComplaint");
            Id(x => x.ID).GeneratedBy.Native("sqnTblTypeStateComplaint").Column("ID");
            Map(x => x.Name).Not.Nullable().Column("str_Name").Length(750);
            Map(x => x.IsDelete).Not.Nullable().Column("bit_IsDelete");
        }
    }
}
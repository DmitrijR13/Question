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
    public class TypeCategoryComplaintMap : ClassMap<TypeCategoryComplaint>
    {
        /// <summary>
        /// Конструктор по-умолчанию
        /// </summary>
        public TypeCategoryComplaintMap()
        {
            Table("lkp_TypeCategoryComplaint");
            Id(x => x.ID).GeneratedBy.Native("sqnTblTypeCategoryComplaint").Column("ID");
            Map(x => x.Name).Not.Nullable().Column("str_Name").Length(750);
            Map(x => x.Price).Not.Nullable().Column("dcm_Price");
            Map(x => x.IsDelete).Not.Nullable().Column("bit_IsDelete");
        }
    }
}
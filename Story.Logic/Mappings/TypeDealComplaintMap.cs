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
    public class TypeDealComplaintMap : ClassMap<TypeDealComplaint>
    {
        /// <summary>
        /// Конструктор по-умолчанию
        /// </summary>
        public TypeDealComplaintMap()
        {
            Table("lkp_TypeDealComplaint");
            Id(x => x.ID).GeneratedBy.Native("sqnTblTypeDealComplaint").Column("ID");
            Map(x => x.Name).Not.Nullable().Column("str_Name").Length(750);
            Map(x => x.IsDelete).Not.Nullable().Column("bit_IsDelete");
        }
    }
}
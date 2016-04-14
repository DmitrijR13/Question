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
    public class ComplaintMap : ClassMap<Complaint>
    {
        /// <summary>
        /// Конструктор по-умолчанию
        /// </summary>
        public ComplaintMap()
        {
            Table("tbl_Complaint");
            Id(x => x.ID).GeneratedBy.Native("sqnTblComplaint").Column("ID");
            Map(x => x.FirstName).Not.Nullable().Column("str_FirstName").Length(750);
            Map(x => x.SecondName).Nullable().Column("str_SecondName").Length(750);
            Map(x => x.LastName).Not.Nullable().Column("str_LastName").Length(750);
            Map(x => x.FederalCodeMO).Not.Nullable().Column("int_FederalCodeMO");
            Map(x => x.Phone).Not.Nullable().Column("str_Phone");
            Map(x => x.Text).Not.Nullable().Column("str_Text").Length(9000);
            References(x => x.TypeDeal).Column("int_TypeDealComplaintID").Not.Nullable();
            References(x => x.TypeCategory).Column("int_TypeCategoryComplaintID").Nullable();
            References(x => x.TypeState).Column("int_TypeStateComplaintID").Nullable();
        }
    }
}
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
    public class SMOOrganizationMap : ClassMap<SMOOrganization>
    {
        /// <summary>
        /// Конструктор по-умолчанию
        /// </summary>
        public SMOOrganizationMap()
        {
            Table("lkp_SMOOrganization");
            Id(x => x.ID).GeneratedBy.Native("sqnLkpSMOOrganization").Column("ID");
            Map(x => x.Code).Not.Nullable().Column("int_Code");
            Map(x => x.Name).Not.Nullable().Column("str_Name").Length(750);
        }
    }
}
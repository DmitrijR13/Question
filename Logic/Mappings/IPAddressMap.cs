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
    public class IPAddressMap : ClassMap<IPAddress>
    {
        /// <summary>
        /// Конструктор по-умолчанию
        /// </summary>
        public IPAddressMap()
        {
            Table("lkp_IPAddress");
            Id(x => x.ID).GeneratedBy.Native("sqnLkpIPAddress").Column("ID");
            Map(x => x.Value).Column("str_Value").Not.Nullable();
            Map(x => x.NameOrganization).Column("str_NameOrganization").Not.Nullable();
        }
    }
}
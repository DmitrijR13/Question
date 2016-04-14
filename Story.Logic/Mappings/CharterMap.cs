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
    public class CharterMap : ClassMap<Charter>
    {
        /// <summary>
        /// Конструктор по-умолчанию
        /// </summary>
        public CharterMap()
        {
            Table("lkp_Charter");
            Id(x => x.ID).GeneratedBy.Native("sqnLkpCharter").Column("ID");
            Map(x => x.Name).Not.Nullable().Column("str_Name").Length(750);
            Map(x => x.IsDelete).Nullable().Column("bit_Delete");
            References(x => x.Image).Column("int_ImageID").Nullable();
        }
    }
}
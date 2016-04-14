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
    public class BFileMap : ClassMap<BFile>
    {
        /// <summary>
        /// Конструктор по-умолчанию
        /// </summary>
        public BFileMap()
        {
            Table("tbl_BFIle");
            Id(x => x.ID).GeneratedBy.Native("sqnTblBFile").Column("ID");
            Map(x => x.FileName).Not.Nullable().Column("str_FileName").Length(750);
            Map(x => x.Guid).Not.Nullable().Column("unq_GUID").Length(750);
        }
    }
}
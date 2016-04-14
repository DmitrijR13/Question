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
    public class ListAnswerMap : ClassMap<ListAnswer>
    {
        /// <summary>
        /// Конструктор по-умолчанию
        /// </summary>
        public ListAnswerMap()
        {
            Table("tbl_List_Answer");
            Id(x => x.ID).GeneratedBy.Native("sqnTblListAnswer").Column("ID");
            References(x => x.Answer).Column("int_AnswerID").Not.Nullable();
            References(x => x.AnswerPrev).Column("int_AnswerPrevID").Not.Nullable();
        }
    }
}
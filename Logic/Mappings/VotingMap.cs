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
    public class VotingMap : ClassMap<Voting>
    {
        /// <summary>
        /// Конструктор по-умолчанию
        /// </summary>
        public VotingMap()
        {
            Table("tbl_Voting");
            Id(x => x.ID).GeneratedBy.Native("sqnTblVoting").Column("ID");
            Map(x => x.FirstName).Nullable().Column("str_FirstName").Length(750);
            Map(x => x.SecondName).Nullable().Column("str_SecondName").Length(750);
            Map(x => x.LastName).Nullable().Column("str_LastName").Length(750);
            Map(x => x.IsAnonymous).Not.Nullable().Column("bit_IsAnonymous");
            Map(x => x.DateVote).Not.Nullable().Column("dtm_DateVote");
            References(x => x.IPAddress).Column("int_IPAddressID").Nullable();
            References(x => x.MOOrganization).Column("int_MOOrganizationID").Not.Nullable();
            References(x => x.SMOOrganization).Column("int_SMOOrganizationID").Not.Nullable();
        }
    }
}
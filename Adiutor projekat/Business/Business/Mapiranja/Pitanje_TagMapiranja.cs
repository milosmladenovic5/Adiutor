using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entiteti;
using FluentNHibernate.Mapping;

namespace Business.Mapiranja
{
    class Pitanje_TagMapiranja : ClassMap<Pitanje_Tag>
    {
        public Pitanje_TagMapiranja() {
            Table("PITANJE_TAG");

            Id(x => x.Id).Column("ID").GeneratedBy.TriggerIdentity();

            HasMany(x => x.ImaPitanja).KeyColumn("PITANJE_ID");
            HasMany(x => x.ImaTagove).KeyColumn("TAG_ID");
        }
    }
}

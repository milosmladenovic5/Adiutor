using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entiteti;
using FluentNHibernate.Mapping;

namespace Business.Mapiranja
{
    class SmerMapiranja : ClassMap<Smer>
    {
        public SmerMapiranja()
        {
            Table("SMER");

            Id(x => x.Id).Column("ID").GeneratedBy.TriggerIdentity();

            Map(x => x.Ime).Column("IME");

            HasManyToMany(x => x.ImaPredmete).Table("PREDMET_SMER").ParentKeyColumn("SMER_ID")
                .ChildKeyColumn("PREDMET_ID")
                .Cascade.All();
        }
    }
}

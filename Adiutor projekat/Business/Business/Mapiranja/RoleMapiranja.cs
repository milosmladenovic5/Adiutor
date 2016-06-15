using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entiteti;
using FluentNHibernate.Mapping;

namespace Business.Mapiranja
{
    class RoleMapiranja : ClassMap<Role>
    {
        public RoleMapiranja()
        {
            Table("ROLE");

            Id(x => x.Id).Column("ID").GeneratedBy.TriggerIdentity();

            Map(x => x.Ime).Column("IME");
            Map(x => x.Opis).Column("OPIS");

            
        }
    }
}

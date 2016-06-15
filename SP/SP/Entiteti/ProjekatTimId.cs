using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Studentski_projekti.Entiteti
{
    public class ProjekatTimId
    {
        public Projekat TimskiProjekat { get; set; }
        public Tim TimProjekta { get; set; }

        public override bool Equals(object obj)
        {
            if (Object.ReferenceEquals(this, obj))
                return true;

            if (obj.GetType() != typeof(ProjekatTimId))
                return false;

            ProjekatTimId recievedObject = (ProjekatTimId)obj;

            if ((TimskiProjekat.Id == recievedObject.TimskiProjekat.Id) &&
                (TimProjekta.Id == recievedObject.TimProjekta.Id))
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entiteti
{
    public class Predmet
    {
        public virtual int Id { get; set; }
        public virtual string Naziv { get; set; }
        public virtual int GodinaStudija { get; set; }
        public virtual int Semestar { get; set; }

        public virtual Korisnik ZaduzeniProfesor { get; set; }

        public virtual IList<Profesor> ImaProfesore { get; set; }
        public virtual IList<Smer> PripadaSmerovima { get; set; }
     

        public Predmet()
        {
            ImaProfesore = new List<Profesor>();
            PripadaSmerovima  = new List<Smer>();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entiteti
{
    public class Smer
    {
        public virtual int Id { get; set; }
        public virtual string Ime { get; set; }

        public virtual IList<Predmet> ImaPredmete { get; set; }

        public Smer()
        {
            ImaPredmete = new List<Predmet>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using Business.Entiteti;

namespace Business.Data_Access
{
    public static class Odgovori
    {
        public static void Dodaj(string tekst, DateTime datum, int plus, int minus, int odobreno, int PitanjeId, int KorisnikId)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Odgovor r = new Odgovor
                {
                    Tekst = tekst,
                    DatumVreme = datum,
                    Plus = plus,
                    Minus = minus,
                    Odobreno = odobreno
                };

                Pitanje p = s.Load<Pitanje>(PitanjeId);
                r.PripadaPitanju = p;

                Korisnik k = s.Load<Korisnik>(KorisnikId);
                r.ImaKorisnika = k;

                s.SaveOrUpdate(r);
                s.Flush();
                s.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        static public void Obrisi(int id)
        {
            try
            {
                ISession s = DataLayer.GetSession();


                Odgovor st = s.Load<Odgovor>(id);

                s.Delete(st);
                s.Flush();
                s.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}

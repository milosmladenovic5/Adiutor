using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using Business.Entiteti;

namespace Business.Data_Access
{
    public static class Pitanja
    {
        public static void Dodaj(string tekst, DateTime datum, int KorisnikId, int OblastId)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Pitanje r = new Pitanje
                {
                    Tekst = tekst,
                    DatumVreme = datum
                };


                Oblast p = s.Load<Oblast>(OblastId);
                r.PripadaOblasti = p;

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


                Pitanje st = s.Load<Pitanje>(id);

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using Business.Entiteti;

namespace Business.Data_Access
{
    public static class Komentari
    {
        public static void Dodaj(string tekst, DateTime datum, int KorisnikId, int OdgovorId)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Komentar r = new Komentar
                {
                    Tekst = tekst,
                    DatumVreme = datum
                };


                Odgovor p = s.Load<Odgovor>(OdgovorId);
                r.NaOdgovor = p;

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


                Korisnik st = s.Load<Korisnik>(id);

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

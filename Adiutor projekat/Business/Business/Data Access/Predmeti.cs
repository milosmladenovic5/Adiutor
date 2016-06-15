using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using Business.Entiteti;

namespace Business.Data_Access
{
    public static class Predmeti
    {
        public static void Dodaj(string naziv, int semestar, int godina)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Predmet r = new Predmet
                {
                    Naziv = naziv,
                    Semestar = semestar,
                    GodinaStudija = godina
                };

                //Smer smer = s.Load<Smer>(38);
                //r.PripadaSmerovima.Add(smer);

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


                Predmet st = s.Load<Predmet>(id);

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

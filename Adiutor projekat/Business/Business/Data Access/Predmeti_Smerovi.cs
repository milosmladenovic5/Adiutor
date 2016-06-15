using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using Business.Entiteti;

namespace Business.Data_Access
{
    public static class Predmeti_Smerovi
    {
        public static void Dodaj(int PredmetId, int SmerId)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Predmet_Smer r = new Predmet_Smer
                {
                    
                };

                Predmet p = s.Load<Predmet>(PredmetId);
                Smer smer = s.Load<Smer>(SmerId);

                r.ImaPredmete.Add(p);
                r.ImaSmerove.Add(smer);

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


                Predmet_Smer st = s.Load<Predmet_Smer>(id);

                s.Delete(st);
                //s.Flush();
                s.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}

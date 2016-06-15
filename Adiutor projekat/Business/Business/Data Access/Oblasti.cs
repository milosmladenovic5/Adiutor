using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using Business.Entiteti;

namespace Business.Data_Access
{
    public static class Oblasti
    {
        public static void Dodaj(string ime)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Oblast r = new Oblast
                {
                    Ime = ime,
                };

                Predmet p = s.Load<Predmet>(20);

                r.PripadaPredmetu = p;

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


                Oblast st = s.Load<Oblast>(id);

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

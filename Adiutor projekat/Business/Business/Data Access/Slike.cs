using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using Business.Entiteti;

namespace Business.Data_Access
{
    public static class Slike
    {
        public static void Dodaj(string link, int OdgovorId)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Slika r = new Slika
                {
                    Link = link
                };

                
                Odgovor p = s.Load<Odgovor>(OdgovorId);

                r.PripadaOdgovoru = p;

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


                Slika st = s.Load<Slika>(id);

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using Business.Entiteti;

namespace Business.Data_Access
{
    public static class Literature
    {
        public static void Dodaj(string ime, string link)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Literatura r = new Literatura
                {
                    Naziv = ime,
                    Link = link
                };

                Oblast p = s.Load<Oblast>(17);

                r.PripadaOblasti = p;

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


                Literatura st = s.Load<Literatura>(id);

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

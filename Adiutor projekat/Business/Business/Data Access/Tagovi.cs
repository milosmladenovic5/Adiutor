using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using Business.Entiteti;

namespace Business.Data_Access
{
    public static class Tagovi
    {
        public static void Dodaj(string ime, string opis, string tagime, int PitanjeId)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Tag r = new Tag
                {
                    Ime = ime,
                    Opis = opis,
                    TagIme = tagime
                };

                Pitanje p = s.Load<Pitanje>(PitanjeId);

                r.PripadaPitanjima.Add(p);

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


                Tag st = s.Load<Tag>(id);

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

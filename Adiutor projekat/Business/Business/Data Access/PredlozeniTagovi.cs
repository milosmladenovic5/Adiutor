using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using Business.Entiteti;

namespace Business.Data_Access
{
    public static class PredlozeniTagovi
    {
        public static void Dodaj(string ime, string opis, DateTime datum_obrade, DateTime datum_postavljanja, string tag_ime)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Predlozeni_Tag r = new Predlozeni_Tag
                {
                    Ime = ime,
                    Opis = opis,
                    DatumObrade = datum_obrade,
                    DatumPostavljanja = datum_postavljanja,
                    TagIme = tag_ime
                };

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


                Predlozeni_Tag st = s.Load<Predlozeni_Tag>(id);

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

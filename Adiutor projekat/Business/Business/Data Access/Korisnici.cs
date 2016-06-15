using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using Business.Entiteti;

namespace Business.Data_Access
{
    public static class Korisnici
    {
        public static void Dodaj(string user, string pass, string email, string ime, string prezime, decimal indeks, string slika, string smer, string opis, decimal godna)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Korisnik r = new Korisnik
                {
                    Username = user,
                    Password = pass,
                    Email = email,
                    Ime = ime,
                    Prezime = prezime,
                    BrojIndeksa = indeks,
                    Slika = slika,
                    Smer = smer,
                    Opis = opis,
                    GodinaStudija = godna
                };

                r.ImaRolu = s.Load<Role>(1);
                r.ImaStatus = s.Load<Status>(1);


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

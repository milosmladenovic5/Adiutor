using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entiteti;
using Business.Mapiranja;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using Business.Data_Access;

namespace Business
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                //Statusi.DodajStatus("administrator", "administrira sajt");

                //Statusi.ObrisiStatus(19);
                //Roles.DodajRolu("administrator", "administrira sajt");
                //Roles.ObrisiRolu(18);
                //Korisnici.Dodaj("rank", "1234", "nikolarank94@gmail.com","nikola", "rankovic", 14885, "", "RII", "Ja sam nikola rankovic", 3);
                //Korisnici.Obrisi(17);
                //Smerovi.Dodaj("EEN");
                //Smerovi.Obrisi(37);
                //Predmeti.Dodaj("OOP", 3, 2);
                //Predmeti_Smerovi.Dodaj(17, 38);
                //Predmeti_Smerovi.Obrisi(17); OBRTATI PAZNJU
                //Profesori.Dodaj("Dragan Draganic");
                //Oblasti.Dodaj("Strukture");
                //Literature.Dodaj("Neka knjiga", "www.knjige.net");
                //Pitanja.Dodaj("Ovo je jedno pitanje", new DateTime(2015, 05, 20), 37, 17);
                //Pitanja.Obrisi(17);
                //Tagovi.Dodaj("fizika", "fizika", "fizika", 18);
                //Odgovori.Dodaj("Ovo je odgovor", new DateTime(2016, 05, 20), 10, 5, 1, 18, 37);
                //PredlozeniTagovi.Dodaj("novi tag", "ovo je novi tag", new DateTime(2016, 05, 20), new DateTime(2016, 05, 18), "novi_tag");
                //Slike.Dodaj("www.slike.com", 17);
                Komentari.Dodaj("ovo je komentar", new DateTime(2016, 05, 22), 37, 17);
                s.Close();
            }
            catch (Exception ec)
            {
                Console.WriteLine(ec.Message);
            }
        }
    }
}
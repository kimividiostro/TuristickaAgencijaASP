using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using WebApplication1.Models;
using System.Web.Mvc;

namespace WebApplication1
{
    
    //BROKER KLASA IMPLEMENTIRA SINGLTON PATERN I CEO RAD SA BAZOM IDE PREKO NJE
    public class Broker
    {

        private static Broker instanca;

        private SqlConnection konekcija;
        private SqlDataReader citac;
        private SqlCommand komanda;

        private string connectionString;

        private Broker()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            konekcija = new SqlConnection(connectionString);
            komanda = new SqlCommand();
            komanda.Connection = konekcija;
        }

        public static Broker Instanca
        {
            get
            {
                if (instanca == null)
                    return new Broker();

                return instanca;
            }
        }
        

        //UCITAJ SVE PODATKE O ARANZMANIMA IZ BAZE I PRAVI LISTU OBJEKATA
        public List<Aranzman> UcitajSveAranzmane()
        {
            List<Aranzman> aranzmani = new List<Aranzman>();
            komanda.CommandType = System.Data.CommandType.Text;
            komanda.CommandText = "SELECT * FROM dbo.Aranzmani";

            try
            {
                konekcija.Open();
                citac = komanda.ExecuteReader();
                while (citac.Read())
                {
                    Aranzman a = new Aranzman
                    {
                        Id = Convert.ToInt32(citac[0]),
                        Destinacija = citac[1].ToString(),
                        Datum = (DateTime)citac[2],
                        Cena = Convert.ToInt32(citac[3])
                    };
                    aranzmani.Add(a);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Greska pri citanju iz baze.");
            }
            finally
            {
                konekcija.Close();
            }
            return aranzmani;
        }



        // VRATI LISTU REZERVISANI ARANZMANA ZA KORISNIKA SA 
        // ODGOVARAJUCIM ID
        public List<Aranzman> RezervisaniAranzmani(string korisnikID)
        {
            List<Aranzman> aranzmani = new List<Aranzman>();
            komanda.CommandText = $"SELECT * FROM dbo.Aranzmani INNER JOIN dbo.Rezervisao ON dbo.Aranzmani.Id = dbo.Rezervisao.aranzmanID WHERE korisnikID = '{korisnikID}'";
            try
            {
                konekcija.Open();
                citac = komanda.ExecuteReader();
                while (citac.Read())
                {
                    Aranzman a = new Aranzman
                    {
                        Id = Convert.ToInt32(citac[0]),
                        Destinacija = citac[1].ToString(),
                        Datum = (DateTime)citac[2],
                        Cena = Convert.ToInt32(citac[3])
                    };
                    aranzmani.Add(a);
                }
            }
            catch(Exception)
            {
                Console.WriteLine("Greska pri povezivanju s bazom.");
            }
            finally
            {
                konekcija.Close();
            }
            return aranzmani;
        }


        
        private void RezervisiAranzman(string korisnikID, Aranzman aranzman)
        {
            komanda.CommandText = $"INSERT INTO dbo.Rezervisao VALUES('{korisnikID}', {aranzman.Id})";
            try
            {
                konekcija.Open();
                komanda.ExecuteNonQuery();
            }
            catch (Exception)
            {
                Console.WriteLine("Greska pri konekciji sa bazom.");
            }
            finally
            {
                konekcija.Close();
            }
        }


        //POZIVA PRIVATNU METODU REZERVISIARANZMAN
        //VRACA BOOL AKO JE USPESNO REZERVISANO
        [HttpPost]
        public bool ProveriIRezervisi(string korisnikID, Aranzman aranzman)
        {
            
            var aranzmani = RezervisaniAranzmani(korisnikID);


            bool vecRezervisao = aranzmani.Contains(aranzman) ? true : false;

            if (vecRezervisao)
                return false;
            else
                RezervisiAranzman(korisnikID, aranzman);

            return true;
        }


        //ZA DODAVANJE NOVIH ARANZMANA U BAZU
        //SAMO ZA ADMINA
        //VRACA BOOL AKO JE USPESNO REZERVISANO
        [HttpPost]
        public bool DodajAranzman(Aranzman aranzman)
        {
            bool uspelo = false;

            komanda.CommandText = $"INSERT INTO dbo.Aranzmani(Destinacija,Datum,Cena) VALUES('{aranzman.Destinacija}', '{aranzman.Datum.ToString("yyyy.M.d")}', {aranzman.Cena})";
            try
            {
                konekcija.Open();
                komanda.ExecuteNonQuery();
                uspelo = true;
            }
            catch (Exception)
            {
                Console.WriteLine("Neuspesno upisivanje.");
            }
            finally
            {
                konekcija.Close();
            }

            return uspelo;
        }
    }
}
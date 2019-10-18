using System.Collections.Generic;
using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class DetaljiViewModel
    {
        public DetaljiViewModel(HashSet<int> aranzmani, ApplicationUser korisnik)
        {
            this.Aranzmani = aranzmani;
            this.Korisnik = korisnik;
        }
        public HashSet<int> Aranzmani { get; set; }
        
        public ApplicationUser Korisnik { get; set; }
    }
}
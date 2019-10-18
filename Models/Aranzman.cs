using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public struct Aranzman
    {

        [Required(ErrorMessage ="Obavezno polje")]   
        public int Id { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        public string Destinacija { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        public DateTime Datum { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        public int Cena { get; set; }

    }
}
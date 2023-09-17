using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace web2projekat.Models
{
    public class Artikal
    {
        
        public int Id { get; set; }
        public string Naziv { get; set; }
        public double Cena { get; set; }
        public int Kolicina { get; set; }
        public string Opis { get; set; }
        public int ProdavacId { get; set; }
        public Korisnik Prodavac { get; set; }
        public List<Narudzbina> NarudzbineId { get; set; }

    }
}

using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace web2projekat.Models
{
    public class Artikal
    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
        public float Cena { get; set; }
        public int Kolicina { get; set; }
        public string Opis { get; set; }
        public string Fotografija { get; set; }
        public int ProdavacId { get; set; }
        public Korisnik Prodavac { get; set; }
        public List<Narudzbina> Narudzbine { get; set; }

    }
}

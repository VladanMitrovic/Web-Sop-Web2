using System.ComponentModel.DataAnnotations;

namespace web2projekat.Models
{
    public class Narudzbina
    {
        [Key]
        public long Id { get; set; }
        public Artikal Artikal { get; set; }
        public int ArtikalId { get; set; }
        public Korisnik Kupac { get; set; }
        public StanjeArtikla Status { get; set; }
        public string Adresa { get; set; }
        public int Kolicina { get; set; }
        public DateTime DatumPorucivanja { get; set; } 
        public DateTime DatumStizanja { get; set; }
    }
}

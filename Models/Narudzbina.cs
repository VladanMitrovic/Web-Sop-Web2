using System.ComponentModel.DataAnnotations;

namespace web2projekat.Models
{
    public class Narudzbina
    {

        public long Id { get; set; }
        public Artikal Artikal { get; set; }
        public string ImeArtikla { get; set; }
        public int PordavacId { get; set; }
        public string ImeKupca { get; set; }
        public string ImeProdavca { get; set; }
        public double Cena { get; set; }
        public string Komentar { get; set; }
        public int ArtikalId { get; set; }
        public Korisnik Kupac { get; set; }
        public int KupacId { get; set; }
        public StanjeArtikla Status { get; set; }
        public string Adresa { get; set; }
        public int Kolicina { get; set; }
        public DateTime DatumPorucivanja { get; set; } 
        public DateTime DatumStizanja { get; set; }
    }
}

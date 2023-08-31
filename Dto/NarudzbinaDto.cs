using web2projekat.Models;

namespace web2projekat.Dto
{
    public class NarudzbinaDto
    {
        public long Id { get; set; }
        public int ArtikalId { get; set; }
        public StanjeArtikla Status { get; set; }
        public string Adresa { get; set; }
        public int Kolicina { get; set; }
        public DateTime DatumPorucivanja { get; set; }
        public DateTime DatumStizanja { get; set; }
    }
}

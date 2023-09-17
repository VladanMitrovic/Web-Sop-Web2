using web2projekat.Models;

namespace web2projekat.Dto
{
    public class ArtikalDto
    {
       
        public int Id { get; set; }
        public int ProdavacId { get; set; }
        public string Naziv { get; set; }
        public float Cena { get; set; }
        public int Kolicina { get; set; }
        public string Opis { get; set; }
      
    }
}

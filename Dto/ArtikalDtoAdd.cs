namespace web2projekat.Dto
{
    public class ArtikalDtoAdd
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public double Cena { get; set; }
        public int Kolicina { get; set; }
        public string Opis { get; set; }
        public int ProdavacId { get; set; }
    }
}

//using System.Text.Json.Serialization;
using web2projekat.Models;

namespace web2projekat.Dto
{
    public class KorisnikRegistracija
    {
        //public int Id { get; set; }
        public string KorisnickoIme { get; set; }
        public string Email { get; set; }
        public string Lozinka { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string DatumRodjenja { get; set; }
        public string Adresa { get; set; }
        public TipKorisnika? TipKorisnika { get; set; }
        public string SlikaKorisnika { get; set; }
    }
}

﻿using System.ComponentModel.DataAnnotations;

namespace web2projekat.Models
{
    public class Korisnik
    {
       
        public int Id { get; set; }
        public string KorisnickoIme { get; set; }
        public string Email { get; set; }
        public string Lozinka { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string DatumRodjenja { get; set; }
        public string Adresa { get; set; }
        public VerifikacijaStatus? VerifikacijaStatus { get; set; } 
        public TipKorisnika TipKorisnika { get; set; }
        public string SlikaKorisnika { get; set;}
        public List<Narudzbina> Narudzbine { get; set; }
        public List<Artikal> Artikli { get; set; }

    }
}

using web2projekat.Dto;

namespace web2projekat.Interfaces
{
    public interface IKorisnikService
    {
        List<KorisnikDto> GetKorisnik();
        KorisnikDto GetById(int id);
        KorisnikDto AddKorisnik(KorisnikRegistracija newKorisnik);
        KorisnikDto UpdateKorisnik(int id, KorisnikUpdateDto newKorisnik);
       
        string UlogujSe(LoginZahtevDto loginZahtev);
        KorisnikDto EmailKorisnik(string email);
        void DeleteKorisnik(int id);
        KorisnikDto ProveriKorisnika(VerifikacijaOdgovorDto verifikacija);
    }
}

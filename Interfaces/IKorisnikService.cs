using web2projekat.Dto;

namespace web2projekat.Interfaces
{
    public interface IKorisnikService
    {
        List<KorisnikDto> GetKorisnik();
        KorisnikDto GetById(int id);
        KorisnikDto AddKorisnik(KorisnikRegistracija newKorisnik);
        KorisnikDto UpdateKorisnik(int id, KorisnikUpdateDto newKorisnik);
        LoginOdgovorDto UlogujSe(LoginZahtevDto loginZahtev);
        void DeleteKorisnik(int id);
    }
}

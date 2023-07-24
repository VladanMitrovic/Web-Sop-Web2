using web2projekat.Dto;

namespace web2projekat.Interfaces
{
    public interface IKorisnikService
    {
        List<KorisnikDto> GetKorisnik();
        KorisnikDto GetById(int id);
        KorisnikDto AddKorisnik(KorisnikDto newKorisnik);
        KorisnikDto UpdateKorisnik(int id, KorisnikDto newKorisnik);
        void DeleteKorisnik(int id);
    }
}

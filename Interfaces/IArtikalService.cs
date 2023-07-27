using web2projekat.Dto;
using web2projekat.ParametersForQuery;

namespace web2projekat.Interfaces
{
    public interface IArtikalService
    {
        List<ArtikalDto> GetArtikal(QueryZaArtikal query);
        ArtikalDto GetById(int id);
        ArtikalDto AddArtikal(ArtikalDtoAdd newArtikal, int id);
        ArtikalDto UpdateArtikal(int id,int idKorisnik, ArtikalDtoAdd newArtikal);
        ArtikalDto DeleteArtikal(int id, int korisnikId);
    }
}

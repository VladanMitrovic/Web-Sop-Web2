using web2projekat.Dto;

namespace web2projekat.Interfaces
{
    public interface IArtikalService
    {
        List<ArtikalDto> GetArtikal();
        ArtikalDto GetById(int id);
        ArtikalDto AddArtikal(ArtikalDto newArtikal, int id);
        ArtikalDto UpdateArtikal(int id, ArtikalDto newArtikal);
        void DeleteArtikal(int id);
    }
}

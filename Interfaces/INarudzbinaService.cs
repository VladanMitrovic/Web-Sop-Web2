using web2projekat.Dto;
using web2projekat.ParametersForQuery;

namespace web2projekat.Interfaces
{
    public interface INarudzbinaService
    {
        List<NarudzbinaDto> GetNarudzbina();
        NarudzbinaDto GetById(long id);
        NarudzbinaDto AddNarudzbina(NarudzbinaDto newNarudzbina, int userId);
        NarudzbinaDto UpdateNarudzbinak(NarudzbinaDto newNarudzbina, int id);
        void DeleteNarudzbina(long id);
    }
}

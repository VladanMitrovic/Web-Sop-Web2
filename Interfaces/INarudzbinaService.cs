using web2projekat.Dto;
using web2projekat.ParametersForQuery;

namespace web2projekat.Interfaces
{
    public interface INarudzbinaService
    {
        List<NarudzbinaDto> GetNarudzbina(QueryZaNarudzbinu query);
        NarudzbinaDto GetById(long id);
        NarudzbinaDto AddNarudzbina(NarudzbinaAddDto newNarudzbina, int id);
        NarudzbinaDto UpdateNarudzbinak(long id, NarudzbinaDto newNarudzbina);
        DeleteNarudzbinaDto DeleteNarudzbina(long id, int korisnikId);
    }
}

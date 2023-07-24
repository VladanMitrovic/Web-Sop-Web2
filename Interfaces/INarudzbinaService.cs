using web2projekat.Dto;

namespace web2projekat.Interfaces
{
    public interface INarudzbinaService
    {
        List<NarudzbinaDto> GetNarudzbina();
        NarudzbinaDto GetById(long id);
        NarudzbinaDto AddNarudzbina(NarudzbinaDto newNarudzbina, int id);
        NarudzbinaDto UpdateNarudzbinak(long id, NarudzbinaDto newNarudzbina);
        void DeleteNarudzbina(long id);
    }
}

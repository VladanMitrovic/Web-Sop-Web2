using web2projekat.Models;
using AutoMapper;
using web2projekat.Dto;

namespace web2projekat.Mapping
{
    public class MapiranjeProfila: Profile
    {
        public MapiranjeProfila()
        {
        
            CreateMap<Korisnik, KorisnikDto>().ReverseMap();
            CreateMap<Artikal, ArtikalDto>().ReverseMap();
            CreateMap<Narudzbina, NarudzbinaDto>().ReverseMap();
            CreateMap<Korisnik, KorisnikRegistracija>().ReverseMap();
            CreateMap<Artikal, ArtikalDtoAdd>().ReverseMap();
        }
    }
}

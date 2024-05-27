using TheGames.Publishers;
using TheGames.Games;
using AutoMapper;

namespace TheGames;

public class TheGamesApplicationAutoMapperProfile : Profile
{
    public TheGamesApplicationAutoMapperProfile()
    {
        CreateMap<Publisher, PublisherDto>();
        CreateMap<CreateUpdatePublisherDto, Publisher>();
        CreateMap<Game, GameDto>();
        CreateMap<CreateUpdateGameDto, Game>();
        CreateMap<Publisher, PublisherLookupDto>();
    }
}

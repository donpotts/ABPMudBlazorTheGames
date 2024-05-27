using TheGames.Publishers;
using TheGames.Games;
using AutoMapper;

namespace TheGames.Web;

public class TheGamesWebAutoMapperProfile : Profile
{
    public TheGamesWebAutoMapperProfile()
    {
        CreateMap<PublisherDto, CreateUpdatePublisherDto>();
        CreateMap<GameDto, CreateUpdateGameDto>();
        CreateMap<Pages.Games.CreateModalModel.CreateGameViewModel, CreateUpdateGameDto>();
        CreateMap<GameDto, Pages.Games.EditModalModel.EditGameViewModel>();
        CreateMap<Pages.Games.EditModalModel.EditGameViewModel, CreateUpdateGameDto>();
    }
}

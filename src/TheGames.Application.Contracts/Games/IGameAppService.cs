using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace TheGames.Games;

public interface IGameAppService :
    ICrudAppService< //Defines CRUD methods
        GameDto, //Used to show games
        int, //Primary key of the game entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateGameDto> //Used to create/update a game
{
    Task<ListResultDto<PublisherLookupDto>> GetPublisherLookupAsync();
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using TheGames.Publishers;
using TheGames.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace TheGames.Games;

[Authorize(TheGamesPermissions.Games.Default)]
public class GameAppService :
    CrudAppService<
        Game, //The Game entity
        GameDto, //Used to show games
        int, //Primary key of the game entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateGameDto>, //Used to create/update a game
    IGameAppService //implement the IGameAppService
{
    private readonly IRepository<Publisher, int> _publisherRepository;

    public GameAppService(
        IRepository<Game, int> repository,
        IRepository<Publisher, int> publisherRepository)
        : base(repository)
    {
        _publisherRepository = publisherRepository;
        GetPolicyName = TheGamesPermissions.Games.Default;
        GetListPolicyName = TheGamesPermissions.Games.Default;
        CreatePolicyName = TheGamesPermissions.Games.Create;
        UpdatePolicyName = TheGamesPermissions.Games.Edit;
        DeletePolicyName = TheGamesPermissions.Games.Delete;
    }

    public override async Task<GameDto> GetAsync(int id)
    {
        //Get the IQueryable<Game> from the repository
        var queryable = await Repository.GetQueryableAsync();

        //Prepare a query to join games and publishers
        var query = from game in queryable
            join publisher in await _publisherRepository.GetQueryableAsync() on game.PublisherId equals publisher.Id
            where game.Id == id
            select new { game, publisher };

        //Execute the query and get the game with publisher
        var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
        if (queryResult == null)
        {
            throw new EntityNotFoundException(typeof(Game), id);
        }

        var gameDto = ObjectMapper.Map<Game, GameDto>(queryResult.game);
        gameDto.PublisherName = queryResult.publisher.Name;
        return gameDto;
    }

    public override async Task<PagedResultDto<GameDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        //Get the IQueryable<Game> from the repository
        var queryable = await Repository.GetQueryableAsync();

        //Prepare a query to join games and publishers
        var query = from game in queryable
            join publisher in await _publisherRepository.GetQueryableAsync() on game.PublisherId equals publisher.Id
            select new {game, publisher};

        //Paging
        query = query
            .OrderBy(NormalizeSorting(input.Sorting))
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);

        //Execute the query and get a list
        var queryResult = await AsyncExecuter.ToListAsync(query);

        //Convert the query result to a list of GameDto objects
        var gameDtos = queryResult.Select(x =>
        {
            var gameDto = ObjectMapper.Map<Game, GameDto>(x.game);
            gameDto.PublisherName = x.publisher.Name;
            return gameDto;
        }).ToList();

        //Get the total count with another query
        var totalCount = await Repository.GetCountAsync();

        return new PagedResultDto<GameDto>(
            totalCount,
            gameDtos
        );
    }

    public async Task<ListResultDto<PublisherLookupDto>> GetPublisherLookupAsync()
    {
        var publishers = await _publisherRepository.GetListAsync();

        return new ListResultDto<PublisherLookupDto>(
            ObjectMapper.Map<List<Publisher>, List<PublisherLookupDto>>(publishers)
        );
    }

    private static string NormalizeSorting(string? sorting)
    {
        if (sorting.IsNullOrEmpty())
        {
            return $"game.{nameof(Game.Name)}";
        }

        if (sorting.Contains("publisherName", StringComparison.OrdinalIgnoreCase))
        {
            return sorting.Replace(
                "publisherName",
                "Publisher.Name",
                StringComparison.OrdinalIgnoreCase
            );
        }

        return $"game.{sorting}";
    }
}

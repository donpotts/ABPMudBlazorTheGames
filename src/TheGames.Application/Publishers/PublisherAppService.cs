using System;
using TheGames.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace TheGames.Publishers;

public class PublisherAppService :
    CrudAppService<
        Publisher, //The Publisher entity
        PublisherDto, //Used to show publishers
        int, //Primary key of the publisher entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdatePublisherDto>, //Used to create/update a publisher
    IPublisherAppService //implement the IPublisherAppService
{
    public PublisherAppService(IRepository<Publisher, int> repository)
        : base(repository)
    {
        GetPolicyName = TheGamesPermissions.Publishers.Default;
        GetListPolicyName = TheGamesPermissions.Publishers.Default;
        CreatePolicyName = TheGamesPermissions.Publishers.Create;
        UpdatePolicyName = TheGamesPermissions.Publishers.Edit;
        DeletePolicyName = TheGamesPermissions.Publishers.Delete;
    }
}

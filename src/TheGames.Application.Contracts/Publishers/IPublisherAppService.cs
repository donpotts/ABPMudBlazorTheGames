using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace TheGames.Publishers;

public interface IPublisherAppService :
    ICrudAppService< //Defines CRUD methods
        PublisherDto, //Used to show publishers
        int, //Primary key of the publisher entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdatePublisherDto> //Used to create/update a publisher
{
}

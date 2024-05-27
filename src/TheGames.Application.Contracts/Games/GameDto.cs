using System;
using Volo.Abp.Application.Dtos;

namespace TheGames.Games;

public class GameDto : AuditedEntityDto<int>
{
    public string? Name { get; set; }

    public string? Developer { get; set; }

    public int PublisherId { get; set; }

    public string? PublisherName { get; set; }
}

using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace TheGames.Games;

public class Game : AuditedAggregateRoot<int>
{
    public string? Name { get; set; }

    public string? Developer { get; set; }

    public int PublisherId { get; set; }
}

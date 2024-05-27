using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace TheGames.Publishers;

public class Publisher : AuditedAggregateRoot<int>
{
    public string? Name { get; set; }

    public string? Country { get; set; }

    public int GameId { get; set; }
}

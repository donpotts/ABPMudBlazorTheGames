using System;
using Volo.Abp.Application.Dtos;

namespace TheGames.Publishers;

public class PublisherDto : AuditedEntityDto<int>
{
    public string? Name { get; set; }

    public string? Country { get; set; }
}

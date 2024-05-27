using System;
using Volo.Abp.Application.Dtos;

namespace TheGames.Games;

public class PublisherLookupDto : EntityDto<int>
{
	public string? Name { get; set; }
}

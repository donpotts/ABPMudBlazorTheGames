using System;

namespace TheGames.Games;

public class CreateUpdateGameDto
{
    public string? Name { get; set; }

    public string? Developer { get; set; }

    public int PublisherId { get; set; }
}

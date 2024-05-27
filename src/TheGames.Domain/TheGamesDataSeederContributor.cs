using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheGames.Publishers;
using TheGames.Games;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace TheGames;

public class TheGamesDataSeederContributor
    : IDataSeedContributor, ITransientDependency
{
    private readonly IRepository<Publisher, int> _publisherRepository;
    private readonly IRepository<Game, int> _gameRepository;

    public TheGamesDataSeederContributor(
        IRepository<Publisher, int> publisherRepository,
        IRepository<Game, int> gameRepository)
    {
        _publisherRepository = publisherRepository;
        _gameRepository = gameRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        Dictionary<int, Publisher> publishers = [];

        if (await _publisherRepository.GetCountAsync() <= 0)
        {
            publishers[1] = await _publisherRepository.InsertAsync(
                new Publisher
                {
                    Name = "Quantum Void",
                    Country = "United States",
                },
                autoSave: true
            );
            publishers[2] = await _publisherRepository.InsertAsync(
                new Publisher
                {
                    Name = "Stellar Nebula",
                    Country = "Japan",
                },
                autoSave: true
            );
            publishers[3] = await _publisherRepository.InsertAsync(
                new Publisher
                {
                    Name = "Galactic Pulse",
                    Country = "United States",
                },
                autoSave: true
            );
            publishers[4] = await _publisherRepository.InsertAsync(
                new Publisher
                {
                    Name = "Orbit Dynamics",
                    Country = "United States",
                },
                autoSave: true
            );
            publishers[5] = await _publisherRepository.InsertAsync(
                new Publisher
                {
                    Name = "Nova Silica",
                    Country = "Sweden",
                },
                autoSave: true
            );
            publishers[6] = await _publisherRepository.InsertAsync(
                new Publisher
                {
                    Name = "Comet Cascade",
                    Country = "United States",
                },
                autoSave: true
            );
            publishers[7] = await _publisherRepository.InsertAsync(
                new Publisher
                {
                    Name = "Plasma Pioneer",
                    Country = "Poland",
                },
                autoSave: true
            );
            publishers[8] = await _publisherRepository.InsertAsync(
                new Publisher
                {
                    Name = "Starlight Synapse",
                    Country = "United States",
                },
                autoSave: true
            );
            publishers[9] = await _publisherRepository.InsertAsync(
                new Publisher
                {
                    Name = "Electron Eclipse",
                    Country = "United States",
                },
                autoSave: true
            );
        }

        Dictionary<int, Game> games = [];

        if (await _gameRepository.GetCountAsync() <= 0)
        {
            games[1] = await _gameRepository.InsertAsync(
                new Game
                {
                    Name = "Quantum Conquest: AI Odyssey",
                    Developer = "Infinity Nexus",
                    PublisherId = 1,
                },
                autoSave: true
            );
            games[2] = await _gameRepository.InsertAsync(
                new Game
                {
                    Name = "The Legend of Andromeda: Breath of the Cosmos",
                    Developer = "AI Nebula",
                    PublisherId = 2,
                },
                autoSave: true
            );
            games[3] = await _gameRepository.InsertAsync(
                new Game
                {
                    Name = "Red Dwarf Revelation: AI Genesis",
                    Developer = "Galactic Pulse AI",
                    PublisherId = 3,
                },
                autoSave: true
            );
            games[4] = await _gameRepository.InsertAsync(
                new Game
                {
                    Name = "Orbit Odyssey: AI Frontier",
                    Developer = "Orbit Dynamics AI",
                    PublisherId = 4,
                },
                autoSave: true
            );
            games[5] = await _gameRepository.InsertAsync(
                new Game
                {
                    Name = "Asteroid Architect: AI Expedition",
                    Developer = "Nova Silica AI",
                    PublisherId = 5,
                },
                autoSave: true
            );
            games[6] = await _gameRepository.InsertAsync(
                new Game
                {
                    Name = "Overdrive Orion: AI Revolution",
                    Developer = "Comet Cascade AI",
                    PublisherId = 6,
                },
                autoSave: true
            );
            games[7] = await _gameRepository.InsertAsync(
                new Game
                {
                    Name = "The Ghost Whisperer 3: AI Horizon",
                    Developer = "Plasma Pioneer AI",
                    PublisherId = 7,
                },
                autoSave: true
            );
            games[8] = await _gameRepository.InsertAsync(
                new Game
                {
                    Name = "Gravity Gambit: AI Velocity",
                    Developer = "Galactic Pulse North",
                    PublisherId = 3,
                },
                autoSave: true
            );
            games[9] = await _gameRepository.InsertAsync(
                new Game
                {
                    Name = "Counter Striker: Galactic Genesis",
                    Developer = "Starlight Synapse AI",
                    PublisherId = 8,
                },
                autoSave: true
            );
            games[10] = await _gameRepository.InsertAsync(
                new Game
                {
                    Name = "Apex Ascension: AI Evolution",
                    Developer = "Respawn Nexus",
                    PublisherId = 9,
                },
                autoSave: true
            );
            games[11] = await _gameRepository.InsertAsync(
                new Game
                {
                    Name = "Soccer Smash 2024: AI Sports",
                    Developer = "Electron Eclipse Sports",
                    PublisherId = 9,
                },
                autoSave: true
            );
            games[12] = await _gameRepository.InsertAsync(
                new Game
                {
                    Name = "Football Nebula: AI Gridiron",
                    Developer = "Electron Eclipse Gridiron",
                    PublisherId = 9,
                },
                autoSave: true
            );
            games[13] = await _gameRepository.InsertAsync(
                new Game
                {
                    Name = "Battlefield Beyond: AI Warfare",
                    Developer = "DICE Nexus",
                    PublisherId = 9,
                },
                autoSave: true
            );
            games[14] = await _gameRepository.InsertAsync(
                new Game
                {
                    Name = "Cybernetic Cosmos: AI Utopia",
                    Developer = "Plasma Pioneer AI",
                    PublisherId = 7,
                },
                autoSave: true
            );
            games[15] = await _gameRepository.InsertAsync(
                new Game
                {
                    Name = "Rocket Resonance: AI Velocity",
                    Developer = "Psyonix Nexus",
                    PublisherId = 4,
                },
                autoSave: true
            );
        }
    }
}

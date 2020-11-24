using Chickencoop.DataAccess;
using Chickencoop.Models.Models;
using Chickencoop.Repositories.Repositories;
using Chickencoop.Tests.LobbyTests.TestCases;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static Chickencoop.Models.Enums.GamesEnum;

namespace Chickencoop.Tests.LobbyTests
{
    public class LobbyRepositoryTests : InMemoryDatabase
    {
        private readonly ChickencoopContext _context;

        public LobbyRepositoryTests()
        {
            _context = new ChickencoopContext(DbOptions);
        }

        [Theory]
        [MemberData(nameof(LobbyRepositoryTestCase.LobbyArgumentExceptionIndex), MemberType = typeof(LobbyRepositoryTestCase))]
        public async void Lobby_ThrowsArgumentException(int i)
        {
            var input = LobbyRepositoryTestCase.LobbyArgumentExceptionTestCase[i];
            string title = (string)input[0];
            Games gn = (Games)input[1];

            var player = await InitiatePlayer("Mateusz");
            var opponent = await InitiatePlayer("Lukasz");

            var repository = new LobbyRepository(_context);

            Lobby lobby = new Lobby
            {
                Title = title,
                GameName = gn,
                PlayerOneId = player.Id,
                PlayerTwoId = opponent.Id,
            };

            Func<Task> act = async () => await repository.Create(lobby);

            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public async void NoPlayerIndatabase_ThrowsArgumentNullException()
        {
            var repository = new LobbyRepository(_context);

            var playerTwo = await InitiatePlayer("Szymon");

            Lobby lobby = new Lobby
            {
                Title = "title",
                GameName = 0,
                PlayerOneId = new Guid(),
                PlayerTwoId = playerTwo.Id
            };

            Func<Task> act = async () => await repository.Create(lobby);

            act.Should().Throw<ArgumentNullException>().WithMessage("Player with this id doesn't exist");
        }

        [Fact]
        public async void PlayerIdsAreEqual_ThrowsArgumentException()
        {
            var repository = new LobbyRepository(_context);

            var player = await InitiatePlayer("Szymon");

            Lobby lobby = new Lobby
            {
                Title = "title",
                GameName = 0,
                PlayerOneId = player.Id,
                PlayerTwoId = player.Id
            };

            Func<Task> act = async () => await repository.Create(lobby);

            act.Should().Throw<ArgumentException>().WithMessage("Second players Id can't be the same as the first player Id");

        }


        private async Task<Player> InitiatePlayer(string nickname)
        {
            Player player = new Player
            {
                Nickname = nickname
            };

            await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();

            return player;

        }
    }
}

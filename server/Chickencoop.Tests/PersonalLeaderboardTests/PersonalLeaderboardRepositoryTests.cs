using Chickencoop.DataAccess;
using Chickencoop.Models.Models;
using Chickencoop.Repositories.Repositories;
using Chickencoop.Tests.PersonalLeaderboardTests.TestCases;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static Chickencoop.Models.Enums.GamesEnum;
using static Chickencoop.Models.Enums.ResultTypeEnum;

namespace Chickencoop.Tests.PersonalLeaderboardTests
{
    public class PersonalLeaderboardRepositoryTests: InMemoryDatabase
    {
        private readonly ChickencoopContext _context;

        public PersonalLeaderboardRepositoryTests()
        {
            _context = new ChickencoopContext(DbOptions);
        }

        private readonly TimeSpan correctTs = new TimeSpan(0, 5, 0);
        private readonly DateTime correctDt = DateTime.Today;
        private readonly ResultType correctRt = 0;
        private readonly Games correctGn= 0;

        [Fact]
        public async void CreatePersonalLeaderboard_Succesful()
        {
            var player = await InitiatePlayer("Szymon");
            var player2 = await InitiatePlayer("Michal");

            var repository = new PersonalLeaderboardRepository(_context);

            PersonalLeaderboard testCreate = new PersonalLeaderboard
            {
                GameTime = correctTs,
                GameDate = correctDt,
                PlayerId = player.Id,
                OpponentId = player2.Id,
                Result = correctRt,
                GameName = correctGn
            };

            await repository.Create(testCreate);

            var get = await repository.GetAll();

            var actual = await repository.Get(testCreate.Id);
            var expected = testCreate;

            actual.Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(PersonalLeadrboardRepositoryTestCase.LeaderboardArgumentExceptionIndex), MemberType = typeof(PersonalLeadrboardRepositoryTestCase))]
        public async void ThrowsArgumentException(int i)
        {
            var input = PersonalLeadrboardRepositoryTestCase.LeaderboardArgumentExceptionTestCase[i];
            TimeSpan ts = (TimeSpan)input[0];
            DateTime dt = (DateTime)input[1];
            ResultType rt = (ResultType)input[2];
            Games gn = (Games)input[3];

            var player = await InitiatePlayer("Szymon");
            var player2 = await InitiatePlayer("Michal");

            var repository = new PersonalLeaderboardRepository(_context);

            PersonalLeaderboard testCreate = new PersonalLeaderboard
            {
                GameTime = ts,
                GameDate = dt,
                PlayerId = player.Id,
                OpponentId = player2.Id,
                Result = rt,
                GameName = gn
            };

            Func<Task> act = async () => await repository.Create(testCreate);

            act.Should().Throw<ArgumentException>();
        }

        [Fact]
       // [MemberData(nameof(RepositoryTestCase.LeaderboardNullReferenceExceptionIndex), MemberType = typeof(RepositoryTestCase))]
        public async void WrongPlayerId_ThrowsNullReferenceException()
        {
            var player = await InitiatePlayer("Szymon");
            var player2 = await InitiatePlayer("Michal");

            var repository = new PersonalLeaderboardRepository(_context);

            PersonalLeaderboard testCreate = new PersonalLeaderboard
            {
                GameTime = correctTs,
                GameDate = correctDt,
                PlayerId = new Guid(),
                OpponentId = player2.Id,
                Result = correctRt,
                GameName = correctGn
            };
            Func<Task> act = async () => await repository.Create(testCreate);

            act.Should().Throw<NullReferenceException>().WithMessage("Player with this id doesn't exist");
        }
        
        [Fact]
        public async void PlayerAndOpponentSameId_ThrowsArgumentException()
        {
            var player = await InitiatePlayer("Szymon");
            var player2 = await InitiatePlayer("Michal");

            var repository = new PersonalLeaderboardRepository(_context);

            PersonalLeaderboard testCreate = new PersonalLeaderboard
            {
                GameTime = correctTs,
                GameDate = correctDt,
                PlayerId = player.Id,
                OpponentId = player.Id,
                Result = correctRt,
                GameName = correctGn
            };

            Func<Task> act = async () => await repository.Create(testCreate);

            act.Should().Throw<ArgumentException>().WithMessage("Opponents Id can't be the same as the players Id");
        }

#region initiate
        
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

#endregion
    }
}

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
using static Chickencoop.Models.Enums.ResultTypeEnum;

namespace Chickencoop.Tests.PlayerTests
{
    public class PersonalLeaderboardRepositoryTests: InMemoryDatabase
    {
        [Fact]
        public async void CreatePersonalLeaderboard_Succesful()
        {
            var context = new ChickencoopContext(DbOptions);

            Player player = new Player
            {
                Nickname = "Szymon"
            };
            
            Player player2 = new Player
            {
                Nickname = "Michal"
            };

            await context.AddAsync(player);
            await context.AddAsync(player2);
            await context.SaveChangesAsync();

            var repository = new PersonalLeaderboardRepository(context);

            PersonalLeaderboard testCreate = new PersonalLeaderboard
            {
                GameTime = new TimeSpan(0,2,0),
                GameDate = DateTime.Today,
                PlayerId = player.Id,
                OpponentId = player2.Id,
                Result = 0
            };

            await repository.Create(testCreate);
            var actual = await repository.Get(testCreate.Id);
            var expected = testCreate;

            actual.Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(RepositoryTestCase.LeaderboardArgumentExceptionIndex), MemberType = typeof(RepositoryTestCase))]
        public async void ThrowsArgumentException(int i)
        {
            var input = RepositoryTestCase.LeaderboardArgumentExceptionTestCase[i];
            TimeSpan ts = (TimeSpan)input[0];
            DateTime dt = (DateTime)input[1];
            ResultType rt = (ResultType)input[2];

            var context = new ChickencoopContext(DbOptions);

            Player player = new Player
            {
                Nickname = "Szymon"
            };

            Player player2 = new Player
            {
                Nickname = "Michal"
            };

            await context.AddAsync(player);
            await context.AddAsync(player2);
            await context.SaveChangesAsync();

            var repository = new PersonalLeaderboardRepository(context);

            PersonalLeaderboard testCreate = new PersonalLeaderboard
            {
                GameTime = ts,
                GameDate = dt,
                PlayerId = player.Id,
                OpponentId = player2.Id,
                Result = rt
            };

            Func<Task> act = async () => await repository.Create(testCreate);

            act.Should().Throw<ArgumentException>();
        }

        
    }
}

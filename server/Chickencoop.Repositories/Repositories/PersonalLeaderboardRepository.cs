using Chickencoop.DataAccess;
using Chickencoop.Models.Models;
using Chickencoop.Repositories.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Chickencoop.Models.Enums.GamesEnum;
using static Chickencoop.Models.Enums.ResultTypeEnum;

namespace Chickencoop.Repositories.Repositories
{
    public class PersonalLeaderboardRepository : IPersonalLeaderboardRepository
    {
        private readonly ChickencoopContext _context;

        public PersonalLeaderboardRepository(ChickencoopContext context)
        {
            _context = context;
        }
        public async Task<List<PersonalLeaderboard>> GetAll()
        {
            return await _context.PersonalLeaderboards.ToListAsync();
        }

        public async Task<List<PersonalLeaderboard>> GetAllByPlayer(Guid playerId)
        {
            DoesPlayerExists(playerId);

            return await _context.PersonalLeaderboards.Where(pl => pl.PlayerId == playerId).ToListAsync();
        }

        public async Task<PersonalLeaderboard> Get(Guid id)
        {
            return await DoesLeaderboardExists(id);
        }

        public async Task<bool> Create(PersonalLeaderboard personalLeaderboard)
        {
            Tests(personalLeaderboard);

            await _context.PersonalLeaderboards.AddAsync(personalLeaderboard);
            var created = await _context.SaveChangesAsync();

            return created > 0;
        }
        public async Task<bool> Update(PersonalLeaderboard personalLeaderboard)
        {
            Tests(personalLeaderboard);

            _context.PersonalLeaderboards.Update(personalLeaderboard);
            var updated = await _context.SaveChangesAsync();

            return updated > 0;
        }

        public async Task<bool> Delete(Guid id)
        {
            var record = await DoesLeaderboardExists(id);
            _context.Remove(record);
            var deleted = await _context.SaveChangesAsync();

            return deleted > 0;

        }



    #region tests
        private void Tests(PersonalLeaderboard personalLeaderboard)
        {
            DoesPlayerExists(personalLeaderboard.PlayerId);
            DoesPlayerExists(personalLeaderboard.OpponentId);
            DoesResultEnumExists(personalLeaderboard.Result);
            DoesGameNameEnumExists(personalLeaderboard.GameName);
            IsPlayerSameAsOpponent(personalLeaderboard.PlayerId, personalLeaderboard.OpponentId);
            IsGameDateToday(personalLeaderboard.GameDate);
            IsGameTimeLargerThanZero(personalLeaderboard.GameTime);
        }

        private async Task<PersonalLeaderboard> DoesLeaderboardExists(Guid id)
        {
            var leaderboard = await _context.PersonalLeaderboards.FirstOrDefaultAsync(pl => pl.Id == id);

            if (leaderboard == null)
                throw new NullReferenceException("Record with this id doesn't exist");

            return leaderboard;
        }

        private void DoesPlayerExists(Guid id)
        {
            var player = _context.Players.FirstOrDefault(pl => pl.Id == id);

            if(player == null)
                throw new NullReferenceException("Player with this id doesn't exist");
        }

        private void DoesResultEnumExists(ResultType results)
        {
            if (!Enum.IsDefined(typeof(ResultType), results))
                throw new ArgumentException("Wrong ResultType");
        }
        
        private void DoesGameNameEnumExists(Games games)
        {
            if (!Enum.IsDefined(typeof(Games), games))
                throw new ArgumentException("Wrong GameName");
        }

        private void IsPlayerSameAsOpponent(Guid playerId, Guid opponentId)
        {
            if (playerId == opponentId)
                throw new ArgumentException("Opponents Id can't be the same as the players Id");
        }

        private void IsGameDateToday(DateTime dateTime)
        {
            if (dateTime != DateTime.Today)
                throw new ArgumentException("Game Date can't be different than today");
        }

        private void IsGameTimeLargerThanZero(TimeSpan timeSpan)
        {
            if (timeSpan < TimeSpan.Zero)
                throw new ArgumentException("Game Time can't be smaller than 0");
        }
        #endregion tests
    }
}

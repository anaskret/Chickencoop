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

        public async Task<PersonalLeaderboard> Get(Guid id)
        {
            try
            {
                var get = await _context.PersonalLeaderboards.FirstOrDefaultAsync(pl => pl.Id == id);

                return get;
            }
            catch
            {
                 throw new NullReferenceException("Record with this id doesn't exist");
            }
        }

        public async Task<bool> Create(PersonalLeaderboard personalLeaderboard)
        {
            try
            {
                var player = await _context.PersonalLeaderboards.FirstOrDefaultAsync(p => p.PlayerId == personalLeaderboard.PlayerId);

            }
            catch
            { 
                throw new NullReferenceException("Selected player doesn't exist"); 
            }
            try
            {
                var opponent = await _context.PersonalLeaderboards.FirstOrDefaultAsync(p => p.OpponentId == personalLeaderboard.OpponentId);


            }
            catch
            { 
                throw new NullReferenceException("Selected opponent doesn't exist"); 
            }

            if (personalLeaderboard.GameDate != DateTime.Today)
                throw new ArgumentException("Game Date can't be different than today");

            if (personalLeaderboard.GameTime < TimeSpan.Zero)
                throw new ArgumentException("Game Time can't be smaller than 0");

            if (personalLeaderboard.OpponentId == personalLeaderboard.PlayerId)
                throw new ArgumentException("Opponents Id can't be the same as the players Id");

            if (!Enum.IsDefined(typeof(ResultType), personalLeaderboard.Result))
                throw new ArgumentException("Wrong Result Type");

            await _context.PersonalLeaderboards.AddAsync(personalLeaderboard);
            var created = await _context.SaveChangesAsync();

            return created > 0;
        }
        public async Task<bool> Update(PersonalLeaderboard personalLeaderboard)
        {
            try
            {
                var record = await _context.PersonalLeaderboards.FirstOrDefaultAsync(pl => pl.Id == personalLeaderboard.Id);
            }
            catch
            {
                    throw new NullReferenceException("Selected record doesn't exist");
            }
            try
            {
                var player = await _context.PersonalLeaderboards.FirstOrDefaultAsync(p => p.PlayerId == personalLeaderboard.PlayerId);
            }
            catch
            {
                throw new NullReferenceException("Selected player doesn't exist");
            }
            try
            {
                var opponent = await _context.PersonalLeaderboards.FirstOrDefaultAsync(p => p.OpponentId == personalLeaderboard.OpponentId);
            }
            catch
            {
                throw new NullReferenceException("Selected opponent doesn't exist");
            }

            if (personalLeaderboard.GameDate != DateTime.Today)
                throw new ArgumentException("Game Date can't be different than today");

            if (personalLeaderboard.GameTime < TimeSpan.Zero)
                throw new ArgumentException("Game Time can't be smaller than 0");

            if (personalLeaderboard.OpponentId == personalLeaderboard.PlayerId)
                throw new ArgumentException("Opponents Id can't be the same as the players Id");

            if (!Enum.IsDefined(typeof(ResultType), personalLeaderboard.Result))
                throw new ArgumentException("Wrong Result Type");

            _context.PersonalLeaderboards.Update(personalLeaderboard);
            var updated = await _context.SaveChangesAsync();

            return updated > 0;
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var record = await _context.PersonalLeaderboards.FirstOrDefaultAsync(pl => pl.Id == id);
                _context.Remove(record);
                var deleted = await _context.SaveChangesAsync();

                return deleted > 0;
            }
            catch
            {
                throw new NullReferenceException("Selected record doesn't exist");
            }

        }
    }
}

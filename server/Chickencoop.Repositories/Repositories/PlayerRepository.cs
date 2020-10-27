using Chickencoop.DataAccess;
using Chickencoop.Models.Models;
using Chickencoop.Repositories.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Chickencoop.Repositories.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly ChickencoopContext _context;
        public PlayerRepository(ChickencoopContext context)
        {
            _context = context;
        }

        public async Task<List<Player>> GetAll()
        {
            return await _context.Players.ToListAsync();
        }

        public async Task<Player> Get(Guid id)
        {
            try
            {
                var get = await _context.Players.FirstOrDefaultAsync(p => p.Id == id);

                return get;
            }
            catch
            {
                throw new NullReferenceException("Player with that Id doesn't exist");
            }
        }

        public async Task<bool> Create(Player player)
        {
            if (player.Nickname.Length < 3 || player.Nickname.Length > 20)
                throw new ArgumentException("Player nickname can't be shorter than 3 characters and longer than 20 characters");

            await _context.AddAsync(player);

            var created = await _context.SaveChangesAsync();

            return created > 0;
        }

        public async Task<bool> Update(Player player)
        {
            try
            {
                if (player.Nickname.Length < 3 && player.Nickname.Length > 20)
                    throw new ArgumentException("Player nickname can't be shorter than 3 characters and longer than 20 characters");

                var update = await _context.Players.FirstOrDefaultAsync(p => p.Id == player.Id);

                _context.Players.Update(update);

                var updated = await _context.SaveChangesAsync();

                return updated > 0;
            }
            catch(NullReferenceException)
            {
                throw new NullReferenceException("Player with that Id doesn't exist");
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var delete = await _context.Players.FirstOrDefaultAsync(p => p.Id == id);

                _context.Players.Remove(delete);

                var deleted = await _context.SaveChangesAsync();

                return deleted > 0;
            }
            catch
            {
                throw new NullReferenceException("Player with that Id doesn't exist");
            }
        }
    }
}

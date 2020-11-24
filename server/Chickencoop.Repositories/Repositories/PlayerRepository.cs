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

        public async Task<Player> Get(string username)
        {  
            return await DoesPlayerExists(username);
        }
        
        public async Task<Player> GetById(Guid id)
        {  
            return await DoesPlayerExistsById(id);
        }

        public async Task<bool> Create(Player player)
        {
            await _context.AddAsync(player);

            var created = await _context.SaveChangesAsync();

            return created > 0;
        }

        public async Task<bool> Update(Player player)
        {
            await DoesPlayerExistsById(player.Id);

            _context.Players.Update(player);

            var updated = await _context.SaveChangesAsync();

            return updated > 0;
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

#region tests
        private async Task<Player> DoesPlayerExists(string username)
        {
            var player = await _context.Players.FirstOrDefaultAsync(pl => pl.Nickname == username);
            
            if(player == null)
                throw new ArgumentNullException("Player with this username doesn't exist");

            return player;
        }
        
        private async Task<Player> DoesPlayerExistsById(Guid id)
        {
            var player = await _context.Players.AsNoTracking().FirstOrDefaultAsync(pl => pl.Id == id);

            if (player == null)
                throw new ArgumentNullException("Player with this id doesn't exist");

            return player;
        }

#endregion tests
    }
}

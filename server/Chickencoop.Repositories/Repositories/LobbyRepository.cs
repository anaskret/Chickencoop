using Chickencoop.DataAccess;
using Chickencoop.Models.Models;
using Chickencoop.Repositories.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Chickencoop.Models.Enums.GamesEnum;

namespace Chickencoop.Repositories.Repositories
{
    public class LobbyRepository : ILobbyRepository
    {
        private readonly ChickencoopContext _context;

        public LobbyRepository(ChickencoopContext context)
        {
            _context = context;
        }

        public async Task<List<Lobby>> GetAll()
        {
            return await _context.Lobbies.ToListAsync();

        }

        public async Task<Lobby> Get(Guid id)
        {
            try
            {
                var get = await _context.Lobbies.FirstOrDefaultAsync(pl => pl.Id == id);

                return get;
            }
            catch
            {
                throw new NullReferenceException("Record with this id doesn't exist");
            }
        }

        
        public async Task<bool> Create(Lobby lobby)
        {
            try
            {
                var playerOne = await _context.Lobbies.FirstOrDefaultAsync(p => p.PlayerOneId == lobby.PlayerOneId);

            }
            catch
            {
                throw new NullReferenceException("Selected playerOne doesn't exist");
            }
            if (lobby.PlayerTwoId != null)
            {
                try
                {
                    var playerTwo = await _context.Lobbies.FirstOrDefaultAsync(p => p.PlayerTwoId == lobby.PlayerTwoId);
                }
                catch
                {
                    throw new NullReferenceException("Selected playerTwo doesn't exist");
                }
            }
            if (!Enum.IsDefined(typeof(Games), lobby.GameName))
                throw new ArgumentException("Wrong GameName");

            await _context.Lobbies.AddAsync(lobby);
            var created = await _context.SaveChangesAsync();

            return created > 0;
        }
        public async Task<bool> Update(Lobby lobby)
        {
            try
            {
                var doesLobbbyExist = await _context.Lobbies.FirstOrDefaultAsync(l => l.Id == lobby.Id);
            }
            catch
            {
                throw new NullReferenceException("Selected lobby doesn't exist");
            }
            try
            {
                var playerOne = await _context.Lobbies.FirstOrDefaultAsync(p => p.PlayerOneId == lobby.PlayerOneId);

            }
            catch
            {
                throw new NullReferenceException("Selected playerOne doesn't exist");
            }
            if (lobby.PlayerTwoId != null)
            {
                try
                {
                    var playerTwo = await _context.Lobbies.FirstOrDefaultAsync(p => p.PlayerTwoId == lobby.PlayerTwoId);
                }
                catch
                {
                    throw new NullReferenceException("Selected playerTwo doesn't exist");
                }
            }
            if (!Enum.IsDefined(typeof(Games), lobby.GameName))
                throw new ArgumentException("Wrong GameName");

            _context.Lobbies.Update(lobby);
            var updated = await _context.SaveChangesAsync();

            return updated > 0;
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var doesLobbbyExist = await _context.Lobbies.FirstOrDefaultAsync(l => l.Id == id);
                _context.Remove(doesLobbbyExist);
                var deleted = await _context.SaveChangesAsync();

                return deleted > 0;
            }
            catch
            {
                throw new NullReferenceException("Selected lobby doesn't exist");
            }
        }
    }
}

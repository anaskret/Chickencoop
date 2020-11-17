using Chickencoop.DataAccess;
using Chickencoop.Models.Models;
using Chickencoop.Repositories.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            DoesLobbyExists(id);
            return await _context.Lobbies.FirstOrDefaultAsync(pl => pl.Id == id);
        }

        
        public async Task<bool> Create(Lobby lobby)
        {
            DoesPlayerExists(lobby.PlayerOneId);
            DoesEnumExists(lobby.GameName);
            IsPlayerSameAsOpponent(lobby);

            if (lobby.PlayerTwoId != null)
            {
                DoesPlayerExists(lobby.PlayerTwoId);
            }

            await _context.Lobbies.AddAsync(lobby);
            var created = await _context.SaveChangesAsync();

            return created > 0;
        }
        public async Task<bool> Update(Lobby lobby)
        {
            //DoesLobbyExists(lobby.Id);
            DoesPlayerExists(lobby.PlayerOneId);
            DoesEnumExists(lobby.GameName);
            IsPlayerSameAsOpponent(lobby);

            if (lobby.PlayerTwoId != null)
            {
                DoesPlayerExists(lobby.PlayerTwoId);
            }

            _context.Lobbies.Update(lobby);
            var updated = await _context.SaveChangesAsync();

            return updated > 0;
        }

        public async Task<bool> Delete(Guid id)
        {
            DoesLobbyExists(id);
            var doesLobbbyExist = await _context.Lobbies.FirstOrDefaultAsync(pl => pl.Id == id);
            _context.Remove(doesLobbbyExist);
            var deleted = await _context.SaveChangesAsync();

            return deleted > 0;
        }



        #region tests
        
        private void DoesLobbyExists(Guid id)
        {
            var lobby = _context.Lobbies.FirstOrDefault(pl => pl.Id == id);
            if (lobby == null)
                throw new NullReferenceException("Lobby with this id doesn't exist");
        }
        
        private void DoesPlayerExists(Guid? id)
        {
            var player = _context.Players.FirstOrDefault(pl => pl.Id == id);
            if (player == null)
                throw new NullReferenceException("Player with this id doesn't exist");
        }

        private void DoesEnumExists(Games games)
        {
            if (!Enum.IsDefined(typeof(Games), games))
                throw new ArgumentException("Wrong GameName");
        }

        private void IsPlayerSameAsOpponent(Lobby lobby)
        {
            if (lobby.PlayerOneId == lobby.PlayerTwoId)
                throw new ArgumentException("Opponents Id can't be the same as the players Id");
        }
        #endregion tests
    }
}

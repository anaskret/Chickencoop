using Chickencoop.DataAccess;
using Chickencoop.Models.Models;
using Chickencoop.Repositories.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            
            return await DoesLobbyExists(id);
        }

        
        public async Task<bool> Create(Lobby lobby)
        {
            IsTitleCorrect(lobby.Title);
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
            await DoesLobbyExists(lobby.Id);
            IsTitleCorrect(lobby.Title);
            DoesPlayerExists(lobby.PlayerOneId);
            if(lobby.PlayerTwoId != null)
                DoesPlayerExists(lobby.PlayerTwoId);
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
            await DoesLobbyExists(id);
            var doesLobbbyExist = await _context.Lobbies.FirstOrDefaultAsync(pl => pl.Id == id);
            _context.Remove(doesLobbbyExist);
            var deleted = await _context.SaveChangesAsync();

            return deleted > 0;
        }

        #region tests
        
        private async Task<Lobby> DoesLobbyExists(Guid id)
        {
            var lobby = await _context.Lobbies.AsNoTracking().FirstOrDefaultAsync(pl => pl.Id == id);

            if (lobby == null)
                throw new ArgumentNullException("Lobby with this id doesn't exist");

            return lobby;
        }
        
        private void DoesPlayerExists(Guid? id)
        {
            var player = _context.Players.FirstOrDefault(pl => pl.Id == id);

            if (player == null)
                throw new ArgumentNullException("Player with this id doesn't exist");
        }
        private void IsTitleCorrect(string title)
        {
            if (title.Length < 2 || title.Length > 50 || string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title has to be longer than 3 characters and shorter than 50");
        }

        private void DoesEnumExists(Games games)
        {
            if (!Enum.IsDefined(typeof(Games), games))
                throw new ArgumentException("Wrong GameName");
        }
        

        private void IsPlayerSameAsOpponent(Lobby lobby)
        {
            if (lobby.PlayerOneId == lobby.PlayerTwoId)
                throw new ArgumentException("Second players Id can't be the same as the first player Id");
        }
        #endregion tests
    }
}

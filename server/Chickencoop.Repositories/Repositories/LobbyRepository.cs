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
            return await DoesLobbyExist(id);
        }

        
        public async Task<bool> Create(Lobby lobby)
        {
            await DoesLobbyExist(lobby.PlayerOneId);
            DoesEnumExists(lobby.GameName);

            if (lobby.PlayerTwoId != null)
            {
                await DoesLobbyExist(lobby.PlayerTwoId);
            }

            await _context.Lobbies.AddAsync(lobby);
            var created = await _context.SaveChangesAsync();

            return created > 0;
        }
        public async Task<bool> Update(Lobby lobby)
        {
            await DoesLobbyExist(lobby.Id);
            await DoesLobbyExist(lobby.PlayerOneId);
            DoesEnumExists(lobby.GameName);

            if (lobby.PlayerTwoId != null)
            {
                await DoesLobbyExist(lobby.PlayerTwoId);
            }


            _context.Lobbies.Update(lobby);
            var updated = await _context.SaveChangesAsync();

            return updated > 0;
        }

        public async Task<bool> Delete(Guid id)
        {
            var doesLobbbyExist = await DoesLobbyExist(id);
            _context.Remove(doesLobbbyExist);
            var deleted = await _context.SaveChangesAsync();

            return deleted > 0;
        }



        #region tests
        private async void Tests(Lobby lobby)
        {

        }
        private async Task<Lobby> DoesLobbyExist(Guid? id)
        {
            try
            {
                return await _context.Lobbies.FirstOrDefaultAsync(pl => pl.Id == id);
            }
            catch(NullReferenceException)
            {
                throw new NullReferenceException("Lobby with this id doesn't exist");
            }
        }

        private bool DoesEnumExists(Games games)
        {
            if (!Enum.IsDefined(typeof(Games), games))
                throw new ArgumentException("Wrong GameName");

            return true;
        }
        #endregion tests
    }
}

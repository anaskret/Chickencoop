using Amazon.CognitoIdentityProvider.Model;
using AutoMapper;
using Chickencoop.Models.Dtos.CreateDtos;
using Chickencoop.Models.Dtos.GetDtos;
using Chickencoop.Models.Dtos.UpdateDtos;
using Chickencoop.Models.Models;
using Chickencoop.Repositories.Repositories.Interfaces;
using Chickencoop.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chickencoop.Services.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IMapper _mapper;

        public PlayerService(IPlayerRepository playerRepository, IMapper mapper)
        {
            _playerRepository = playerRepository;
            _mapper = mapper;
        }

        public async Task<List<GetPlayerDto>> GetAllPlayers()
        {
            var getAll = await _playerRepository.GetAll();
            return getAll.Select(p => _mapper.Map<GetPlayerDto>(p)).ToList();
        }

        public async Task<GetPlayerDto> GetPlayer(string username)
        {
            return _mapper.Map<GetPlayerDto>(await _playerRepository.Get(username));
        }
        
        public async Task<GetPlayerDto> GetPlayerById(Guid id)
        {
            return _mapper.Map<GetPlayerDto>(await _playerRepository.GetById(id));
        }

        public async Task<Player> CreatePlayer(CreatePlayerDto createPlayerDto)
        {
            Player mappedPlayer = _mapper.Map<Player>(createPlayerDto);
            var created = await _playerRepository.Create(mappedPlayer);

            if (!created)
                throw new Exception("Failed to create the Player");

            return mappedPlayer;
        }
        public async Task<bool> UpdatePlayer(UpdatePlayerDto updatePlayerDto)
        {
            var updated = await _playerRepository.Update(_mapper.Map<Player>(updatePlayerDto));

            if (!updated)
                throw new Exception("Failed to update the Player");

            return updated;
        }

        public async Task<bool> DeletePlayer(Guid id)
        {
            var deleted = await _playerRepository.Delete(id);

            if (!deleted)
                throw new Exception("Failed to delete the Player");

            return deleted;
        }

        //method for adding users to the database if Cognito User Database is different from the app db
        public async Task<bool> CheckPlayers(ListUsersResponse listUsers) 
        {
            var getAll = await GetAllPlayers();

            var missingUsers = CompareUsersInDatabases(getAll, listUsers);

            if (missingUsers.Count() == 0)
                return true;

            foreach (var user in missingUsers)
            {
                CreatePlayerDto createPlayer = new CreatePlayerDto
                {
                    Nickname = user
                };

                if (!await _playerRepository.Create(_mapper.Map<Player>(createPlayer)))
                {
                    return false;
                }
            }
            return true;
        }

        //method compares users in both databases and returns the ones that are missing from app db
        //if a player is present in app database but not in Cognito db, he is removed from the app database
        private List<string> CompareUsersInDatabases(List<GetPlayerDto> players, ListUsersResponse listUsers)
        {
            List<string> usernames = new List<string>();
            
            foreach(var user in listUsers.Users)
            { 
                usernames.Add(user.Username);
            }
            foreach(var player in players)
            {
                if(usernames.Contains(player.Nickname))
                {
                    usernames.Remove(player.Nickname);
                }
                else
                {
                    usernames.Remove(player.Nickname);
                    _playerRepository.Delete(player.Id);
                }
            }

            return usernames;
        }

    }
}

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

        public async Task<GetPlayerDto> GetPlayer(Guid id)
        {
            try
            {
                return _mapper.Map<GetPlayerDto>(await _playerRepository.Get(id));
            }
            catch
            {
                throw;
            }
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
            try
            {
                var updated = await _playerRepository.Update(_mapper.Map<Player>(updatePlayerDto));

                if (!updated)
                    throw new Exception("Failed to update the Player");

                return updated;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DeletePlayer(Guid id)
        {
            try
            {
                var deleted = await _playerRepository.Delete(id);

                if (!deleted)
                    throw new Exception("Failed to delete the Player");

                return deleted;
            }
            catch
            {
                throw;
            }
        }
    }
}

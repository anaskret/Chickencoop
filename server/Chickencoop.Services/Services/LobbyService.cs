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
    public class LobbyService : ILobbyService
    {
        private readonly ILobbyRepository _lobbyRepository;
        private readonly IMapper _mapper;

        public LobbyService(ILobbyRepository lobbyRepository, IMapper mapper)
        {
            _lobbyRepository = lobbyRepository;
            _mapper = mapper;
        }

        public async Task<List<GetLobbyDto>> GetAllLobbies()
        {
            var getAll = await _lobbyRepository.GetAll();
            return getAll.Select(l => _mapper.Map<GetLobbyDto>(l)).ToList();
        }

        public async Task<GetLobbyDto> GetLobby(Guid id)
        {
            return _mapper.Map<GetLobbyDto>(await _lobbyRepository.Get(id));
        }

        public async Task<Lobby> CreateLobby(CreateLobbyDto createLobby)
        {
            Lobby mappedLobby = _mapper.Map<Lobby>(createLobby);
            var created = await _lobbyRepository.Create(mappedLobby);

            if (!created)
                throw new Exception("Failed to create the lobby");

            return mappedLobby;
        }
        public async Task<bool> UpdateLobby(UpdateLobbyDto updateLobby)
        {
            var updated = await _lobbyRepository.Update(_mapper.Map<Lobby>(updateLobby));

            if (!updated)
                throw new Exception("Failed to update the lobby");

            return updated;
        }

        public async Task<bool> DeleteLobby(Guid id)
        {
            var deleted = await _lobbyRepository.Delete(id);

            if (!deleted)
                throw new Exception("Failed to delete the lobby");

            return deleted;
        }

    }
}

using Chickencoop.Models.Dtos.CreateDtos;
using Chickencoop.Models.Dtos.GetDtos;
using Chickencoop.Models.Dtos.UpdateDtos;
using Chickencoop.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Chickencoop.Services.Services.Interfaces
{
    public interface ILobbyService
    {
        Task<List<GetLobbyDto>> GetAllLobbies();
        Task<GetLobbyDto> GetLobby(Guid id);
        Task<Lobby> CreateLobby(CreateLobbyDto createLobby);
        Task<bool> UpdateLobby(UpdateLobbyDto updateLobby);
        Task<bool> DeleteLobby(Guid id);
    }
}

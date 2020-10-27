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
    public interface IPlayerService
    {
        Task<List<GetPlayerDto>> GetAllPlayers();
        Task<GetPlayerDto> GetPlayer(Guid id);
        Task<Player> CreatePlayer(CreatePlayerDto createPlayerDto);
        Task<bool> UpdatePlayer(UpdatePlayerDto updatePlayerDto);
        Task<bool> DeletePlayer(Guid id);
    }
}

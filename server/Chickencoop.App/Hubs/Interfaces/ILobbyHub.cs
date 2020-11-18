using Chickencoop.Models.Dtos.UpdateDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chickencoop.App.Hubs.Interfaces
{
    public interface ILobbyHub
    {
        Task JoinLobby(Guid lobbyId);
        Task LeaveLobby(Guid lobbyId);
        Task NewPlayer(Guid lobbyId, Guid playerId);
        Task LobbyChange();
        Task HostChange(Guid lobbyId);
    }
}

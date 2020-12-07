using Chickencoop.App.Hubs.Interfaces;
using Chickencoop.Models.Dtos.UpdateDtos;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chickencoop.App.Hubs
{
    public class LobbyHub : Hub<ILobbyHub>
    {
        public async Task JoinLobby(Guid lobbyId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, lobbyId.ToString());
        }

        public async Task LeaveLobby(Guid lobbyId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, lobbyId.ToString());
        }

        public async Task NewPlayer(Guid lobbyId, Guid playerId)
        {
            await Clients.Group(lobbyId.ToString()).NewPlayer(lobbyId, playerId);
            await LobbyChange();
        }

        public async Task LobbyChange()
        {
            await Clients.All.LobbyChange();
        }

        public async Task HostChange(Guid lobbyId)
        {
            await Clients.OthersInGroup(lobbyId.ToString()).HostChange(lobbyId);
            await LobbyChange();
        }

        public async Task NewGame(Guid lobbyId)
        {
            await Clients.OthersInGroup(lobbyId.ToString()).NewGame(lobbyId);
            //await LobbyChange();
        }

        public async Task OpponentLeft(Guid lobbyId)
        {
            await Clients.OthersInGroup(lobbyId.ToString()).OpponentLeft(lobbyId);
            await LobbyChange();
        }
        
        public async Task GameAccepted(Guid lobbyId)
        {
            await Clients.Group(lobbyId.ToString()).GameAccepted(lobbyId);
        }
    }
}

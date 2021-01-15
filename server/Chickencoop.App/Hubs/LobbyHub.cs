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
        public async Task JoinLobby(Guid lobbyId) //adds user to the group
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, lobbyId.ToString());
        }

        public async Task LeaveLobby(Guid lobbyId) //removes user from the group
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, lobbyId.ToString());
        }

        public async Task NewPlayer(Guid lobbyId, Guid playerId) //sends message that a new player has joined the lobby
        {
            await Clients.Group(lobbyId.ToString()).NewPlayer(lobbyId, playerId);
            await LobbyChange();
        }

        public async Task LobbyChange() //sends message that a new game was created or that a new player joined or left a lobby
        {
            await Clients.All.LobbyChange();
        }

        public async Task HostChange(Guid lobbyId) //sends message to other player in the game that the host has left
        {
            await Clients.OthersInGroup(lobbyId.ToString()).HostChange(lobbyId);
            await LobbyChange();
        }

        public async Task NewGame(Guid lobbyId) //message to start a new game
        {
            await Clients.OthersInGroup(lobbyId.ToString()).NewGame(lobbyId);
            //yawait LobbyChange();
        }

        public async Task OpponentLeft(Guid lobbyId) //sends a message that opponent left the lobby
        {
            await Clients.OthersInGroup(lobbyId.ToString()).OpponentLeft(lobbyId);
            await LobbyChange();
        }
        
        public async Task GameAccepted(Guid lobbyId) //sends a signal that the new game request was accepted
        {
            await Clients.Group(lobbyId.ToString()).GameAccepted(lobbyId);
        }
    }
}

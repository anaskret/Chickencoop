﻿using Chickencoop.App.Hubs.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chickencoop.App.Hubs
{
    public class LobbyHub: Hub<ILobbyHub>
    {
        public async Task JoinLobby(Guid lobbyId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, lobbyId.ToString());
        }

        public async Task LeaveLobby(Guid lobbyId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, lobbyId.ToString());
        }

        public async Task NewPlayer(Guid? playerId, Guid lobbyId)
        {
            await Clients.Group(lobbyId.ToString()).NewPlayer(playerId, lobbyId);
        }

        public async Task LobbyChange() 
        {
            await Clients.All.LobbyChange();
        }
    }
}

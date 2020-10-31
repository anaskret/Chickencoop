using Chickencoop.App.Hubs.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chickencoop.App.Hubs
{
    public class TicTacToeHub : Hub<ITicTacToeHub>
    {
        private string turn = "x";
        public async Task TurnChange(int x, int y, string player, Guid lobbyId)
        {
            if (player == "x")
                turn = "o";
            else
                turn = "x";

            await Clients.Caller.TurnChange(x, y, turn, lobbyId);
            await Clients.All.TurnChange(x, y, turn, lobbyId);
        }

        public async Task Victory(string player, Guid lobbyId)
        {
            await Clients.All.Victory(player, lobbyId);
        }
    }
}

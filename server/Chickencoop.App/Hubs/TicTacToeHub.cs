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
        public async Task TurnChange(int x, int y, string player)
        {
            await Clients.Others.TurnChange(x, y, player);
        }
    }
}

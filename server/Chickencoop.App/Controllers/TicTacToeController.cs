using Chickencoop.App.Hubs;
using Chickencoop.App.Hubs.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chickencoop.App.Controllers
{
    public class TicTacToeController: Controller
    {
        private string[,] board = new string[3, 3] { { "", "", "" }, { "", "", "" }, { "", "", "" } };
        private readonly IHubContext<TicTacToeHub, ITicTacToeHub> hubContext;

        public TicTacToeController(IHubContext<TicTacToeHub, ITicTacToeHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        [HttpPatch(ApiRoutes.TicTacToe.TurnChange)]
        public async Task<IActionResult> TurnChange(int x, int y, string player)
        {
            board[x, y] = player;

            /*if (player == "x")
                player = "o";
            else
                player = "x";*/

            await this.hubContext
                .Clients
                .All
                .OpponentTurnEnd(x, y, player);

            return new JsonResult(board);
        }
    }
}

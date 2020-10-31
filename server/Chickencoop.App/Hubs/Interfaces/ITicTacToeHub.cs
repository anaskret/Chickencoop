using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chickencoop.App.Hubs.Interfaces
{
    public interface ITicTacToeHub
    {
        Task TurnChange(int x, int y, string player, Guid lobbyId);
        Task Victory(string player, Guid lobbyId);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chickencoop.App.Hubs.Interfaces
{
    public interface ITicTacToeHub
    {
        Task TurnChange(int x, int y, string player);
        Task Victory(string player);
        Task JoinLobby(Guid lobbyId);
        Task LeaveLobby(Guid lobbyId);

    }
}

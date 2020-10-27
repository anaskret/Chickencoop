using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chickencoop.App.Hubs.Interfaces
{
    public interface ITicTacToeHub
    {
        Task BoardstateChange(string[,] board, string player);
    }
}

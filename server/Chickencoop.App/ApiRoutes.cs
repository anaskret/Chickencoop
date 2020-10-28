using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chickencoop.App
{
    public class ApiRoutes
    {
        private const string Root = "api";

        public static class Players
        {
            public const string Create = Root + "/players";
            public const string GetAll = Root + "/players";
            public const string Get = Root + "/players/{id:guid}";
            public const string Update = Root + "/players/{id:guid}";
            public const string Delete = Root + "/players/{id:guid}";
        }
        
        public static class PersonalLeaderboard
        {
            public const string Create = Root + "/records";
            public const string GetAll = Root + "/records";
            public const string Get = Root + "/records/{id:guid}";
            public const string Update = Root + "/records/{id:guid}";
            public const string Delete = Root + "/records/{id:guid}";
        }

        public static class TicTacToe
        {
            public const string TurnChange = Root + "/tictactoe";
        }
    }
}

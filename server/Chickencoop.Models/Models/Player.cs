using System;
using System.Collections.Generic;
using System.Text;

namespace Chickencoop.Models.Models
{
    public class Player
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; }
        public bool IsOnline { get; set; } = false;

        public virtual ICollection<PersonalLeaderboard> PersonalLeaderboards { get; set; }
        public virtual ICollection<Lobby> Lobbies { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Chickencoop.Models.Models
{
    public class Player
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; }

        public virtual ICollection<PersonalLeaderboard> PersonalLeaderboards { get; set; }
    }
}

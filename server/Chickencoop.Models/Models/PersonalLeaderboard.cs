using System;
using System.Collections.Generic;
using System.Text;
using static Chickencoop.Models.Enums.GamesEnum;
using static Chickencoop.Models.Enums.ResultTypeEnum;

namespace Chickencoop.Models.Models
{
    public class PersonalLeaderboard
    {
        public Guid Id { get; set; }
        public TimeSpan GameTime { get; set; }
        public DateTime GameDate { get; set; }
        public Guid PlayerId { get; set; }
        public Guid OpponentId { get; set; }
        public ResultType Result { get; set; }
        public Games GameName { get; set; }

        public virtual Player Player { get; set; }
    }
}

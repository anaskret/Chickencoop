using System;
using System.Collections.Generic;
using System.Text;
using static Chickencoop.Models.Enums.GamesEnum;
using static Chickencoop.Models.Enums.ResultTypeEnum;

namespace Chickencoop.Models.Dtos.IDtos
{
    public abstract class PersonalLeaderboardDto
    {
        public TimeSpan GameTime { get; set; }
        public DateTime GameDate { get; set; }
        public Guid PlayerId { get; set; }
        public Guid OpponentId { get; set; }
        public ResultType Result { get; set; }
        public Games GameName{ get; set; }
    }
}

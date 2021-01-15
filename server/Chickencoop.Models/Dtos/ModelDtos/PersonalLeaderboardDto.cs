using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;
using static Chickencoop.Models.Enums.GamesEnum;
using static Chickencoop.Models.Enums.ResultTypeEnum;

namespace Chickencoop.Models.Dtos.IDtos
{
    public abstract class PersonalLeaderboardDto //base leaderboardDto from which other dtos inherit
    {
        public TimeSpan GameTime { get; set; }
        public DateTime GameDate { get; set; }
        public Guid PlayerId { get; set; }
        public Guid OpponentId { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public ResultType Result { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Games GameName{ get; set; }
    }
}

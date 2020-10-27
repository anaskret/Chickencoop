using Chickencoop.Models.Dtos.IDtos;
using System;
using System.Collections.Generic;
using System.Text;
using static Chickencoop.Models.Enums.ResultTypeEnum;

namespace Chickencoop.Models.Dtos.GetDtos
{
    public class GetPersonalLeaderboardDto : IPersonalLeaderboardDto
    {
        public Guid Id { get; set; }
        public TimeSpan GameTime { get; set; }
        public DateTime GameDate { get; set; }
        public Guid PlayerId { get; set; }
        public Guid OpponentId { get; set; }
        public ResultType ResultType { get; set; }
    }
}

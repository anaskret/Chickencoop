using Chickencoop.Models.Dtos.IDtos;
using System;
using System.Collections.Generic;
using System.Text;
using static Chickencoop.Models.Enums.ResultTypeEnum;

namespace Chickencoop.Models.Dtos.CreateDtos
{
    public class CreatePersonalLeaderboardDto : IPersonalLeaderboardDto
    {
        public TimeSpan GameTime { get; set; }
        public DateTime GameDate { get; set; }
        public Guid PlayerId { get; set; }
        public Guid OpponentId { get; set; }
        public ResultType ResultType { get; set; }
    }
}

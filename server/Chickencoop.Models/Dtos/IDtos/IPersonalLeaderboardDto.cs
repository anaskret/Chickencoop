using System;
using System.Collections.Generic;
using System.Text;
using static Chickencoop.Models.Enums.ResultTypeEnum;

namespace Chickencoop.Models.Dtos.IDtos
{
    public interface IPersonalLeaderboardDto
    {
        TimeSpan GameTime { get; set; }
        DateTime GameDate { get; set; }
        Guid PlayerId { get; set; }
        Guid OpponentId { get; set; }
        ResultType ResultType { get; set; }
    }
}

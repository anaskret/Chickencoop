using Chickencoop.Models.Dtos.IDtos;
using System;
using System.Collections.Generic;
using System.Text;
using static Chickencoop.Models.Enums.ResultTypeEnum;

namespace Chickencoop.Models.Dtos.GetDtos
{
    public class GetPersonalLeaderboardDto : PersonalLeaderboardDto
    {
        public Guid Id { get; set; }
    }
}

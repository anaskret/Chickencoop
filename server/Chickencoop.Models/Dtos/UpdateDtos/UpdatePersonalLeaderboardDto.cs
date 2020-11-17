using Chickencoop.Models.Dtos.IDtos;
using System;
using System.Collections.Generic;
using System.Text;
using static Chickencoop.Models.Enums.ResultTypeEnum;

namespace Chickencoop.Models.Dtos.UpdateDtos
{
    public class UpdatePersonalLeaderboardDto: PersonalLeaderboardDto
    {
        public Guid Id { get; set; }
    }
}

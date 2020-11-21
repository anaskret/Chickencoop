using System;
using System.Collections.Generic;
using System.Text;

namespace Chickencoop.Models.Dtos.GetDtos
{
    public class GetPersonalLeaderboardWithNicknameDto: GetPersonalLeaderboardDto
    {
        public string OpponentNickname { get; set; }
    }
}

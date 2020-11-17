using Chickencoop.Models.Dtos.IDtos;
using Chickencoop.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using static Chickencoop.Models.Enums.GamesEnum;

namespace Chickencoop.Models.Dtos.GetDtos
{
    public class GetLobbyDto : LobbyDto
    {
        public Guid Id { get; set; }
    }
}

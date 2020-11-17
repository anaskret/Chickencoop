using Chickencoop.Models.Dtos.IDtos;
using System;
using System.Collections.Generic;
using System.Text;
using static Chickencoop.Models.Enums.GamesEnum;

namespace Chickencoop.Models.Dtos.UpdateDtos
{
    public class UpdateLobbyDto:LobbyDto
    {
        public Guid Id { get; set; }
    }
}

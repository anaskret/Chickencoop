using Chickencoop.Models.Dtos.IDtos;
using Chickencoop.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using static Chickencoop.Models.Enums.GamesEnum;

namespace Chickencoop.Models.Dtos.GetDtos
{
    public class GetLobbyDto : ILobbyDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Games GameName { get; set; }
        public Guid PlayerOneId { get; set; }
        public Guid? PlayerTwoId { get; set; }
    }
}

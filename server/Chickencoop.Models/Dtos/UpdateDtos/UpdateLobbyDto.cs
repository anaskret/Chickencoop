﻿using Chickencoop.Models.Dtos.IDtos;
using System;
using System.Collections.Generic;
using System.Text;
using static Chickencoop.Models.Enums.GamesEnum;

namespace Chickencoop.Models.Dtos.UpdateDtos
{
    public class UpdateLobbyDto:ILobbyDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Games GameName { get; set; }
        public Guid PlayerOneId { get; set; }
        public Guid? PlayerTwoId { get; set; }
    }
}

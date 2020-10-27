using Chickencoop.Models.Dtos.BaseDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chickencoop.Models.Dtos.CreateDtos
{
    public class CreatePlayerDto : IPlayerDto
    {
        public string Nickname { get; set; }
    }
}

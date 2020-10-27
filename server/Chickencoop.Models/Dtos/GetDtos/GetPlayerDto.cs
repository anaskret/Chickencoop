using Chickencoop.Models.Dtos.BaseDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chickencoop.Models.Dtos.GetDtos
{
    public class GetPlayerDto : IPlayerDto
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; }
    }
}

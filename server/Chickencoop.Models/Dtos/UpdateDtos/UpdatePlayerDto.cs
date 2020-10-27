using Chickencoop.Models.Dtos.BaseDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chickencoop.Models.Dtos.UpdateDtos
{
    public class UpdatePlayerDto : IPlayerDto
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; }
    }
}

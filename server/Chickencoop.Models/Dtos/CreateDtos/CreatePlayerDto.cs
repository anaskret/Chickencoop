using Chickencoop.Models.Dtos.BaseDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chickencoop.Models.Dtos.CreateDtos
{
    public class CreatePlayerDto : IPlayerDto
    {
        [StringLength(20, MinimumLength = 3)]
        public string Nickname { get; set; }
    }
}

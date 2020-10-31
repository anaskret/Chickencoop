using Chickencoop.Models.Dtos.BaseDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chickencoop.Models.Dtos.UpdateDtos
{
    public class UpdatePlayerDto : IPlayerDto
    {
        public Guid Id { get; set; }
        [StringLength(20, MinimumLength = 3)]
        public string Nickname { get; set; }
    }
}

using Chickencoop.Models.Dtos.BaseDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chickencoop.Models.Dtos.UpdateDtos
{
    public class UpdatePlayerDto : PlayerDto
    {
        public Guid Id { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chickencoop.Models.Dtos.BaseDtos
{
    public abstract class PlayerDto
    {
        [StringLength(20, MinimumLength = 3)]
        public string Nickname { get; set; }
    }
}

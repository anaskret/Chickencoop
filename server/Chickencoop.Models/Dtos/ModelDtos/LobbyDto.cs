using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static Chickencoop.Models.Enums.GamesEnum;

namespace Chickencoop.Models.Dtos.IDtos
{
    public abstract class LobbyDto
    {
        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }
        public Games GameName{ get; set; }
        public Guid PlayerOneId{ get; set; }
        public Guid? PlayerTwoId{ get; set; }
        public bool IsFull { get; set; }
    }
}

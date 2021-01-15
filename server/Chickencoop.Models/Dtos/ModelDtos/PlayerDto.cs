using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chickencoop.Models.Dtos.BaseDtos
{
    public abstract class PlayerDto //base playerDto from which other dtos inherit
    {
        [StringLength(20, MinimumLength = 3)]
        public string Nickname { get; set; }
        public string AvatarUrl { get; set; } = "https://cdn.vuetifyjs.com/images/profiles/marcus.jpg";
        public string BackgroundUrl { get; set; } = "https://cdn.vuetifyjs.com/images/cards/server-room.jpg";
        public bool IsOnline { get; set; }
    }
}

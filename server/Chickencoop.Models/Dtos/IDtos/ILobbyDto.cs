using System;
using System.Collections.Generic;
using System.Text;
using static Chickencoop.Models.Enums.GamesEnum;

namespace Chickencoop.Models.Dtos.IDtos
{
    public interface ILobbyDto
    {
        string Title { get; set; }
        Games GameName{ get; set; }
        Guid PlayerOneId{ get; set; }
        Guid? PlayerTwoId{ get; set; }
        bool isFull { get; set; }
    }
}

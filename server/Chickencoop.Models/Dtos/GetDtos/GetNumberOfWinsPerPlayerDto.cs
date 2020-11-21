using System;
using System.Collections.Generic;
using System.Text;

namespace Chickencoop.Models.Dtos.GetDtos
{
    public class GetNumberOfWinsPerPlayerDto
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; }
        public int Wins { get; set; }
        public bool IsOnline { get; set; }
    }
}

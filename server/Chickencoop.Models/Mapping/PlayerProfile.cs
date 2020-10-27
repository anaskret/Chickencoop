using AutoMapper;
using Chickencoop.Models.Dtos.BaseDtos;
using Chickencoop.Models.Dtos.CreateDtos;
using Chickencoop.Models.Dtos.GetDtos;
using Chickencoop.Models.Dtos.UpdateDtos;
using Chickencoop.Models.Models;
using System.Collections.Generic;

namespace Chickencoop.Services.Mapping
{
    public class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            CreateMap<Player, CreatePlayerDto>();
            CreateMap<Player, GetPlayerDto>();
            CreateMap<Player, UpdatePlayerDto>();
            CreateMap<IPlayerDto, Player>()
                .ForMember(d => d.Id, opt => opt.Ignore());
            CreateMap<List<Player>, List<IPlayerDto>>();
        }
    }
}

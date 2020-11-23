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
            CreateMap<CreatePlayerDto, Player>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Lobbies, opt => opt.Ignore())
                .ForMember(dest => dest.PersonalLeaderboards, opt => opt.Ignore());
            CreateMap<Player, GetPlayerDto>();
            CreateMap<UpdatePlayerDto, Player>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Lobbies, opt => opt.Ignore())
                .ForMember(dest => dest.PersonalLeaderboards, opt => opt.Ignore());
            CreateMap<List<Player>, List<PlayerDto>>();
        }
    }
}

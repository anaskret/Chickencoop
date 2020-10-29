using AutoMapper;
using Chickencoop.Models.Dtos.CreateDtos;
using Chickencoop.Models.Dtos.GetDtos;
using Chickencoop.Models.Dtos.IDtos;
using Chickencoop.Models.Dtos.UpdateDtos;
using Chickencoop.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chickencoop.Models.Mapping
{
    public class LobbyProfile: Profile
    {
        public LobbyProfile()
        {
            CreateMap<Lobby, CreateLobbyDto>();
            CreateMap<Lobby, GetLobbyDto>();
            CreateMap<Lobby, UpdateLobbyDto>();
            CreateMap<ILobbyDto, Lobby>()
                .ForMember(d => d.Id, opt => opt.Ignore());
        }
    }
}

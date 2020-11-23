﻿using AutoMapper;
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
            CreateMap<CreateLobbyDto, Lobby>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Player, opt => opt.Ignore());
            CreateMap<Lobby, GetLobbyDto>();
            CreateMap<UpdateLobbyDto, Lobby>()
                .ForMember(dest => dest.Player, opt => opt.Ignore());

        }
    }
}

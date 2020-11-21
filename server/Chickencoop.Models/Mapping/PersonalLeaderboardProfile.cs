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
    public class PersonalLeaderboardProfile : Profile
    {
        public PersonalLeaderboardProfile()
        {
            CreateMap<CreatePersonalLeaderboardDto, PersonalLeaderboard>();
            CreateMap<PersonalLeaderboard, GetPersonalLeaderboardDto>();
            CreateMap<PersonalLeaderboard, GetPersonalLeaderboardWithNicknameDto>()
                .ForMember(dest => dest.OpponentNickname, opt => opt.Ignore());
            CreateMap<UpdatePersonalLeaderboardDto, PersonalLeaderboard>();
        }
    }
}

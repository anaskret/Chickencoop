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
            CreateMap<PersonalLeaderboard, CreatePersonalLeaderboardDto>();
            CreateMap<PersonalLeaderboard, GetPersonalLeaderboardDto>();
            CreateMap<PersonalLeaderboard, UpdatePersonalLeaderboardDto>();
            CreateMap<IPersonalLeaderboardDto, PersonalLeaderboard>()
                .ForMember(d => d.Id, opt => opt.Ignore());
        }
    }
}

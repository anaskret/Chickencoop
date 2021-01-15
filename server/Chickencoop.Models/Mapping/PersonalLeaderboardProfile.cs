using AutoMapper;
using Chickencoop.Models.Dtos.CreateDtos;
using Chickencoop.Models.Dtos.GetDtos;
using Chickencoop.Models.Dtos.IDtos;
using Chickencoop.Models.Dtos.UpdateDtos;
using Chickencoop.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using static Chickencoop.Models.Enums.ResultTypeEnum;

namespace Chickencoop.Models.Mapping
{
    public class PersonalLeaderboardProfile : Profile
    {
        public PersonalLeaderboardProfile() //Leaderboard mapping
        {
            CreateMap<CreatePersonalLeaderboardDto, PersonalLeaderboard>() //map from CreateDto to Model should ignore the Id and the relation
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Player, opt => opt.Ignore());
            CreateMap<PersonalLeaderboard, GetPersonalLeaderboardDto>();
            CreateMap<PersonalLeaderboard, GetPersonalLeaderboardWithNicknameDto>() //map from Model to GetDto should ignore the relation
                .ForMember(dest => dest.OpponentNickname, opt => opt.Ignore());
            CreateMap<UpdatePersonalLeaderboardDto, PersonalLeaderboard>() //map from UpdateDto to Model should ignore the relation
                .ForMember(dest => dest.Player, opt => opt.Ignore());
        }
    }
}

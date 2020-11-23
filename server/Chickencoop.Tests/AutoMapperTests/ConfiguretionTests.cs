using AutoMapper;
using Chickencoop.Models.Mapping;
using Chickencoop.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Chickencoop.Tests.AutoMapperTests
{
    public class ConfiguretionTests
    {
        [Fact]
        public void AutoMapper_Configuration_IsValid()
        {
            var config = new MapperConfiguration(cfg => 
            {
                cfg.AddProfile<PersonalLeaderboardProfile>();
                cfg.AddProfile<LobbyProfile>();
                cfg.AddProfile<PlayerProfile>();
            });

            config.AssertConfigurationIsValid();
        }
    }
}

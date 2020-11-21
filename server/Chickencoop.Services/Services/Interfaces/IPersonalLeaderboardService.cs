using Chickencoop.Models.Dtos.CreateDtos;
using Chickencoop.Models.Dtos.GetDtos;
using Chickencoop.Models.Dtos.UpdateDtos;
using Chickencoop.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Chickencoop.Services.Services.Interfaces
{
    public interface IPersonalLeaderboardService
    {
        Task<List<GetPersonalLeaderboardDto>> GetAllRecords();
        Task<List<GetPersonalLeaderboardWithNicknameDto>> GetAllRecordsByPlayer(Guid playerId);
        Task<List<GetNumberOfWinsPerPlayerDto>> GetRanking();
        Task<GetPersonalLeaderboardDto> GetRecord(Guid id);
        Task<PersonalLeaderboard> CreateRecord(CreatePersonalLeaderboardDto createPersonalLeaderboard);
        Task<bool> UpdateRecord(UpdatePersonalLeaderboardDto updatePersonalLeaderboard);
        Task<bool> DeleteRecord(Guid id);
    }
}

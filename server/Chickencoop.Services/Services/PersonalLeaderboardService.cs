using AutoMapper;
using Chickencoop.Models.Dtos.CreateDtos;
using Chickencoop.Models.Dtos.GetDtos;
using Chickencoop.Models.Dtos.UpdateDtos;
using Chickencoop.Models.Models;
using Chickencoop.Repositories.Repositories.Interfaces;
using Chickencoop.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chickencoop.Services.Services
{
    public class PersonalLeaderboardService : IPersonalLeaderboardService
    {
        private readonly IPersonalLeaderboardRepository _repository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IMapper _mapper;
        public PersonalLeaderboardService(IMapper mapper, IPersonalLeaderboardRepository repository, IPlayerRepository playerRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _playerRepository = playerRepository;
        }
        public async Task<List<GetPersonalLeaderboardDto>> GetAllRecords()
        {
            var getAll = await _repository.GetAll();
            return getAll.Select(pl => _mapper.Map<GetPersonalLeaderboardDto>(pl)).ToList();
        }

        public async Task<List<GetPersonalLeaderboardWithNicknameDto>> GetAllRecordsByPlayer(Guid playerId)
        {
            var getAll = await _repository.GetAllByPlayer(playerId);
            return getAll.Select(pl => _mapper.Map<GetPersonalLeaderboardWithNicknameDto>(pl, opt => 
                opt.AfterMap((src, dest) => dest.OpponentNickname = _playerRepository.GetById(dest.OpponentId).Result.Nickname))).ToList();
        }

        public async Task<GetPersonalLeaderboardDto> GetRecord(Guid id)
        {
             return _mapper.Map<GetPersonalLeaderboardDto>(await _repository.Get(id));
        }

        public async Task<PersonalLeaderboard> CreateRecord(CreatePersonalLeaderboardDto createPersonalLeaderboard)
        {
            PersonalLeaderboard mappedLeaderboard = _mapper.Map<PersonalLeaderboard>(createPersonalLeaderboard);
            var created = await _repository.Create(mappedLeaderboard);

            if (!created)
                throw new Exception("Failed to create the record");

            return mappedLeaderboard;
        }
        public async Task<bool> UpdateRecord(UpdatePersonalLeaderboardDto updatePersonalLeaderboard)
        {
            var updated = await _repository.Update(_mapper.Map<PersonalLeaderboard>(updatePersonalLeaderboard));

            if (!updated)
                throw new Exception("Failed to update the record");

            return updated;
        }

        public async Task<bool> DeleteRecord(Guid id)
        {
            var deleted = await _repository.Delete(id);

            if (!deleted)
                throw new Exception("Failed to delete the record");

            return deleted;
        }

        public async Task<List<GetNumberOfWinsPerPlayerDto>> GetRanking()
        {
            var getAll = await _repository.GetAll();
            var groupedRecords = getAll.GroupBy(pl => pl.PlayerId);
            var numberOfWins = new List<GetNumberOfWinsPerPlayerDto>();

            foreach(var group in groupedRecords)
            {
                var player = await _playerRepository.GetById(group.Key);
                var playerRecord = new GetNumberOfWinsPerPlayerDto
                {
                    Id = group.Key,
                    Nickname = player.Nickname,
                    Wins = group.Count(pl => pl.Result == 0),
                    IsOnline = player.IsOnline
                };

                numberOfWins.Add(playerRecord);
            }

            return numberOfWins;
        }
    }
}

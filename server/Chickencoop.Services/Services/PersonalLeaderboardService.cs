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
        private readonly IMapper _mapper;
        public PersonalLeaderboardService(IMapper mapper, IPersonalLeaderboardRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<List<GetPersonalLeaderboardDto>> GetAllRecords()
        {
            var getAll = await _repository.GetAll();
            return getAll.Select(pl => _mapper.Map<GetPersonalLeaderboardDto>(pl)).ToList();
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
    }
}

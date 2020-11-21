using Chickencoop.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Chickencoop.Repositories.Repositories.Interfaces
{
    public interface IPersonalLeaderboardRepository
    {
        Task<List<PersonalLeaderboard>> GetAll();
        Task<List<PersonalLeaderboard>> GetAllByPlayer(Guid playerId);
        Task<PersonalLeaderboard> Get(Guid id);
        Task<bool> Create(PersonalLeaderboard personalLeaderboard);
        Task<bool> Update(PersonalLeaderboard personalLeaderboard);
        Task<bool> Delete(Guid id);
    }
}

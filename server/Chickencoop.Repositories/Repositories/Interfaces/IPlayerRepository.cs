using Chickencoop.Models.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chickencoop.Repositories.Repositories.Interfaces
{
    public interface IPlayerRepository
    {
        Task<List<Player>> GetAll();
        Task<Player> Get(Guid id);
        Task<bool> Create(Player player);
        Task<bool> Update(Player player);
        Task<bool> Delete(Guid id);
    }
}

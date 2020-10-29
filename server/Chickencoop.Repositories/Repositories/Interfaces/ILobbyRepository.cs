using Chickencoop.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Chickencoop.Repositories.Repositories.Interfaces
{
    public interface ILobbyRepository
    {
        Task<List<Lobby>> GetAll();
        Task<Lobby> Get(Guid id);
        Task<bool> Create(Lobby lobby);
        Task<bool> Update(Lobby lobby);
        Task<bool> Delete(Guid id);
    }
}

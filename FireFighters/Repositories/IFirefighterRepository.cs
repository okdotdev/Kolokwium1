using FireFighters.Models;
using FireFighters.Models.DTOs;

namespace FireFighters.Repositories;

public interface IFirefighterRepository
{
    Task<ReturnedFireAction> GetActionById(int id);
    Task<bool> DeleteActionWithId(int id);
}
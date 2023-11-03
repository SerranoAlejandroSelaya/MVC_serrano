using MVC.Controllers.tasks.entity;
using Task = System.Threading.Tasks.Task;

namespace MVC.Controllers.tasks.repository;

public interface ITaskRep
{
    Task<IEnumerable<Card>> GetAllCardsAsync();
    Task<IEnumerable<Card>> GetAllCardsByUserIdAsync(string idUser);
    Task<IEnumerable<Card>> GetAllCardsByStatusAsync(string idUser, string status);
    Task<Card> GetCardByIdAsync(string id);
    Task<Card> InsertCardAsync(Card card);
    Task<Card> UpdateCardAsync(string id, Card card);
    Task<bool> DeleteCardAsync(string id);
}
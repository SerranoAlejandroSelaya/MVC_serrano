using MVC.Controllers.tasks.entity;
using Task = System.Threading.Tasks.Task;

namespace MVC.Controllers.tasks.repository;

public interface ITaskRep
{
    Task<IEnumerable<Card>> GetAllCardsAsync();
    Task<Card> GetCardById(string id);
    Task<Card> InsertCardAsync(Card card);
    Task<Card> UpdateCardAsync(string id, Card card);
    Task<bool> DeleteCardAsync(string id);
    Task<IEnumerable<Card>> SearchCardsAsync(string searchTerm);
}
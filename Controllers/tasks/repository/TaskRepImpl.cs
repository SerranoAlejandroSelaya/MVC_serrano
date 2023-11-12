
using MongoDB.Bson;

namespace MVC.Controllers.tasks.repository;

using MVC.Controllers.tasks.entity;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

public class TaskRepImpl : ITaskRep
{
    private readonly IMongoCollection<Card> _cards;

    public TaskRepImpl(IMongoDatabase cards)
    {
        _cards = cards.GetCollection<Card>("cards");
    }
    
    public async Task<IEnumerable<Card>> GetAllCardsAsync()
    {
        return await _cards.Find(_ => true).ToListAsync();
    }
    
    public async Task<Card> GetCardById(string id)
    {
        return await _cards.Find(c => c.Id == id).FirstOrDefaultAsync();
    }


    public async Task<Card> InsertCardAsync(Card card)
    {
        try
        {
            await _cards.InsertOneAsync(card);
            return card;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al insertar el documento: {ex.Message}", ex);
        }
    }

    public async Task<Card> UpdateCardAsync(string id, Card card)
    {
        var result = await _cards.ReplaceOneAsync(c => c.Id == id, card);
        
        if (!result.IsAcknowledged || result.ModifiedCount < 0 || Equals(result,null))
        {
            throw new Exception($"Error al insertar el documento:");
        }
        return card;
    }

    public async Task<bool> DeleteCardAsync(string id)
    {
        var result = await _cards.DeleteOneAsync(card => card.Id == id);
        return result.IsAcknowledged && result.DeletedCount > 0;
    }
    
    public async Task<IEnumerable<Card>> SearchCardsAsync(string searchTerm)
    {
        var filter = Builders<Card>.Filter.Or(
            Builders<Card>.Filter.Regex("nameTask", new BsonRegularExpression($".*{searchTerm}.*", "i")),
            Builders<Card>.Filter.Regex("description", new BsonRegularExpression($".*{searchTerm}.*", "i")),
            Builders<Card>.Filter.Regex("status", new BsonRegularExpression($".*{searchTerm}.*", "i")),
            Builders<Card>.Filter.Regex("storyP", new BsonRegularExpression($".*{searchTerm}.*", "i"))
        );
        
        var result = await _cards.Find(filter).ToListAsync();
        return result;
    }
}

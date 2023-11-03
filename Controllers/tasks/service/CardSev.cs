using MVC.Controllers.tasks.controller.CardReq;
using MVC.Controllers.tasks.controller.cardRes;
using MVC.Controllers.tasks.entity;
using MVC.Controllers.tasks.repository;

namespace MVC.Controllers.tasks.service;

public class CardSev
{
    private ITaskRep _rep;

    public CardSev(ITaskRep rep)
    {
        _rep = rep;
    }
    
    public async Task<IEnumerable<CardResp>> GetAllCardsAsync()
    {
        IEnumerable<Card> cards = await _rep.GetAllCardsAsync();
        IEnumerable<CardResp> cardResponses = cards.Select(card => 
            CardResp.CardToResponse(card));
        return cardResponses;
    }
    
    public async Task<IEnumerable<CardResp>> GetAllCardsByUserIdAsync(string idUser)
    {
        if (string.IsNullOrEmpty(idUser))
        {
            throw new Exception("The id User is Null");
        }
        IEnumerable<Card> cards = await _rep.GetAllCardsByUserIdAsync(idUser);
        IEnumerable<CardResp> cardResponses = cards.Select(card => 
            CardResp.CardToResponse(card));
        return cardResponses;
    }
    
    public async Task<IEnumerable<CardResp>> GetAllCardsByStatusAsync(string idUser, TaskStatus status)
    {
        if (string.IsNullOrEmpty(idUser))
        {
            throw new Exception("The id User or status is Null");
        }

        IEnumerable<Card> cards = await _rep.GetAllCardsByStatusAsync(idUser, status.ToString());
        IEnumerable<CardResp> cardResponses = cards.Select(card => 
            CardResp.CardToResponse(card));
        return cardResponses;
    }

    public async Task<CardResp> GetCardByIdAsync(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new Exception("The id is Null");
        }
        
        Card card = await _rep.GetCardByIdAsync(id);
        return CardResp.CardToResponse(card);
    }

    public async Task<CardResp> InsertCardAsync(CardReq card)
    {
        var result = await _rep.InsertCardAsync(
            CardReq.RequestToCard(card));
        return CardResp.CardToResponse(result);
    }

    public async Task<CardResp> UpdateCardAsync(string id, CardReq card)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new Exception("The id is Null");
        }
        var result = await _rep.UpdateCardAsync(id, CardReq.RequestToCard(card));
        return CardResp.CardToResponse(result);
    }

    public async Task<bool> DeleteCardAsync(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new Exception("The id is Null");
        }
        return await _rep.DeleteCardAsync(id);
    }
}
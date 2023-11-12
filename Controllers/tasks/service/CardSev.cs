using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
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

    public async Task<CardResp> GetCardById(string id)
    {
        var card = await _rep.GetCardById(id);
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

    public async Task<IEnumerable<CardResp>> SearchCardsAsync(string searchTerm)
    {
        var cards = _rep.SearchCardsAsync(searchTerm);
        return cards.Result.Select(card => CardResp.CardToResponse(card));
    }

}
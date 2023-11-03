using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using MVC.Controllers.tasks.controller.CardReq;
using MVC.Controllers.tasks.controller.cardRes;
using MVC.Controllers.tasks.service;

namespace MVC.Controllers.tasks.controller;

public class CardController : Controller
{
    private readonly CardSev _sev;

    public CardController(CardSev sev)
    {
        _sev = sev ?? throw new ArgumentNullException(nameof(sev));
    }

    public async Task<IActionResult> GetAll()
    {
        IEnumerable<CardResp> cardResponses = await _sev.GetAllCardsAsync();
        return View("GetAllCards", cardResponses); // Pasa el modelo a la vista "GetAllCards"
    }

    public async Task<IActionResult> GetAllCardsById([NotNull] string id)
    {
        IEnumerable<CardResp> cardResponses = await _sev.GetAllCardsByUserIdAsync(id);
        return View("GetAllCards", cardResponses); // Pasa el modelo a la vista "GetAllCards"
    }

    public async Task<IActionResult> GetAllByStatus([NotNull] string id, TaskStatus status)
    {
        IEnumerable<CardResp> cardResponses = await _sev.GetAllCardsAsync();
        return View("GetAllCards", cardResponses); // Pasa el modelo a la vista "GetAllCards"
    }

    public async Task<IActionResult> Get([NotNull] string id)
    {
        CardResp card = await _sev.GetCardByIdAsync(id);
        return View("GetCard", card); // Pasa el modelo a la vista "GetCard"
    }

    [HttpPost]
    public async Task<IActionResult> Insert(CardReq.CardReq card)
    {
        if (!ModelState.IsValid)
        {
            return View(card);
        }

        CardResp result = await _sev.InsertCardAsync(card);
        return RedirectToAction("Index");
    }


    public async Task<IActionResult> Update([NotNull] string id, [FromBody] CardReq.CardReq card)
    {
        if (Equals(card, null))
        {
            return BadRequest();
        }

        CardResp updatedCard = await _sev.UpdateCardAsync(id, card);
        if (Equals(updatedCard, null))
        {
            return NotFound();
        }

        return View("CardUpdated", updatedCard); // Pasa el modelo a la vista "CardUpdated"
    }

    public async Task<IActionResult> Delete([NotNull] string id)
    {
        bool result = await _sev.DeleteCardAsync(id);
        if (!result)
        {
            return NotFound();
        }

        return View("Index"); 
    }
    
    public async Task<IActionResult> Index()
    {
        var cardResponses = await _sev.GetAllCardsAsync();
        if (Equals(cardResponses, null))
        {
            return View(new List<CardResp>()); 
        }
        return View(cardResponses);
    }
    
    public IActionResult Privacy()
    {
        return View();
    }
    
    [HttpGet]
    public IActionResult CreateCardForm()
    {
        var cardReq = new CardReq.CardReq();
        return PartialView("_CreateCardForm", cardReq);
    }


}

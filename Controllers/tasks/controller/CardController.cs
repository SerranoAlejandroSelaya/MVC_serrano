using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVC.Controllers.tasks.controller.CardReq;
using MVC.Controllers.tasks.controller.cardRes;
using MVC.Controllers.tasks.service;

namespace MVC.Controllers.tasks.controller
{
    public class CardController : Controller
    {
        private readonly CardSev _sev;

        public CardController(CardSev sev)
        {
            _sev = sev ?? throw new ArgumentNullException(nameof(sev));
        }

        public async Task<IActionResult> Get()
        {
            IEnumerable<CardResp> cardResponses = await _sev.GetAllCardsAsync();
            return View("Index", cardResponses);
        }

        [HttpPost]
        public async Task<IActionResult> Insert(CardReq.CardReq card)
        {
            await _sev.InsertCardAsync(card);
            return Redirect("/");
        }
        
        [HttpGet]
        public async Task<IActionResult> GetCardById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }
            var card = await _sev.GetCardById(id);
            if (Equals(card,null))
            {
                return NotFound();
            }
            ViewBag.IsUpdate = true;
            ViewBag.CardData = card;
            return PartialView("_UpdateCardForm", CardReq.CardReq.ResponseToReq(card));
        }


        public async Task<IActionResult> Update([NotNull] string id, [FromBody] CardReq.CardReq card)
        {
            Console.WriteLine(id);

            CardResp updatedCard = await _sev.UpdateCardAsync(id, card);
            if (Equals(updatedCard, null))
            {
                return NotFound();
            }
            return View("/");
        }

        public async Task<IActionResult> Delete([NotNull] string id)
        {
            bool result = await _sev.DeleteCardAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return Redirect("/");
        }
        
        public async Task<IActionResult> Index(string search)
        {
            var cardResponses = await _sev.SearchCardsAsync(search);
            if (Equals(cardResponses, null))
            {
                return View(new List<CardResp>());
            }
            return View(cardResponses);
        }


        public async Task<IActionResult> Search(string search)
        {
            var cardResponses = await _sev.SearchCardsAsync(search);
            return View("Index", cardResponses);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateCardForm()
        {
            return PartialView("_CreateCardForm");
        }
    }
}

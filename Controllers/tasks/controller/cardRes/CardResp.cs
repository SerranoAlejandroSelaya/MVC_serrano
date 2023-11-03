using MVC.Controllers.tasks.entity;

namespace MVC.Controllers.tasks.controller.cardRes;

public class CardResp
{
    public string Id { get; set; }
    public string idUser { get; set; } = "";
    public string idChecks { get; set; } = "";
    public string nameTask { get; set; } = "";
    public string description { get; set; } = "";
    public string status { get; set; } = "";
    public int? storyP { get; set; } = 1;
    
    public static CardResp CardToResponse(Card card)
    {
        return new CardResp
        {
            Id = card.Id,
            idUser = card.idUser,
            idChecks = card.idChecks,
            nameTask = card.nameTask,
            description = card.description,
            storyP = card.storyP,
            status = card.status
        };
    }
}
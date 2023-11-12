using System.ComponentModel.DataAnnotations;
using MVC.Controllers.tasks.entity;
using MongoDB.Bson;
using MVC.Controllers.tasks.controller.cardRes;

namespace MVC.Controllers.tasks.controller.CardReq;

public class CardReq
{
    [Required(ErrorMessage = "El campo 'idUser' es requerido.")]
    public string idUser { get; set; }
    [Required(ErrorMessage = "El campo 'nameTask' es requerido.")]
    public string nameTask { get; set; } = "";
    public string idChecks { get; set; } = "";
    public string description { get; set; } = "";
    public TaskStatus status { get; set; } = TaskStatus.Canceled;
    public int? storyP { get; set; } = 0;
    
    public static Card RequestToCard(CardReq req)
    {
        return new Card
        {
            Id =  ObjectId.GenerateNewId().ToString(),
            idUser = req.idUser,
            idChecks = req.idChecks,
            nameTask = req.nameTask,
            description = req.description,
            storyP = req.storyP,
            status = req.status.ToString()
        };
    }
    
    public static CardReq ResponseToReq(CardResp req)
    {
        return new CardReq()
        {
            idUser = req.idUser,
            idChecks = req.idChecks,
            nameTask = req.nameTask,
            description = req.description,
            storyP = req.storyP,
        };
    }
}
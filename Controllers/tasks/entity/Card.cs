namespace MVC.Controllers.tasks.entity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Card
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    [BsonElement("idUser")]
    public string? idUser { get; set; } = "";
    [BsonElement("idChecks")]
    public string? idChecks { get; set; } = "";
    [BsonElement("nameTask")]
    public string? nameTask { get; set; } = "";
    [BsonElement("description")]
    public string? description { get; set; } = "";
    [BsonElement("storyP")]
    public int? storyP { get; set; } = null;
    [BsonElement("status")]
    public string? status { get; set; } = "";
}
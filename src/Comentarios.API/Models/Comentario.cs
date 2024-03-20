using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Comentarios.API.Models;

public class Comentario
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;

    [JsonPropertyName("content_id")]
    public int ContentId { get; set; }
}

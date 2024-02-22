using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace GraphQL_API_X_clone.Models.Schemas;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonRepresentation(BsonType.String)]
    public string UserName { get; set; } = null!;

    [BsonRepresentation(BsonType.String)]
    public string Email { get; set; } = null!;

    [BsonRepresentation(BsonType.String)]
    public string Image { get; set; } = null!;

    [BsonRepresentation(BsonType.String)]
    public string Description { get; set; } = null!;

   
    [BsonElement("CreatedAt")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("followers")]
    [JsonPropertyName("followers")]
    public List<string> userIds { get; set; } = null!;

}


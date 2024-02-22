using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace GraphQL_API_X_clone.Models.Schemas;

public class Post
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonRepresentation(BsonType.String)]
    public string Content { get; set; } = null!;

    [BsonRepresentation(BsonType.String)]
    public string Media { get; set; } = null!;

    [BsonRepresentation(BsonType.ObjectId)]
    public string? userId { get; set; }

    [BsonElement("likes")]
    [JsonPropertyName("likes")]
    public List<string> userIds { get; set; } = null!;

    [BsonElement("replies")]
    [JsonPropertyName("replies")]
    public List<string> postsIds { get; set; } = null!;

    [BsonElement("CreatedAt")]
    public DateTime CreatedAt { get; set; }

}


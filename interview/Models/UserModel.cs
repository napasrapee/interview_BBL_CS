using System.Text.Json.Serialization;

namespace MyWebApi.Models;

public record UserModel(
    [property: JsonPropertyName("id")] long Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("username")] string Username,
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("phone")] string Phone,
    [property: JsonPropertyName("website")] string Website
);

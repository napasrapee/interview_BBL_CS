using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace MyWebApi.Models;

public record UserModel(
    [property: JsonPropertyName("id")] 
    long Id,

    [property: JsonPropertyName("name")] 
    [Required(ErrorMessage = "Name is required.")] // <-- เพิ่ม Validation
    string Name,

    [property: JsonPropertyName("username")] 
    [Required(ErrorMessage = "Username is required.")] // <-- เพิ่ม Validation
    string Username,

    [property: JsonPropertyName("email")] 
    [Required(ErrorMessage = "Email is required.")] // <-- เพิ่ม Validation
    [EmailAddress(ErrorMessage = "Invalid email format.")] // (แถม) ตรวจสอบรูปแบบอีเมลให้อัตโนมัติ
    string Email,

    [property: JsonPropertyName("phone")] 
    string Phone,

    [property: JsonPropertyName("website")] 
    string Website
);
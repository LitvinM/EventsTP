using System.Text.Json.Serialization;

namespace DataAccess.Entities;

public class ParticipantEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string Surname { get; set; } = "";
    public DateOnly DateOfBirth { get; set; }
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
    [JsonIgnore]
    public List<EventEntity> Events { get; set; } = new ();
    
}
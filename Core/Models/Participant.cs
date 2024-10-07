using System.Text.Json.Serialization;

namespace Core.Models;

public class Participant
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    [JsonIgnore]
    public List<Event> Events { get; set; } = new ();
    
    private Participant(string name, string surname, DateOnly dateOfBirth, string email, string password)
    {
        Name = name;
        Surname = surname;
        DateOfBirth = dateOfBirth;
        Email = email;
        Password = password;
    }

    public static Participant Create(string name, string surname, DateOnly dateOfBirth, string email, string password)
    {
        return new Participant(name, surname, dateOfBirth, email, password);
    }
}
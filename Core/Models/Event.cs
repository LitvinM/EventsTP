using System.Reflection;

namespace Core.Models;

public class Event
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime TimeAndDate { get; set; }
    public string Place { get; set; }
    public string Category { get; set; }
    public uint ParticipantsMaxAmount { get; set; }
    public List<Participant> Participants { get; set; } = new ();
    public string Image { get; set; }

    private Event(string name, string description, DateTime timeAndDate, string place, string category, uint participantsMaxAmount, string image)
    {
        Name = name;
        Description = description;
        TimeAndDate = timeAndDate;
        Place = place;
        Category = category;
        ParticipantsMaxAmount = participantsMaxAmount;
        Image = image;
    }

    public static Event Create(string name, string description, DateTime timeAndDate, string place, string category,
        uint participantsMaxAmount, string image)
    {
        return new Event(name, description, timeAndDate, place, category,
            participantsMaxAmount, image);
    }
    
    public bool Contains(string searchString)
    {
        if (string.IsNullOrEmpty(searchString))
            return false;

        var properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

        return properties.Select(property => property.GetValue(this)?.ToString())
            .Any(value => value != null && value.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0);
    }
}
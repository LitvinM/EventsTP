using System.Reflection;

namespace DataAccess.Entities;

public class EventEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public DateTime TimeAndDate { get; set; }
    public string Place { get; set; } = "";
    public string Category { get; set; } = "";
    public uint ParticipantsMaxAmount { get; set; }
    public List<ParticipantEntity> Participants { get; set; } = new ();
    public string Image { get; set; } = "";

    public bool Contains(string searchString)
    {
        if (string.IsNullOrEmpty(searchString))
            return false;

        var properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

        return properties.Select(property => property.GetValue(this)?.ToString())
            .Any(value => value != null && value.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ZkmBusTimetables.Core.Models
{
    public class Variant
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid LineId { get; set; }
        [JsonIgnore]
        public Line Line { get; set; } = default!;
        public string Route { get; set; } = default!;
        public bool IsDefault { get; set; } = default!;
        public string? Slug { get; private set; }
        public DateOnly ValidFrom { get; set; }
        public ICollection<RouteStop> RouteStops { get; init; } = new List<RouteStop>();
        public ICollection<Departure> Departures { get; init; } = new List<Departure>();
        public ICollection<RouteLinePoint> RouteLinePoints { get; init; } = new List<RouteLinePoint>();
        
        public void GenerateSlug()
        {

            var name = Route.ToLowerInvariant()
            .Replace(", ", "-").Replace(". ", "-")
                .Replace(' ', '-').Replace('/', '-')
                .Replace('.', '-').Replace("(", "")
                .Replace(")", "");



            var stringBuilder = new StringBuilder();

            foreach (var character in name)
            {
                Dictionary<char, char> polishToEnglishChars = new Dictionary<char, char>
                {
                    { 'ą', 'a' }, { 'ć', 'c' }, { 'ę', 'e' }, { 'ł', 'l' }, { 'ń', 'n' },
                    { 'ó', 'o' }, { 'ś', 's' }, { 'ź', 'z' }, { 'ż', 'z' }
                };
                if (polishToEnglishChars.TryGetValue(character, out var englishCharacter))
                {
                    stringBuilder.Append(englishCharacter);
                }
                else
                {
                    stringBuilder.Append(character);
                }
            }
            Slug = stringBuilder.ToString();
        }

    }
}

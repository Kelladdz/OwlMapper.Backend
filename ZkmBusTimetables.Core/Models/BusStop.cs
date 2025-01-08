using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ZkmBusTimetables.Core.Models
{
    public class BusStop
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string City { get; set; } = default!;
        public Coordinate Coordinate { get; init; }
        public bool IsRequest { get; set; }
        public string Slug { get; private set; } = default!;
        public  ICollection<RouteStop> RouteStops { get; init; } = new List<RouteStop>();
        public void GenerateSlug(int? number)
        {
            var stringForSlug = Name.Contains(City) ? Name : $"{City} {Name}";

            var name = stringForSlug.ToLowerInvariant()
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
            if (number is not null)
            {
                stringBuilder.Append($"-{number}");
            }
            Slug = stringBuilder.ToString();
        }

        public override string ToString() => Name.Contains(City) ? Name : $"{City} {Name}";
    }
}

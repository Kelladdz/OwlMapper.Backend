using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZkmBusTimetables.Core.Models
{
    public class ChangeHistory
    {
        public Guid Id { get; set; }
        public string EntityType { get; set; } = default!; // Typ obiektu (np. Przystanek, Linia, Rozkład jazdy)
        public string? EntityId { get; set; } // Identyfikator obiektu
        [NotMapped]
        public object? Entity { get; set; } // Obiekt, który uległ zmianie
        public string ChangeType { get; set; } = default!; // Typ zmiany (Dodanie, Usunięcie, Modyfikacja)
        public string ChangedBy { get; set; } = default!; // Użytkownik, który dokonał zmiany
        public DateTime ChangeDate { get; set; } // Data zmiany
    }
}

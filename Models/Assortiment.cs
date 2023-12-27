using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab4.Models
{
    public class Assortiment
    {
        public int Id { get; set; }
        [Required]
        public string? Kod { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Price { get; set; }
        List<Registration> Registrations { get; set; } = new();
    }
}

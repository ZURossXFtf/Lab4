using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Models
{
    public class Registration
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Weight { get; set; }
        public string? Cost { get; set; }
        [Required]
        public DateTime? DateConfirm { get; set; }
        public int AssortimentId { get; set; }
        public Assortiment? Assortiment { get; set; }
    }
}

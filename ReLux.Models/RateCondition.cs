using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReLux.Models
{
    public class RateCondition
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Rate must be between 1 and 5 stars")]
        public int Stars { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReLux.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Image { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Price should be larger than $1")]
        public double Price { get; set; }

        [Display(Name = "Est. Retail Value")]
        [Range(1, double.MaxValue, ErrorMessage = "Price should be larger than $1")]
        public double EstRetailValue { get; set; }

        [Required]
        public bool IsSold { get; set; }

        // Set relationship
        [Display(Name = "Rate Condition")]
        public int RateConditionId { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        // Create Foreign Key
        [ForeignKey("RateConditionId")]
        public RateCondition RateCondition { get; set; }
        public Category Category { get; set; }
    }
}

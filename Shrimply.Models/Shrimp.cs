using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shrimply.Models
{
    public class Shrimp
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        [DisplayName("Bar Code")]
        public string BarCode { get; set; }
        [Required]
        public string Owner { get; set; }
        [Required]
        [Display(Name="List Price")]
        [Range(1, 1000)]
        public double ListPrice { get; set; }
        [Required]
        [Display(Name = "Price 1-50")]
        [Range(1, 1000)]
        public double Price { get; set; }
        [Required]
        [Display(Name = "Price 50+")]
        [Range(1, 1000)]
        public double Price50 { get; set; }
        [Required]
        [Display(Name = "Price 100+")]
        [Range(1, 1000)]
        public double Price100 { get; set; }
        public int SpeciesId { get; set; }
        [ForeignKey("SpeciesId")]
        [ValidateNever]
        public Species Species { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerWebshop.APIClientLibrary.DTO
{
    public class BeerDTO
    {
        [Required, MaxLength(100)]
        public required string Name { get; set; }
        [Required, MaxLength(100)]
        public required string Brewery { get; set; }
        [Required]
        public required float Price { get; set; }
        [MaxLength(1000)]
        public required string Description { get; set; }
        [Required]
        public required int Stock { get; set; }
        [Required]
        public float ABV { get; set; }
        [Required]
        public required string Type { get; set; }
        [Required, MaxLength(100)]
        public string Url { get; set; }
    }
}

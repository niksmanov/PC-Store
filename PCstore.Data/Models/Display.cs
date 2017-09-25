using System.ComponentModel.DataAnnotations;

namespace PCstore.Data.Models
{
    public class Display
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(0d, 19d)]
        public double Size { get; set; }

        [Required]
        [MinLength(7)] //800x600
        [MaxLength(9)] //1920x1024        
        public string Resolution { get; set; }
        
        [Required]
        [MinLength(1)]
        [MaxLength(5)]
        public string Type { get; set; }


        [Required]
        [Range(256000d, 17000000d)]
        public double Colors { get; set; }

        [Required]
        [Range(1, 10000)]
        public decimal Price { get; set; }
    }
}

using PCstore.Data.Model.Abstracts;
using PCstore.Data.Model.Contracts;
using System.ComponentModel.DataAnnotations;

namespace PCstore.Data.Model
{
    public class Display : DataModel, IDevice
    {
        [Required]
        [Range(1d, 19d)]
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

        [Required]
        [MinLength(8)]
        [MaxLength(12)]
        public string SellerPhone { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(500)]
        public string Description { get; set; }

        public virtual User Seller { get; set; }
    }
}

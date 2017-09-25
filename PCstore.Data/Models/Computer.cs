using System.ComponentModel.DataAnnotations;

namespace PCstore.Data.Models
{
    public class Computer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(30)]
        public string CPU { get; set; }

        [Required]
        [Range(0d, 5d)]
        public double CpuSpeed { get; set; }

        [Required]
        [Range(0, 64)]
        public int RAM { get; set; }

        [Required]
        [MinLength(0)]
        [MaxLength(10)]
        public string RamType { get; set; }

        [Required]
        [Range(0, 2000)]
        public int HDD { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(30)]
        public string GPU { get; set; }

        [Required]
        [Range(0d, 5d)]
        public double GpuMemory { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(20)]
        public string OpticalDevice { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(20)]
        public string OperatingSystem { get; set; }

        [Required]
        [Range(1, 10000)]
        public decimal Price { get; set; }

        [Required]
        [Range(1, 10)]
        public long SellerPhone { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(20)]
        public string SellerEmail { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(500)]
        public string Description { get; set; }
    }
}

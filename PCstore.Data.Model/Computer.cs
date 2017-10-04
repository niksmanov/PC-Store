using PCstore.Data.Model.Abstracts;
using PCstore.Data.Model.Contracts;
using System.ComponentModel.DataAnnotations;

namespace PCstore.Data.Model
{
    public class Computer : DataModel, IDevice
    {
        [Required]
        [MinLength(10)]
        [MaxLength(30)]
        public string CPU { get; set; }

        [Required]
        [Range(1d, 5d)]
        public double CpuSpeed { get; set; }

        [Required]
        [Range(1, 64)]
        public int RAM { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(10)]
        public string RamType { get; set; }

        [Required]
        [Range(1, 2000)]
        public int HDD { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(30)]
        public string GPU { get; set; }

        [Required]
        [Range(1, 8)]
        public int GpuMemory { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        public string OpticalDevice { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(20)]
        public string OperatingSystem { get; set; }

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

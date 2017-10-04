using PCstore.Data.Model;
using PCstore.Web.Infrastructure;
using System;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace PCstore.Web.ViewModels.Device
{
    public class ComputerViewModel : IMapFrom<Computer>, IHaveCustomMappings, IDeviceViewModel
    {
        public Guid Id { get; set; }


        [Required]
        [MinLength(10)]
        [MaxLength(30)]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        public string CPU { get; set; }


        [Required]
        [Display(Name = "CPU Speed")]
        [Range(1d, 5d, ErrorMessage = "CPU speed must be between 1 and 5 Ghz decimal number")]
        public double CpuSpeed { get; set; }


        [Required]
        [Range(1, 64, ErrorMessage = "RAM must be between 1 and 64 GB integer number")]
        public int RAM { get; set; }


        [Required]
        [MinLength(1)]
        [MaxLength(10)]
        [Display(Name = "Ram Type")]
        [StringLength(10, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        public string RamType { get; set; }


        [Required]
        [Range(1, 2000, ErrorMessage = "HDD must be between 1 and 2000 GB integer number")]
        public int HDD { get; set; }


        [Required]
        [MinLength(10)]
        [MaxLength(30)]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        public string GPU { get; set; }


        [Required]
        [Display(Name = "GPU Memory")]
        [Range(1, 8, ErrorMessage = "GPU Memory must be between 1 and 8 integer number")]
        public int GpuMemory { get; set; }


        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        [Display(Name = "Optical Device")]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        public string OpticalDevice { get; set; }


        [Required]
        [MinLength(4)]
        [MaxLength(20)]
        [Display(Name = "Operating System")]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        public string OperatingSystem { get; set; }


        [Required]
        [Range(1, 10000, ErrorMessage = "Price must be between 1 and 10000 decimal number")]
        public decimal Price { get; set; }


        [Required]
        [MinLength(8)]
        [MaxLength(12)]
        [Display(Name = "Seller Phone")]
        [StringLength(12, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        public string SellerPhone { get; set; }


        [Required]
        [MinLength(1)]
        [MaxLength(500)]
        [StringLength(500, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        public string Description { get; set; }


        [Required]
        public virtual User Seller { get; set; }


        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime CreatedOn { get; set; }


        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime PostedOn { get; set; }


        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Computer, ComputerViewModel>()
                .ForMember(viewModel => viewModel.PostedOn, cfg => cfg.MapFrom(model => model.ModifiedOn));
        }
    }
}
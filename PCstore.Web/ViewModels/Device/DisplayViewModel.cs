using PCstore.Data.Model;
using PCstore.Web.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;

namespace PCstore.Web.ViewModels.Device
{
    public class DisplayViewModel : IMapFrom<Display>, IHaveCustomMappings, IDeviceViewModel
    {
        public Guid Id { get; set; }


        [Required]
        [Display(Name = "Display Size")]
        [Range(1d, 19d, ErrorMessage = "Display size must be between 1 and 19 inch decimal number")]
        public double Size { get; set; }


        [Required]
        [MinLength(7)] //800x600
        [MaxLength(9)] //1920x1024 
        [Display(Name = "Display Resolution")]
        [StringLength(9, ErrorMessage = "The {0} must be at least {2} characters long in format 1920x1024", MinimumLength = 7)]
        public string Resolution { get; set; }


        [Required]
        [MinLength(1)]
        [MaxLength(5)]
        [StringLength(5, ErrorMessage = "The {0} must be between {2} and 5 characters long", MinimumLength = 1)]
        public string Type { get; set; }


        [Required]
        [Range(256000d, 17000000d, ErrorMessage = "Colors must be between 256 k and 17 mil decimal number")]
        public double Colors { get; set; }


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
            configuration.CreateMap<Display, DisplayViewModel>()
                .ForMember(viewModel => viewModel.PostedOn, cfg => cfg.MapFrom(model => model.ModifiedOn));
        }
    }
}
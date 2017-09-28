using PCstore.Data.Model;
using PCstore.Web.Infrastructure;
using System;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace PCstore.Web.ViewModels.Device
{
    public class LaptopViewModel : IMapFrom<Laptop>, IHaveCustomMappings
    {
        [Required]
        public double DisplaySize { get; set; }
        [Required]
        public string DisplayResolution { get; set; }
        [Required]
        public string CPU { get; set; }
        [Required]
        public double CpuSpeed { get; set; }
        [Required]
        public int RAM { get; set; }
        [Required]
        public string RamType { get; set; }
        [Required]
        public int HDD { get; set; }
        [Required]
        public string GPU { get; set; }
        [Required]
        public double GpuMemory { get; set; }
        [Required]
        public bool Battery { get; set; }
        [Required]
        public string OpticalDevice { get; set; }
        [Required]
        public string OperatingSystem { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string SellerPhone { get; set; }
        [Required]
        public string SellerEmail { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public virtual User Seller { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime PostedOn { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Laptop, LaptopViewModel>()
             .ForMember(x => x.PostedOn, cfg => cfg.MapFrom(y => y.CreatedOn));
        }
    }
}
using PCstore.Data.Model;
using PCstore.Web.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;

namespace PCstore.Web.ViewModels.Device
{
    public class DisplayViewModel : IMapFrom<Display>, IHaveCustomMappings
    {
        [Required]
        public double Size { get; set; }
        [Required]
        public string Resolution { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public double Colors { get; set; }
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
            configuration.CreateMap<Display, DisplayViewModel>()
             .ForMember(x => x.PostedOn, cfg => cfg.MapFrom(y => y.CreatedOn));
        }
    }
}
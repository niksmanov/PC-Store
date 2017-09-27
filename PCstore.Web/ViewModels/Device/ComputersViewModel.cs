using AutoMapper.Attributes;
using PCstore.Data.Model;
using System;
using System.ComponentModel.DataAnnotations;

namespace PCstore.Web.ViewModels.Device
{
    [MapsFrom(typeof(Computer))]
    public class ComputersViewModel
    {
        public string CPU { get; set; }
        public double CpuSpeed { get; set; }
        public int RAM { get; set; }
        public string RamType { get; set; }
        public int HDD { get; set; }
        public string GPU { get; set; }
        public double GpuMemory { get; set; }
        public string OpticalDevice { get; set; }
        public string OperatingSystem { get; set; }
        public decimal Price { get; set; }
        public string SellerPhone { get; set; }
        public string SellerEmail { get; set; }
        public string Description { get; set; }
        public virtual User Seller { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime PostedOn { get; set; }
    }
}
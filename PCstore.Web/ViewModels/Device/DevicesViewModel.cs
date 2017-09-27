using System.Collections.Generic;

namespace PCstore.Web.ViewModels.Device
{
    public class DevicesViewModel
    {
        public ICollection<ComputersViewModel> Computers { get; set; }
        public ICollection<LaptopsViewModel> Laptops { get; set; }
        public ICollection<DisplaysViewModel> Displays { get; set; }
    }
}
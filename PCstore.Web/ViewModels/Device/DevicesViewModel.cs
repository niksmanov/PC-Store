using System.Collections.Generic;

namespace PCstore.Web.ViewModels.Device
{
    public class DevicesViewModel
    {
        public ICollection<ComputerViewModel> Computers { get; set; }
        public ICollection<LaptopViewModel> Laptops { get; set; }
        public ICollection<DisplayViewModel> Displays { get; set; }
    }
}
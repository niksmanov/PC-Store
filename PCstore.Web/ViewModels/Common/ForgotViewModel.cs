using System.ComponentModel.DataAnnotations;

namespace PCstore.Web.Models
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
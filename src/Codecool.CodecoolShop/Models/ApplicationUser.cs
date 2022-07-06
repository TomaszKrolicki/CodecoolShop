using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Codecool.CodecoolShop.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(128)]
        [Display(Name = "Street")]
        public string StreetAddress { get; set; }
        [MaxLength(64)]
        [Display(Name = "City")]
        public string City { get; set; }
        [MaxLength(64)]
        [Display(Name = "State")]
        public string State { get; set; }
        [MaxLength(16)]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }
    }
}

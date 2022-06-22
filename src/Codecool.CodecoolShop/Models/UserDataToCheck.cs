using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Codecool.CodecoolShop.Models
{
    public class UserDataToCheck
    {
        [DisplayName("First Name")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        [Required]
        [StringLength(30)]
        public string LastName { get; set; }
        
        [DisplayName("Email")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        [Required]
        [StringLength(30)]
        public string Email { get; set; }

        [DisplayName("Phone Number")]
        [RegularExpression(@"^\d+$")]
        [Required]
        [MinLength(6)]
        [StringLength(12)]
        public string PhoneNumber { get; set; }

        [DisplayName("Country")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        [Required]
        [StringLength(30)]
        public string BillingCountry { get; set; }

        [DisplayName("City")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        [Required]
        [StringLength(30)]
        public string BillingCity { get; set; }

        [DisplayName("ZipCode")]
        [RegularExpression(@"^[0-9]{2}-[0-9]{3}$")]
        [Required]
        [StringLength(6)]
        [MinLength(6)]
        public string BillingZip { get; set; }

        [DisplayName("Address")]
        [Required]
        [StringLength(60)]
        public string BillingAddress { get; set; }

        [DisplayName("Country")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        [Required]
        [StringLength(30)]
        public string ShippingCountry { get; set; }

        [DisplayName("City")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        [Required]
        [StringLength(30)]
        public string ShippingCity { get; set; }

        [DisplayName("ZipCode")]
        [RegularExpression(@"^[0-9]{2}-[0-9]{3}$")]
        [Required]
        [StringLength(6)]
        [MinLength(6)]
        public string ShippingZip { get; set; }

        [DisplayName("Address")]
        [Required]
        [StringLength(60)]
        public string ShippingAddress { get; set; }
        [DisplayName("Do you want to pay now?")]
        public bool IsPayedNow { get; set; }
    }
}

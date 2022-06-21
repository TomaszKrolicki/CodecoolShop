using System.ComponentModel;

namespace Codecool.CodecoolShop.Models
{
    public class UserData
    {
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }
        [DisplayName("Country")]
        public string BillingCountry { get; set; }
        [DisplayName("City")]
        public string BillingCity { get; set; }
        [DisplayName("ZipCode")]
        public string BillingZip { get; set; }
        [DisplayName("Address")]
        public string BillingAddress { get; set; }
        [DisplayName("Country")]
        public string ShippingCountry { get; set; }
        [DisplayName("City")]
        public string ShippingCity { get; set; }
        [DisplayName("ZipCode")]
        public string ShippingZip { get; set; }
        [DisplayName("Address")]
        public string ShippingAddress { get; set; }
    }
}

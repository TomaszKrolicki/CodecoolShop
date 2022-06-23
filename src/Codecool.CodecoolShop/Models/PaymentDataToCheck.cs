using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Codecool.CodecoolShop.Models
{
    public class PaymentDataToCheck
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

        [DisplayName("Card Number")]
        [RegularExpression(@"^\d+$")]
        [Required]
        [MinLength(16)]
        [StringLength(16)]
        public string CardNumber { get; set; }

        [DisplayName("Expiry Date")]
        [RegularExpression(@"^[0-9]{4}-[0-9]{4}-[0-9]{4}-[0-9]{4}$")]
        [Required]
        [StringLength(19)]
        [MinLength(19)]
        public string ExpiryDate { get; set; }

        [DisplayName("CVV Code")]
        [RegularExpression(@"^[0-9]{2}-[0-9]{2}$")]
        [Required]
        [StringLength(5)]
        [MinLength(5)]
        public string CvvCode { get; set; }



    }
}
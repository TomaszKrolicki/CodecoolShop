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
        [RegularExpression(@"^[0-9]{4}-[0-9]{4}-[0-9]{4}-[0-9]{4}$")]
        [Required]
        [MinLength(19)]
        [StringLength(19)]
        public string CardNumber { get; set; }

        [DisplayName("Expiry Date")]
        [RegularExpression(@"^[0-9]{2}/[0-9]{2}")]
        [Required]
        [StringLength(5)]
        [MinLength(5)]
        public string ExpiryDate { get; set; }

        [DisplayName("CVV Code")]
        [RegularExpression(@"^[0-9]{3}$")]
        [Required]
        [StringLength(3)]
        [MinLength(3)]
        public string CvvCode { get; set; }



    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codecool.CodecoolShop.Models
{
    public class CartDetail
    {
        [Key]
        public int Id { get; set; }
        //[Required]
        //public int ProductId { get; set; }
        //[ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
        [Required]
        public int CartId { get; set; }
        [ForeignKey(nameof(CartId))]
        public Cart Cart { get; set; }
        //[Required]
        //public int Quantity { get; set; }
    }
}

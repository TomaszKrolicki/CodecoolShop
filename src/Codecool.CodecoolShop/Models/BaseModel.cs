using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Codecool.CodecoolShop.Models
{
    public abstract class BaseModel
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(128)]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Codecool.CodecoolShop.Models
{
    public class ProductCategory: BaseModel
    {
        [DataMember]
        public List<Product> Products { get; set; }
        [DataMember]
        public string Department { get; set; }
    }
}

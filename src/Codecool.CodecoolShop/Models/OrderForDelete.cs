using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Codecool.CodecoolShop.Services;

namespace Codecool.CodecoolShop.Models
{
    [DataContract]
    public class OrderForDelete
    {
        [DataMember]
        public bool IsSuccessFull { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string UserId { get; set; }
        [DataMember]
        public List<CartDetail> CartDetails { get; set; }
        [DataMember]
        public decimal ShoppingCartValue { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string PhoneNumber { get; set; }
        [DataMember]
        public string BillingCountry { get; set; }
        [DataMember]
        public string BillingCity { get; set; }
        [DataMember]
        public string BillingZip { get; set; }
        [DataMember]
        public string BillingAddress { get; set; }
        [DataMember]
        public string ShippingCountry { get; set; }
        [DataMember]
        public string ShippingCity { get; set; }
        [DataMember]
        public string ShippingZip { get; set; }
        [DataMember]
        public string ShippingAddress { get; set; }
        [DataMember]
        public DateTime OrderDateTime { get; set; }
        [DataMember]
        public int OrderId { get; set; }
        [DataMember]
        public bool IsPayed { get; set; }
        public string SaveToJson()
        {
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(OrderForDelete));
            MemoryStream msObj = new MemoryStream();
            json.WriteObject(msObj, this);
            msObj.Position = 0;
            StreamReader sr = new StreamReader(msObj);
            string jsonOrder = sr.ReadToEnd();
            sr.Close();
            msObj.Close();
            return jsonOrder;
        }
    }
}
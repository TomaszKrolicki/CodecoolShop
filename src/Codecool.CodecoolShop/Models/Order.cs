using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Codecool.CodecoolShop.Services;

namespace Codecool.CodecoolShop.Models
{
    [DataContract]
    public class Order : BaseModel
    {
        [DataMember]
        public User User { get; set; }
        //[DataMember]
        //public UserDataToCheck UserPersonalInformation { get; set; }
        [DataMember]
        public DateTime OrderDateTime { get; set; }

        [DataMember]
        public bool IsSuccessFull { get; set; }
        public bool IsPayedNow { get; set; }
      
        public string SaveToJson()
        {
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(Order));
            MemoryStream msObj = new MemoryStream();
            json.WriteObject(msObj, this);
            msObj.Position = 0;
            StreamReader sr = new StreamReader(msObj);

            // "{\"Description\":\"Share Knowledge\",\"Name\":\"C-sharpcorner\"}"
            string jsonOrder = sr.ReadToEnd();

            sr.Close();
            msObj.Close();
            return jsonOrder;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TestTask.Models
{
    public class Storage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductStorage> Products { get; set; }
        public List<Transfer> DeliveryIn { get; set; }
        public List<Transfer> DeliveryOut { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TestTask.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductStorage> Storages { get; set; }
        public List<TransferProduct> Transfers { get; set; }
    }
}

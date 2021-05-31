using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TestTask.Models
{
    public class ProductStorage
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int StorageId { get; set; }
        public Storage Storage { get; set; }
        public int Count { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask.Models
{
    public class TransferProduct
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int TransferId { get; set; }
        public Transfer Transfer { get; set; }
        public int Count { get; set; }
    }
}

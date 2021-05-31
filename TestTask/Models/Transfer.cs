using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask.Models
{
    public class Transfer
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int FromStorageId { get; set; }
        public Storage FromStorage { get; set; }
        public int ToStorageId { get; set; }
        public Storage ToStorage { get; set; }
        public List<TransferProduct> Products { get; set; }
    }
}

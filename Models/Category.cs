using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FS_OOP_Practice_V1.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public Category? BaseCategory { get; set; }
        public int ChildCount { get; set; }
        public string Description { get; set; }
        public byte IsActive { get; set; } = 0;
        public int Deleted { get; set; } = 0;
    }
}

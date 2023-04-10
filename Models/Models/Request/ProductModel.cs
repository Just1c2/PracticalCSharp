using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models.Request
{
    public class ProductModel
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public DateTime? ExpDate { get; set; }

        public int Status { get; set; }

        public decimal Price { get; set; }

        public int Amount { get; set; }

        public string? Description { get; set; }
    }
}

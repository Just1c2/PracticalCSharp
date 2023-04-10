using Models.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models.Response
{
    public class CustomerDetailModel
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public int? Age { get; set; }

        public string? Gender { get; set; }

        public string? Address { get; set; }

        public string Username { get; set; } = null!;

        public string? Password { get; set; }

        public int Status { get; set; }

        public decimal? Debit { get; set; }

        public string? Description { get; set; }

        public List<Product>? Products { get; set; }

    }
}

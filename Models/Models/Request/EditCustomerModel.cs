using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models.Request
{
    public class EditCustomerModel
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public int? Age { get; set; }

        public string? Gender { get; set; }

        public string? Address { get; set; }

        public int Status { get; set; }

        public string? Description { get; set; }

        public string? UpdatedBy { get; set; }
    }
}

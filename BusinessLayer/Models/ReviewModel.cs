using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models
{
    public class ReviewModel
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public Guid CustomerId { get; set; }
        public Guid OfferId { get; set; }
    }
}

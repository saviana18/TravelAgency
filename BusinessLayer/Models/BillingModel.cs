using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models
{
    public class BillingModel
    {
        public Guid Id { get; set; }
        public Guid BookingId { get; set; }
        public string AdditionalComments { get; set; }
        public DateTime Date { get; set; }
    }
}

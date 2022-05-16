using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class BillingEntity
    {
        public Guid Id { get; set; }
        public BookingEntity Booking { get; set; }
        public string AdditionalComments { get; set; }
        public DateTime Date { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class BookingEntity
    {
        public Guid Id { get; set; }
        public CustomerEntity Customer { get; set; }
        public OfferEntity Offer { get; set; }
        public int NoOfBookedSeats { get; set; }
        public float TotalPrice { get; set; }
    }
}

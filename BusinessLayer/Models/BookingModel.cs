using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models
{
    public class BookingModel
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid OfferId { get; set; }
        public int NoOfBookedSeats { get; set; }
        public float TotalPrice { get; set; }
    }
}

using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LayersOnWeb.Factory
{
    public interface IWriter
    {
        public void write(List<BillingModel> billings, List<BookingModel> bookings);
    }
}

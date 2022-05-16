using BusinessLayer.Models;
using BusinessLayer.Observer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Observer
{
    public class EmailObserver : IBillingObserver
    {
        public void Update(BillingModel billing)
        {
            Console.WriteLine("Billing no. '{0}' for booking no. '{1}' was exported. An email was sent to customer.",
                billing.Id.ToString(), billing.BookingId.ToString());
        }
    }
}

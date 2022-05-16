using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Contracts;
using BusinessLayer.Interfaces;

namespace LayersOnWeb.Factory
{
    public class ConcreteCreator : Creator
    {
        public IBillingService billingService;
        public IBookingService bookingService;

        public ConcreteCreator(IBillingService billingService, IBookingService bookingService)
        {
            this.billingService = billingService;
            this.bookingService = bookingService;
        }
        public override void createWriter(string method)
        {
            switch(method)
            {
                case "CSV":
                    IWriter csv = new WriterCSV();
                    csv.write(billingService.GetAllBillings(), bookingService.GetAllBookings());
                    break;
                case "XML":
                    IWriter xml = new WriterXML();
                    xml.write(billingService.GetAllBillings(), bookingService.GetAllBookings());
                    break; 
                default:
                    throw new ApplicationException(string.Format("Writer '{0}' cannot be created", method));
            }
        }
    }
}

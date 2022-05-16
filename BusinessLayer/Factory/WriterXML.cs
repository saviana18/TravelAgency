using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using BusinessLayer;
using BusinessLayer.Contracts;
using BusinessLayer.Interfaces;
using BusinessLayer.Models;

namespace LayersOnWeb.Factory
{
    public class WriterXML : IWriter
    {
        public void write(List<BillingModel> billings, List<BookingModel> bookings)
        {
            try
            {
                XmlWriterSettings xmlWriterSettings = new XmlWriterSettings()
                {
                    Indent = true,
                    IndentChars = "\t",
                    NewLineOnAttributes = true
                };

                Random rnd = new Random();
                string path = "C://temp//billing-" + rnd.Next() + ".xml";
                XmlWriter xmlWriter = XmlWriter.Create(path, xmlWriterSettings);

                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("Billing");

                foreach (var booking in bookings)
                {
                    foreach (var billing in billings)
                    {
                        if (booking.Id == billing.BookingId)
                        {
                            xmlWriter.WriteStartElement("BillingId");
                            xmlWriter.WriteAttributeString("OfferId", booking.OfferId.ToString());
                            xmlWriter.WriteAttributeString("CustomerId", booking.CustomerId.ToString());
                            xmlWriter.WriteAttributeString("TotalPrice", booking.TotalPrice.ToString());
                            xmlWriter.WriteAttributeString("AdditionalComments", billing.AdditionalComments.ToString());
                            xmlWriter.WriteAttributeString("Date", billing.Date.ToString());
                            xmlWriter.WriteString(billing.Id.ToString());
                            xmlWriter.WriteEndElement();
                        }
                    }
                }

                xmlWriter.WriteEndDocument();
                xmlWriter.Close();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }
    }
}

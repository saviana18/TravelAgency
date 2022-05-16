using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using BusinessLayer;
using BusinessLayer.Contracts;
using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using CsvHelper;

namespace LayersOnWeb.Factory
{
    public class WriterCSV : IWriter
    {
        public void write(List<BillingModel> billings, List<BookingModel> bookings)
        {
            try
            {
                using var mem = new MemoryStream();
                using var writer = new StreamWriter(mem);
                using var csvWriter = new CsvWriter(writer, CultureInfo.CurrentCulture);

                csvWriter.WriteField("Billing ID");
                csvWriter.WriteField("Offer ID");
                csvWriter.WriteField("Customer ID");
                csvWriter.WriteField("Total price");
                csvWriter.WriteField("Additional comments");
                csvWriter.WriteField("Date");
                csvWriter.NextRecord();

                foreach (var booking in bookings)
                {
                    foreach (var billing in billings)
                    {
                        if (booking.Id == billing.BookingId)
                        {
                            csvWriter.WriteField(billing.Id);
                            csvWriter.WriteField(booking.OfferId);
                            csvWriter.WriteField(booking.CustomerId);
                            csvWriter.WriteField(booking.TotalPrice);
                            csvWriter.WriteField(billing.AdditionalComments);
                            csvWriter.WriteField(billing.Date);
                            csvWriter.NextRecord();
                            csvWriter.NextRecord();
                            csvWriter.NextRecord();
                            csvWriter.NextRecord();
                            csvWriter.NextRecord();
                        }
                    }
                }

                writer.Flush();

                var res = Encoding.UTF8.GetString(mem.ToArray());
                Random rnd = new Random();

                string path = "C://temp//billing-" + rnd.Next() + ".csv";
                File.WriteAllText(path, res);
                Console.WriteLine(res);
            } catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }
    }
}

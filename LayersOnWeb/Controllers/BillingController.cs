using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using LayersOnWeb.Factory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LayersOnWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingController : ControllerBase
    {
        private readonly IBillingService billingService;
        private readonly IBookingService bookingService;

        public BillingController(IBillingService billingService, IBookingService bookingService)
        {
            this.billingService = billingService;
            this.bookingService = bookingService;
        }

        [HttpGet]
        public IEnumerable<BillingModel> Get()
        {
            var result = new List<BillingModel>();
            foreach(var x in billingService.GetAllBillings())
            {
                result.Add(new BillingModel
                {
                    Id = x.Id,
                    BookingId = x.BookingId,
                    AdditionalComments = x.AdditionalComments,
                    Date = x.Date
                });
            }
            return result;
        }

        [HttpPost]
        public IActionResult Post(Guid Id, Guid BookingId, string AdditionalComments, DateTime Date)
        {
            try
            {
                billingService.AddBillingModel(Id, BookingId, AdditionalComments, Date);
                
                return Ok("Added");
            } 
            catch (Exception e)
            {
                _ = e.StackTrace;
                return BadRequest("Couldn't add");
            }
        }

        [HttpGet("GetById")]
        public IEnumerable<BillingModel> GetById(Guid id)
        {
            var result = new List<BillingModel>();
            var x = billingService.GetById(id);
            result.Add(new BillingModel
            {
                Id = x.Id,
                BookingId = x.BookingId,
                AdditionalComments = x.AdditionalComments,
                Date = x.Date
            });

            return result;
        }
        
        [HttpPut("Update")]
        public IActionResult Put(Guid Id, Guid BookingId, string AdditionalComments, DateTime Date)
        {
            try
            {
                billingService.UpdateBillingModel(Id, BookingId, AdditionalComments, Date);

                return Ok();
            }
            catch(Exception e)
            {
                _ = e.StackTrace;
                return BadRequest();
            }
            
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                billingService.DeleteBillingModel(id);

                return Ok();
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
                return BadRequest();
            }
        }

        [HttpGet("ExportDataToCSV")]
        public ActionResult ExportDataToCSV()
        {
            try
            {
                Creator creator = new ConcreteCreator(billingService, bookingService);
                creator.createWriter("CSV");
                return Ok("The billing has been exported successfully!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return BadRequest("Fail");
            }
        }

        [HttpGet("ExportDataToXML")]
        public ActionResult ExportDataToXML()
        {
            try
            {
                Creator creator = new ConcreteCreator(billingService, bookingService);
                creator.createWriter("XML");
                return Ok("The billing has been exported successfully!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return BadRequest("Fail");
            }
        }
    }
}

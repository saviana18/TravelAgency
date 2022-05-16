using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace LayersOnWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService bookingService;
        

        public BookingController(IBookingService bookingService)
        {
            this.bookingService = bookingService;
        }

        [HttpGet]
        public IEnumerable<BookingModel> Get()
        {
            var result = new List<BookingModel>();
            foreach (var x in bookingService.GetAllBookings())
            {
                result.Add(new BookingModel
                {
                    Id = x.Id,
                    CustomerId = x.CustomerId,
                    OfferId = x.OfferId,
                    NoOfBookedSeats = x.NoOfBookedSeats,
                    TotalPrice = x.TotalPrice
                });
            }
            return result;
        }

        [HttpPost]
        public IActionResult Post(Guid Id, Guid CustomerId, Guid OfferId, int NoOfBookedSeats, float TotalPrice)
        {
            try
            {
                string result = bookingService.AddBookingModel(Id, CustomerId, OfferId, NoOfBookedSeats, TotalPrice);
                System.Console.WriteLine(result);
                

                if(result.Equals("There still are available spots for this offer"))
                    return Ok(result);
                else
                    return BadRequest(result);
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
                return BadRequest("Couldn't add");
            }
        }

        [HttpGet("GetById")]
        public IEnumerable<BookingModel> GetById(Guid id)
        {
            var result = new List<BookingModel>();
            var x = bookingService.GetById(id);
            result.Add(new BookingModel
            {
                Id = x.Id,
                CustomerId = x.CustomerId,
                OfferId = x.OfferId,
                NoOfBookedSeats = x.NoOfBookedSeats,
                TotalPrice = x.TotalPrice
            });

            return result;
        }

        [HttpPut("Update")]
        public IActionResult Put(Guid Id, Guid CustomerId, Guid OfferId, int NoOfBookedSeats, float TotalPrice)
        {
            try
            {
                bookingService.UpdateBookingModel(Id, CustomerId, OfferId, NoOfBookedSeats, TotalPrice);

                return Ok();
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
                return BadRequest();
            }

        }

        [HttpDelete("Delete")]
        public IActionResult Delete(Guid id, Guid OfferId)
        {
            try
            {
                bookingService.DeleteBookingModel(id, OfferId);

                return Ok();
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
                return BadRequest();
            }
        }
    }
}

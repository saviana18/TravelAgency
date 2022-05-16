using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IBookingService
    {
        public List<BookingModel> GetAllBookings();
        public BookingModel GetById(Guid id);
        public string AddBookingModel(Guid Id, Guid CustomerId, Guid OfferId, int NoOfBookedSeats, float TotalPrice);
        public void UpdateBookingModel(Guid Id, Guid CustomerId, Guid OfferId, int NoOfBookedSeats, float TotalPrice);
        public void DeleteBookingModel(Guid id, Guid OfferId);
    }
}

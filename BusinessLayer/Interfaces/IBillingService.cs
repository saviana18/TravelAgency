using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Models;
using BusinessLayer.Observer.Interfaces;

namespace BusinessLayer.Interfaces
{
    public interface IBillingService : IBillingNotifier
    {
        public List<BillingModel> GetAllBillings();
        public BillingModel GetById(Guid id);
        public void AddBillingModel(Guid Id, Guid BookingId, string AdditionalComments, DateTime Date);
        public void UpdateBillingModel(Guid Id, Guid BookingId, string AdditionalComments, DateTime Date);
        public void DeleteBillingModel(Guid id);
    }
}

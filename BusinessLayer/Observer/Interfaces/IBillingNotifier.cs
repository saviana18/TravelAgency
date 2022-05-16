using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Observer.Interfaces
{
    public interface IBillingNotifier
    {
        void Attach(IBillingObserver observer);
        void Detach(IBillingObserver observer);
        void Notify(BillingModel billing);
    }
}

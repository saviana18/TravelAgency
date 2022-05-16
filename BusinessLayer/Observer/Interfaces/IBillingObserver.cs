using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Observer.Interfaces
{
    public interface IBillingObserver
    {
        void Update(BillingModel billing);
    }
}

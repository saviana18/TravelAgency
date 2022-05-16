using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IReviewService
    {
        public List<ReviewModel> GetAllReviews();
        public ReviewModel GetById(Guid id);
        public void AddReviewModel(Guid Id, string Message, Guid CustomerId, Guid OfferId);
        public void UpdateReviewModel(Guid Id, string Message, Guid CustomerId, Guid OfferId);
        public void DeleteReviewModel(Guid id);
    }
}

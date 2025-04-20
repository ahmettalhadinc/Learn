using Learn.Core.Models;
using Learn.Core.Repositories;
using Learn.Core.Services;
using Learn.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Service.Services
{
    public class PaymentService : Service<Payment>, IPaymentService
    {
        public PaymentService(IGenericRepository<Payment> repository, IUnitOfWorks unitOfWorks) : base(repository, unitOfWorks)
        {
        }

       
    }
}

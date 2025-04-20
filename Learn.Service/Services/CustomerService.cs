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
    public class CustomerService(IGenericRepository<Customer> repository, IUnitOfWorks unitOfWorks,ICustomerRepository customerRepository) : Service<Customer>(repository, unitOfWorks), ICustomerService
    {
        private readonly ICustomerRepository _customerRepository=customerRepository;
    }
}

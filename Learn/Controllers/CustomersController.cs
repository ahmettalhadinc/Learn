using AutoMapper;
using Learn.API.Filters;
using Learn.Core.DTO;
using Learn.Core.DTO.UpdateDTOs;
using Learn.Core.Models;
using Learn.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Learn.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : CustomBaseController
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        public CustomersController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var customers=_customerService.GetAll();
            var dtos= _mapper.Map<List<CustomerDto>>(customers).ToList();
            return CreateActionResult(CustomResponseDto<List<CustomerDto>>.Success(200,dtos));
        }

        [ServiceFilter(typeof(NotFoundFilter<Customer>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer= await _customerService.GetByIdAsync(id);
            var customerDto=_mapper.Map<CustomerDto>(customer);
            return CreateActionResult(CustomResponseDto<CustomerDto>.Success(200, customerDto));
        }

        [ServiceFilter(typeof(NotFoundFilter<Customer>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> Remove(int id)
        {
            int userId = 1;
            var customer = await _customerService.GetByIdAsync(id);
            customer.UpdatedBy = userId;
            _customerService.ChangeStatus(customer);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpPost]
        public async Task<IActionResult> Save(CustomerDto customerDto)
        {
            int userId = 1;
            var processedEntity= _mapper.Map<Customer>(customerDto);

            processedEntity.UpdatedBy = userId;
            processedEntity.CreatedBy = userId;

            var customer = await _customerService.AddAsync(processedEntity);
            var customerResponseDto = _mapper.Map<CustomerDto>(customer);
            return CreateActionResult(CustomResponseDto<CustomerDto>.Success(201, customerDto));

        }
        [HttpPut]

        public async Task<IActionResult> Update(CustomerUpdateDto customerDto)
        {
            var userId = 1;

            var currentCustomer=await _customerService.GetByIdAsync(customerDto.Id);
            currentCustomer.UpdatedBy = userId;
            currentCustomer.Name = customerDto.Name;
            _customerService.Update(currentCustomer);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}

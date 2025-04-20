using AutoMapper;
using Learn.API.Filters;
using Learn.Core.DTO.UpdateDTOs;
using Learn.Core.DTO;
using Learn.Core.Models;
using Learn.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Learn.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : CustomBaseController
    {
        private readonly ISaleService _saleService;
        private readonly IMapper _mapper;
        public SalesController(ISaleService saleService, IMapper mapper)
        {
            _saleService = saleService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var sales = _saleService.GetAll();
            var dtos = _mapper.Map<List<SaleDto>>(sales).ToList();
            return CreateActionResult(CustomResponseDto<List<SaleDto>>.Success(200, dtos));
        }

        [ServiceFilter(typeof(NotFoundFilter<Sale>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var sale = await _saleService.GetByIdAsync(id);
            var saleDto = _mapper.Map<SaleDto>(sale);
            return CreateActionResult(CustomResponseDto<SaleDto>.Success(200, saleDto));
        }

        [ServiceFilter(typeof(NotFoundFilter<Sale>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> Remove(int id)
        {
            int userId = 1;
            var sale = await _saleService.GetByIdAsync(id);
            sale.UpdatedBy = userId;
            _saleService.ChangeStatus(sale);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpPost]
        public async Task<IActionResult> Save(SaleDto saleDto)
        {
            int userId = 1;
            var processedEntity = _mapper.Map<Sale>(saleDto);

            processedEntity.UpdatedBy = userId;
            processedEntity.CreatedBy = userId;

            var sale = await _saleService.AddAsync(processedEntity);
            var saleResponseDto = _mapper.Map<SaleDto>(sale);
            return CreateActionResult(CustomResponseDto<SaleDto>.Success(201, saleDto));

        }
        [HttpPost("[action]")]
        public async Task<IActionResult> SaleProduct(SaleDto saleDto)
        {
            int userId = 1;
            var processedEntity = _mapper.Map<Sale>(saleDto);

            processedEntity.UpdatedBy = userId;
            processedEntity.CreatedBy = userId;

           await _saleService.SaleProduct(processedEntity);
          
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));

        }
        [HttpPut]

        public async Task<IActionResult> Update(SaleUpdateDto saleDto)
        {
            var userId = 1;

            var currentSale = await _saleService.GetByIdAsync(saleDto.Id);
            currentSale.UpdatedBy = userId;
       
            _saleService.Update(currentSale);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}

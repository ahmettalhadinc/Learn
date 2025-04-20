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
    public class ProductsController : CustomBaseController
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products = _productService.GetAll();
            var dtos = _mapper.Map<List<ProductDto>>(products).ToList();
            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, dtos));
        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            var productDto = _mapper.Map<ProductDto>(product);
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productDto));
        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> Remove(int id)
        {
            int userId = 1;
            var product = await _productService.GetByIdAsync(id);
            product.UpdatedBy = userId;
            _productService.ChangeStatus(product);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> BuyProduct(ProductDto productDto)
        {
            int userId = 1;
            var processedEntity = _mapper.Map<Product>(productDto);

            processedEntity.UpdatedBy = userId;
            processedEntity.CreatedBy = userId;

            await _productService.BuyProduct(processedEntity);
            
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));

        }
        [HttpPut]

        public async Task<IActionResult> Update(ProductUpdateDto productDto)
        {
            var userId = 1;

            var currentProduct = await _productService.GetByIdAsync(productDto.Id);
            currentProduct.UpdatedBy = userId;
            currentProduct.Name = productDto.Name;
            _productService.Update(currentProduct);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }
    }
}

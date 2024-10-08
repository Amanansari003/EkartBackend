using AutoMapper;
using Core.Interfaces;
using Core.Specifications;
using Ekart.Dtos;
using Ekart.Entities;
using Ekart.Errors;
using Ekart.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Ekart.Controllers
{
    public class ProductsController : BaseApiController
    {
        //private readonly IProductRepository _repo;
        private readonly IGenericRepo<ProductBrand> _productBrandRepo;
        private readonly IGenericRepo<ProductType> _productTypeRepo;
        private readonly IGenericRepo<Product> _productsRepo;
        private readonly IMapper _mapper;
        public ProductsController(
            IGenericRepo<Product> productsRepo,
            IGenericRepo<ProductType> productTypeRepo,
            IGenericRepo<ProductBrand> productBrandRepo,
            IMapper mapper)
        {
            _mapper = mapper;
            _productsRepo = productsRepo;
            _productTypeRepo = productTypeRepo;
            _productBrandRepo = productBrandRepo;

        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts(
            [FromQuery] ProductSpecParams productParams)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(productParams);
            var countSpec = new ProductsWithFiltersForCountSpecification(productParams);

            var totalItems = await _productsRepo.CountAsync(countSpec);
            var products = await _productsRepo.ListAsync(spec);
            var data = _mapper.Map<IReadOnlyList<ProductToReturnDto>>(products);
            return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex,
                productParams.PageSize, totalItems, data));
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProductById(int Id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(Id);
            var product = await _productsRepo.GetEntityWithSpec(spec);

            if (product == null) return NotFound(new ApiResponse(404));

            return Ok(_mapper.Map<Product, ProductToReturnDto>(product));
        }

        [HttpPost]
        public ActionResult InsertProdcut(Product product)
        {
            return Ok("Sucessfully added the product in the list");
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var brands = await _productBrandRepo.ListAllAsync();
            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductTypes()
        {
            var types = await _productTypeRepo.ListAllAsync();
            return Ok(types);
        }
    }
}

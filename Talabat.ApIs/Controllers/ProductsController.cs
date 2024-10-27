using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Talabat.ApIs.Dtos;
using Talabat.ApIs.Errors;
using Talabat.ApIs.Helpers;
using Talabat.Core;
using Talabat.Core.Entites;
using Talabat.Core.Repositories;
using Talabat.Core.Specification;

namespace Talabat.ApIs.Controllers
{

    public class ProductsController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
       /* private readonly IGenricRepository<Product> _productRepo;
        private readonly IGenricRepository<ProductBrand> _brandRepo;
        private readonly IGenricRepository<ProductType> _typeRepo;*/

        public ProductsController(
            IUnitOfWork unitOfWork,
            IMapper mapper)
           /* IGenricRepository<Product> productRepo,
            IGenricRepository<ProductBrand> BrandRepo,
            IGenricRepository<ProductType> TypeRepo,*/
        {
          /*  _productRepo = productRepo;
            _brandRepo = BrandRepo;
            _typeRepo = TypeRepo;*/
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

       // [Authorize]
        [HttpGet]
                                                                                        // from qurery here as i send 5 paramater in object from class so i told to srever as take parameter from query params  this process is called REST which meaning sit down here for a temporery time
        public async Task<ActionResult<IReadOnlyList<ProductToRetuenDto>>> GetProducts([FromQuery] ProductSpecParams specParams)
        {
            var spec = new ProductWithBrandAndTypeSpecification(specParams);

            var products = await _unitOfWork.Repository<Product>().GetAllWithSpecAsync(spec);
            return Ok(_mapper.Map<IEnumerable<Product>, IEnumerable<ProductToRetuenDto>>(products));
        }


        [ProducesResponseType(typeof(ProductToRetuenDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToRetuenDto>> GetByIdAsync(int id)
        {
            var spec = new ProductWithBrandAndTypeSpecification(id);

            var product = await _unitOfWork.Repository<Product>().GetByIdWithSpecAsync(spec);
            if (product is null)
                return NotFound(new ApiResponse(404));

            return Ok(_mapper.Map<Product, ProductToRetuenDto>(product));
        }

        [HttpGet("brands")] //  GET : api/products/brands
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brands = await _unitOfWork.Repository<ProductBrand>().GetAllAsync();
            return Ok(brands);
        }

        [HttpGet("types")]  // GET : api/products/types
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
        {
            var types = await _unitOfWork.Repository<ProductType>().GetAllAsync();
            return Ok(types);
        }
    }
}

using BAL.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.ApplicationConfig;
using Model.DTO;
using Repository.UnitOfWork;

namespace retailmanagementAPI.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductService _productService;
        public ProductController(IUnitOfWork unitOfWork, IProductService productService)
        {
            _unitOfWork = unitOfWork;
            _productService = productService;
        }

        [HttpGet("GetAllProduct")]
        public async Task<IActionResult> GetAllProduct()
        {
            
            try
            {
                var productdata = await _unitOfWork.Products.GetAll();
                var activeProducts = productdata.Where(p => p.ActiveFlag).ToList();

                return Ok(new ResponseModel { Message = "Successfully", Status = APIStatus.Successful, Data = activeProducts });
            }

            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.Error });

            }
        }
        [HttpGet("GetProductByID")]
        public async Task<IActionResult> GetProductByID(Guid id)
        {
            try
            {
               var productdata = await _unitOfWork.Products.GetByCondition(x=>x.ProductId == id);
                var activeProducts = productdata.Where(p => p.ActiveFlag).ToList();

                return Ok(new ResponseModel { Message = "Success", Status = APIStatus.Successful,Data=activeProducts });

            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.Error });

                throw;
            }
        }
        
        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(AddProductDTO inputModel)
        {
            try
            {
                await _productService.AddProduct(inputModel);
                return Ok(new ResponseModel { Message = "Add Success", Status = APIStatus.Successful });
            }

            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.Error });

                throw;
            }
        }
        [HttpPost("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(UpdateProductDTO inputModel)
        {
            try
            {
                await _productService.UpdateProduct(inputModel);
                return Ok(new ResponseModel { Message = "Update Success", Status = APIStatus.Successful });
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult>DeleteProduct(DeleteProductDTO inputModel) 
        {
            try
            {
                await _productService.DeleteProduct(inputModel);
                return Ok(new ResponseModel { Message = "Delete Success", Status = APIStatus.Successful });

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}

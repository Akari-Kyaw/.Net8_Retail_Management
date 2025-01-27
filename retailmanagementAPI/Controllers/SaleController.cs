using BAL.IService;
using BAL.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.ApplicationConfig;
using Model.DTO;
using Repository.UnitOfWork;

namespace retailmanagementAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISaleService _saleService;
        public SaleController(IUnitOfWork unitOfWork, ISaleService saleService)
        {
            _unitOfWork = unitOfWork;
            _saleService = saleService;
        }
        [HttpPost("AddSale")]       
            
        public async Task<IActionResult> AddSale(AddSaleDTO inputModel)
        {
            try
            {
                await _saleService.AddSale(inputModel);
                return Ok(new ResponseModel { Message = "Add Success", Status = APIStatus.Successful });
            }

            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.InnerException.Message, Status = APIStatus.Error });

                throw;
            }
        }
        [HttpPost("AddSaleMutiple")]

        public async Task<IActionResult> AddSaleMultiple(IEnumerable<AddSaleDTO> inputModel)
        {
            try
            {
                if (inputModel == null || !inputModel.Any())
                {
                    return BadRequest(new ResponseModel { Message = "Input model cannot be null or empty.", Status = APIStatus.Error });
                }

                await _saleService.AddSaleMultiple(inputModel);
                return Ok(new ResponseModel { Message = "Add Success", Status = APIStatus.Successful });
            }
            //    await _saleService.AddSaleMultiple(inputModel);
            //    return Ok(new ResponseModel { Message = "Add Success", Status = APIStatus.Successful });
            

            catch (Exception ex)
            {
                
                return Ok(new ResponseModel { Message = ex.InnerException.Message, Status = APIStatus.Error });

                throw;
            }
        }
        [HttpGet("GetAllSale")]
        public async Task<IActionResult> GetAllSale()
        {

            try
            {
                var saledata= await _unitOfWork.Sales.GetAll();
                var salereport = saledata.Where(p => p.ActiveFlag).ToList();


                return Ok(new ResponseModel { Message = "Successfully", Status = APIStatus.Successful, Data = salereport });
            }

            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.Error });

                throw;
            }
        }
        [HttpGet("GetSaleByID")]
        public async Task<IActionResult> GetSaleByID(Guid id)
        {
            try
            {
                var saledata = (await _unitOfWork.Sales.GetByCondition(x => x.SaleId == id)).FirstOrDefault();
                if (saledata == null)
                {
                    throw new Exception("Sale Not Found");

                }
                else
                {
                    var salereport = saledata.ActiveFlag == true;

                    return Ok(new ResponseModel { Message = "Success", Status = APIStatus.Successful, Data = saledata });

                }



            


            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.Error });

                throw;
            }
        }
        [HttpGet("ShowProfit")]
        public async Task<IActionResult> ShowProfit()
        {

            try
            {
                var saledata = await _saleService.ShowReport();

                return Ok(new ResponseModel { Message = "Successfully", Status = APIStatus.Successful, Data = saledata });
            }

            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.Error });

                throw;
            }
        }
        [HttpDelete("DeleteSale")]
        public async Task<IActionResult> DeleteReoprt(DeleteReportDTO inputModel)
        {
            try
            {
                await _saleService.DeleteReport(inputModel);
                return Ok(new ResponseModel { Message = "Delete Success", Status = APIStatus.Successful });

            }
            catch (Exception ex)
            {

                throw;
            }
        }


    }
    }



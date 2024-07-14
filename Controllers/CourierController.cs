using Microsoft.AspNetCore.Mvc;
using WMSBackend.DataTransferObject;
using WMSBackend.Interfaces;
using WMSBackend.Models;

namespace WMSBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourierController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourierController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("CreateCourier")]
        public async Task<ActionResult<Courier>> CreateCourier(CourierDto courierDto)
        {
            var newCourier = new Courier
            {
                Name = courierDto.Name,
                Price = courierDto.Price,
                Remark = courierDto.Remark
            };

            var createdCourier = await _unitOfWork.CourierRepository.AddAsync(newCourier);
            await _unitOfWork.CommitAsync();

            return Ok(createdCourier);
        }

        [HttpGet]
        [Route("GetAllCouriers")]
        public async Task<ActionResult<List<Courier>>> GetAllCouriers(bool isGetRelations)
        {
            var allCouriers = await _unitOfWork.CourierRepository.GetAllAsync(isGetRelations);

            return Ok(allCouriers);
        }

        [HttpGet]
        [Route("GetCourier")]
        public async Task<ActionResult<Courier>> GetCourier(int id, bool isGetRelations)
        {
            var foundCourier = await _unitOfWork.CourierRepository.GetAsync(id, isGetRelations);
            if (foundCourier == null)
            {
                return NotFound("Courier not found");
            }

            return Ok(foundCourier);
        }

        [HttpPut]
        [Route("UpdateCourier")]
        public async Task<ActionResult<bool>> UpdateCourier(int id, CourierDto courierDto)
        {
            var foundCourier = await _unitOfWork.CourierRepository.GetAsync(id, false);
            if (foundCourier == null)
            {
                return NotFound("Courier not found");
            }
            foundCourier.Name = courierDto.Name;
            foundCourier.Price = courierDto.Price;
            foundCourier.Remark = courierDto.Remark;

            var success = await _unitOfWork.CourierRepository.UpdateAsync(foundCourier);
            var saveSuccess = await _unitOfWork.CommitAsync();

            return Ok(success && saveSuccess > 0);
        }

        [HttpDelete]
        [Route("DeleteCourier")]
        public async Task<ActionResult<bool>> DeleteCourier(int id)
        {
            var foundCourier = await _unitOfWork.CourierRepository.GetAsync(id, false);
            if (foundCourier == null)
            {
                return NotFound("Courier not found");
            }

            var success = await _unitOfWork.CourierRepository.DeleteAsync(id);
            var saveSuccess = await _unitOfWork.CommitAsync();

            return Ok(success && saveSuccess > 0);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        [Route("CreateCourierProductRelationship")]
        public async Task<ActionResult<Courier>> CreateCourierProductRelationship(
            CourierCustomerOrderDto courierCustomerOrderDto
        )
        {
            var foundCourier = await _unitOfWork.CourierRepository.GetAsync(
                courierCustomerOrderDto.CourierId,
                true
            );
            if (foundCourier == null)
            {
                return NotFound("Courier not found");
            }

            var foundCustomerOrder = await _unitOfWork.CustomerOrderRepository.GetAsync(
                courierCustomerOrderDto.CustomerOrderId,
                false,
                false,
                true,
                false
            );
            if (foundCustomerOrder == null)
            {
                return NotFound("Customer Order not found");
            }

            foundCourier.CustomerOrders.Add(foundCustomerOrder);
            await _unitOfWork.CommitAsync();

            return Ok(foundCourier);
        }

        [HttpPost]
        [Route("DeleteCourierProductRelationship")]
        public async Task<ActionResult<Courier>> DeleteCourierProductRelationship(
            CourierCustomerOrderDto courierCustomerOrderDto
        )
        {
            var foundCourier = await _unitOfWork.CourierRepository.GetAsync(
                courierCustomerOrderDto.CourierId,
                true
            );
            if (foundCourier == null)
            {
                return NotFound("Courier not found");
            }

            var foundCustomerOrder = await _unitOfWork.CustomerOrderRepository.GetAsync(
                courierCustomerOrderDto.CustomerOrderId,
                false,
                false,
                true,
                false
            );
            if (foundCustomerOrder == null)
            {
                return NotFound("Customer Order not found");
            }

            foundCourier.CustomerOrders.Remove(foundCustomerOrder);
            await _unitOfWork.CommitAsync();

            return Ok(foundCourier);
        }
    }
}

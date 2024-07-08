using Microsoft.AspNetCore.Mvc;
using WMSBackend.DataTransferObject;
using WMSBackend.Interfaces;
using WMSBackend.Models;

namespace WMSBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerOrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerOrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("CreateCustomerOrder")]
        public async Task<ActionResult<CustomerOrder>> CreateCustomerOrder(
            CustomerOrderDto customerOrderDto
        )
        {
            var newCustomerOrder = new CustomerOrder
            {
                ExpectedArrivalDate = customerOrderDto.ExpectedArrivalDate,
                OrderCreationDate = customerOrderDto.OrderCreationDate,
                OrderAddress = customerOrderDto.OrderAddress
            };

            var createdCustomerOrder = await _unitOfWork.CustomerOrderRepository.AddAsync(
                newCustomerOrder
            );
            await _unitOfWork.CommitAsync();

            return Ok(createdCustomerOrder);
        }

        [HttpGet]
        [Route("GetAllCustomerOrders")]
        public async Task<ActionResult<List<CustomerOrder>>> GetAllCustomerOrders(
            bool isGetRelations
        )
        {
            var allCustomerOrders = await _unitOfWork.CustomerOrderRepository.GetAllAsync(
                isGetRelations
            );

            return Ok(allCustomerOrders);
        }

        [HttpGet]
        [Route("GetCustomerOrder/{id}")]
        public async Task<ActionResult<CustomerOrder>> GetCustomerOrder(int id, bool isGetRelations)
        {
            var foundCustomerOrder = await _unitOfWork.CustomerOrderRepository.GetAsync(
                id,
                isGetRelations
            );
            if (foundCustomerOrder == null)
            {
                return NotFound("Customer Order not found");
            }

            return Ok(foundCustomerOrder);
        }

        [HttpPut]
        [Route("UpdateCustomerOrder")]
        public async Task<ActionResult<bool>> UpdateCustomerOrder(
            int id,
            CustomerOrderDto customerOrderDto
        )
        {
            var foundCustomerOrder = await _unitOfWork.CustomerOrderRepository.GetAsync(id, false);
            if (foundCustomerOrder == null)
            {
                return NotFound("Customer Order not found");
            }

            foundCustomerOrder.ExpectedArrivalDate = customerOrderDto.ExpectedArrivalDate;
            foundCustomerOrder.OrderCreationDate = customerOrderDto.OrderCreationDate;
            foundCustomerOrder.OrderAddress = customerOrderDto.OrderAddress;

            var success = await _unitOfWork.CustomerOrderRepository.UpdateAsync(foundCustomerOrder);
            var saveSuccess = await _unitOfWork.CommitAsync();

            return Ok(success && saveSuccess > 0);
        }

        [HttpDelete]
        [Route("DeleteCustomerOrder")]
        public async Task<ActionResult<bool>> DeleteCustomerOrder(int id)
        {
            var foundCustomerOrder = await _unitOfWork.CustomerOrderRepository.GetAsync(id, false);
            if (foundCustomerOrder == null)
            {
                return NotFound("Customer Order not found");
            }

            var success = await _unitOfWork.CustomerOrderRepository.Delete(id);
            var saveSuccess = await _unitOfWork.CommitAsync();

            return Ok(success && saveSuccess > 0);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        [Route("CreateCustomerOrderDetail")]
        public async Task<ActionResult<CustomerOrderDetail>> CreateCustomerOrderDetail(
            CustomerOrderDetailDto customerOrderDetailDto
        )
        {
            var newCustomerOrderDetail = new CustomerOrderDetail
            {
                Quantity = customerOrderDetailDto.Quantity,
                Status = customerOrderDetailDto.Status,
                OrderBin = customerOrderDetailDto.OrderBin
            };

            var createdCustomerOrderDetail =
                await _unitOfWork.CustomerOrderDetailRepository.AddAsync(newCustomerOrderDetail);
            await _unitOfWork.CommitAsync();

            return Ok(createdCustomerOrderDetail);
        }

        [HttpGet]
        [Route("GetAllCustomerOrderDetails")]
        public async Task<ActionResult<List<CustomerOrderDetail>>> GetAllCustomerOrderDetails(
            int customerOrderId,
            bool isGetRelations
        )
        {
            var customerOrderDetails = await _unitOfWork.CustomerOrderDetailRepository.FindAsync(
                x => x.CustomerOrderId == customerOrderId,
                isGetRelations
            );

            return Ok(customerOrderDetails);
        }

        [HttpGet]
        [Route("GetCustomerOrderDetail/{id}")]
        public async Task<ActionResult<CustomerOrderDetail>> GetCustomerOrderDetail(
            int id,
            bool isGetRelations
        )
        {
            var foundCustomerOrderDetail = await _unitOfWork.CustomerOrderDetailRepository.GetAsync(
                id,
                isGetRelations
            );
            if (foundCustomerOrderDetail == null)
            {
                return NotFound("Customer Order Detail not found");
            }

            return Ok(foundCustomerOrderDetail);
        }

        [HttpPut]
        [Route("UpdateCustomerOrderDetail")]
        public async Task<ActionResult<bool>> UpdateCustomerOrderDetail(
            int id,
            CustomerOrderDetailDto customerOrderDetailDto
        )
        {
            var foundCustomerOrderDetail = await _unitOfWork.CustomerOrderDetailRepository.GetAsync(
                id,
                false
            );
            if (foundCustomerOrderDetail == null)
            {
                return NotFound("Customer Order Detail not found");
            }

            foundCustomerOrderDetail.Quantity = customerOrderDetailDto.Quantity;
            foundCustomerOrderDetail.Status = customerOrderDetailDto.Status;
            foundCustomerOrderDetail.OrderBin = customerOrderDetailDto.OrderBin;

            var success = await _unitOfWork.CustomerOrderDetailRepository.UpdateAsync(
                foundCustomerOrderDetail
            );
            var saveSuccess = await _unitOfWork.CommitAsync();

            return Ok(success && saveSuccess > 0);
        }

        [HttpDelete]
        [Route("DeleteCustomerOrderDetail")]
        public async Task<ActionResult<bool>> DeleteCustomerOrderDetail(int id)
        {
            var foundCustomerOrderDetail = await _unitOfWork.CustomerOrderDetailRepository.GetAsync(
                id,
                false
            );
            if (foundCustomerOrderDetail == null)
            {
                return NotFound("Customer Order Detail not found");
            }

            var success = await _unitOfWork.CustomerOrderDetailRepository.Delete(id);
            var saveSuccess = await _unitOfWork.CommitAsync();

            return Ok(success && saveSuccess > 0);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        [Route("CreateCustomerOrderCustomerOrderDetailRelationship")]
        public async Task<
            ActionResult<CustomerOrder>
        > CreateCustomerOrderCustomerOrderDetailRelationship(
            CustomerOrderCustomerOrderDetailDto customerOrderCustomerOrderDetailDto
        )
        {
            var foundCustomerOrder = await _unitOfWork.CustomerOrderRepository.GetAsync(
                customerOrderCustomerOrderDetailDto.CustomerOrderId,
                true
            );
            if (foundCustomerOrder == null)
            {
                return NotFound("Customer Order not found");
            }

            var foundCustomerOrderDetail = await _unitOfWork.CustomerOrderDetailRepository.GetAsync(
                customerOrderCustomerOrderDetailDto.CustomerOrderDetailId,
                true,
                false
            );
            if (foundCustomerOrderDetail == null)
            {
                return NotFound("Customer Order Detail not found");
            }

            foundCustomerOrder.CustomerOrderDetails.Add(foundCustomerOrderDetail);
            await _unitOfWork.CommitAsync();

            return Ok(foundCustomerOrder);
        }

        [HttpDelete]
        [Route("DeleteCustomerOrderCustomerOrderDetailRelationship")]
        public async Task<ActionResult<bool>> DeleteCustomerOrderCustomerOrderDetailRelationship(
            CustomerOrderCustomerOrderDetailDto customerOrderCustomerOrderDetailDto
        )
        {
            var foundCustomerOrder = await _unitOfWork.CustomerOrderRepository.GetAsync(
                customerOrderCustomerOrderDetailDto.CustomerOrderId,
                true
            );
            if (foundCustomerOrder == null)
            {
                return NotFound("Customer Order not found");
            }

            var foundCustomerOrderDetail = await _unitOfWork.CustomerOrderDetailRepository.GetAsync(
                customerOrderCustomerOrderDetailDto.CustomerOrderDetailId,
                true,
                false
            );
            if (foundCustomerOrderDetail == null)
            {
                return NotFound("Customer Order Detail not found");
            }

            foundCustomerOrder.CustomerOrderDetails.Remove(foundCustomerOrderDetail);
            await _unitOfWork.CommitAsync();

            return Ok(foundCustomerOrder);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        [Route("CreateCustomerOrderDetailProductRelationship")]
        public async Task<ActionResult<CustomerOrder>> CreateCustomerOrderDetailProductRelationship(
            CustomerOrderDetailProductDto customerOrderDetailProductDto
        )
        {
            var foundCustomerOrderDetail = await _unitOfWork.CustomerOrderDetailRepository.GetAsync(
                customerOrderDetailProductDto.CustomerOrderDetailId,
                false,
                true
            );
            if (foundCustomerOrderDetail == null)
            {
                return NotFound("Customer Order Detail not found");
            }

            var foundProduct = await _unitOfWork.CustomerOrderRepository.GetAsync(
                customerOrderDetailProductDto.ProductId,
                true
            );
            if (foundProduct == null)
            {
                return NotFound("Product not found");
            }

            foundProduct.CustomerOrderDetails.Add(foundCustomerOrderDetail);
            await _unitOfWork.CommitAsync();

            return Ok(foundCustomerOrderDetail);
        }

        [HttpDelete]
        [Route("CreateCustomerOrderDetailProductRelationship")]
        public async Task<ActionResult<bool>> DeleteCustomerOrderDetailProductRelationship(
            CustomerOrderDetailProductDto customerOrderDetailProductDto
        )
        {
            var foundCustomerOrderDetail = await _unitOfWork.CustomerOrderDetailRepository.GetAsync(
                customerOrderDetailProductDto.CustomerOrderDetailId,
                false,
                true
            );
            if (foundCustomerOrderDetail == null)
            {
                return NotFound("Customer Order Detail not found");
            }

            var foundProduct = await _unitOfWork.CustomerOrderRepository.GetAsync(
                customerOrderDetailProductDto.ProductId,
                true
            );
            if (foundProduct == null)
            {
                return NotFound("Product not found");
            }

            foundProduct.CustomerOrderDetails.Remove(foundCustomerOrderDetail);
            await _unitOfWork.CommitAsync();

            return Ok(foundCustomerOrderDetail);
        }
    }
}

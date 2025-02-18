﻿using Microsoft.AspNetCore.Mvc;
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
        [Route("GetCustomerOrder")]
        public async Task<ActionResult<CustomerOrder>> GetCustomerOrder(
            Guid id,
            bool isGetRelations
        )
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
            Guid id,
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
        public async Task<ActionResult<bool>> DeleteCustomerOrder(Guid id)
        {
            var foundCustomerOrder = await _unitOfWork.CustomerOrderRepository.GetAsync(id, false);
            if (foundCustomerOrder == null)
            {
                return NotFound("Customer Order not found");
            }

            var success = await _unitOfWork.CustomerOrderRepository.DeleteAsync(id);
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
        [Route("GetCustomerOrderDetail")]
        public async Task<ActionResult<CustomerOrderDetail>> GetCustomerOrderDetail(
            Guid id,
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
            Guid id,
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

            var success = await _unitOfWork.CustomerOrderDetailRepository.UpdateAsync(
                foundCustomerOrderDetail
            );
            var saveSuccess = await _unitOfWork.CommitAsync();

            return Ok(success && saveSuccess > 0);
        }

        [HttpDelete]
        [Route("DeleteCustomerOrderDetail")]
        public async Task<ActionResult<bool>> DeleteCustomerOrderDetail(Guid id)
        {
            var foundCustomerOrderDetail = await _unitOfWork.CustomerOrderDetailRepository.GetAsync(
                id,
                false
            );
            if (foundCustomerOrderDetail == null)
            {
                return NotFound("Customer Order Detail not found");
            }

            var success = await _unitOfWork.CustomerOrderDetailRepository.DeleteAsync(id);
            var saveSuccess = await _unitOfWork.CommitAsync();

            return Ok(success && saveSuccess > 0);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        [Route("CreateBin")]
        public async Task<ActionResult<Bin>> CreateBin(BinDto binDto)
        {
            var newBin = new Bin { Name = binDto.Name, };

            var createdBin = await _unitOfWork.BinRepository.AddAsync(newBin);
            await _unitOfWork.CommitAsync();

            return Ok(createdBin);
        }

        [HttpGet]
        [Route("GetAllBins")]
        public async Task<ActionResult<List<Bin>>> GetAllBins(Guid binId, bool isGetRelations)
        {
            var bins = await _unitOfWork.BinRepository.FindAsync(
                x => x.Id == binId,
                isGetRelations
            );

            return Ok(bins);
        }

        [HttpGet]
        [Route("GetBin")]
        public async Task<ActionResult<Bin>> GetBin(Guid id, bool isGetRelations)
        {
            var foundBin = await _unitOfWork.BinRepository.GetAsync(id, isGetRelations);
            if (foundBin == null)
            {
                return NotFound("Bin not found");
            }

            return Ok(foundBin);
        }

        [HttpPut]
        [Route("UpdateBin")]
        public async Task<ActionResult<bool>> UpdateBin(Guid id, BinDto binDto)
        {
            var foundBin = await _unitOfWork.BinRepository.GetAsync(id, false);
            if (foundBin == null)
            {
                return NotFound("Bin not found");
            }
            foundBin.Name = binDto.Name;

            var success = await _unitOfWork.BinRepository.UpdateAsync(foundBin);
            var saveSuccess = await _unitOfWork.CommitAsync();

            return Ok(success && saveSuccess > 0);
        }

        [HttpDelete]
        [Route("DeleteBin")]
        public async Task<ActionResult<bool>> DeleteBin(Guid id)
        {
            var foundBin = await _unitOfWork.BinRepository.GetAsync(id, false);
            if (foundBin == null)
            {
                return NotFound("Bin not found");
            }

            var success = await _unitOfWork.BinRepository.DeleteAsync(id);
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

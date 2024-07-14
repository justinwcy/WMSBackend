using Microsoft.AspNetCore.Mvc;
using WMSBackend.DataTransferObject;
using WMSBackend.Interfaces;
using WMSBackend.Models;

namespace WMSBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefundOrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public RefundOrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("CreateRefundOrder")]
        public async Task<ActionResult<RefundOrder>> CreateRefundOrder(
            RefundOrderDto refundOrderDto
        )
        {
            var newRefundOrder = new RefundOrder
            {
                RefundReason = refundOrderDto.RefundReason,
                Status = refundOrderDto.Status,
            };

            var createdRefundOrder = await _unitOfWork.RefundOrderRepository.AddAsync(
                newRefundOrder
            );
            await _unitOfWork.CommitAsync();

            return Ok(createdRefundOrder);
        }

        [HttpGet]
        [Route("GetAllRefundOrders")]
        public async Task<ActionResult<List<RefundOrder>>> GetAllRefundOrders(bool isGetRelations)
        {
            var allRefundOrders = await _unitOfWork.RefundOrderRepository.GetAllAsync(
                isGetRelations
            );

            return Ok(allRefundOrders);
        }

        [HttpGet]
        [Route("GetRefundOrder")]
        public async Task<ActionResult<RefundOrder>> GetRefundOrder(int id, bool isGetRelations)
        {
            var foundRefundOrder = await _unitOfWork.RefundOrderRepository.GetAsync(
                id,
                isGetRelations
            );
            if (foundRefundOrder == null)
            {
                return NotFound("Refund Order not found");
            }

            return Ok(foundRefundOrder);
        }

        [HttpPut]
        [Route("UpdateRefundOrder")]
        public async Task<ActionResult<bool>> UpdateRefundOrder(
            int id,
            RefundOrderDto refundOrderDto
        )
        {
            var foundRefundOrder = await _unitOfWork.RefundOrderRepository.GetAsync(id, false);
            if (foundRefundOrder == null)
            {
                return NotFound("Refund Order not found");
            }

            foundRefundOrder.RefundReason = refundOrderDto.RefundReason;
            foundRefundOrder.Status = refundOrderDto.Status;

            var success = await _unitOfWork.RefundOrderRepository.UpdateAsync(foundRefundOrder);
            var saveSuccess = await _unitOfWork.CommitAsync();

            return Ok(success && saveSuccess > 0);
        }

        [HttpDelete]
        [Route("DeleteRefundOrder")]
        public async Task<ActionResult<bool>> DeleteRefundOrder(int id)
        {
            var foundRefundOrder = await _unitOfWork.RefundOrderRepository.GetAsync(id, false);
            if (foundRefundOrder == null)
            {
                return NotFound("Refund Order not found");
            }

            var success = await _unitOfWork.RefundOrderRepository.DeleteAsync(id);
            var saveSuccess = await _unitOfWork.CommitAsync();

            return Ok(success && saveSuccess > 0);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        [Route("CreateRefundOrderProductRelationship")]
        public async Task<ActionResult<RefundOrderProduct>> CreateRefundOrderProductRelationship(
            RefundOrderProductDto refundOrderProductDto
        )
        {
            var foundRefundOrder = await _unitOfWork.RefundOrderRepository.GetAsync(
                refundOrderProductDto.RefundOrderId,
                true
            );
            if (foundRefundOrder == null)
            {
                return NotFound("Refund Order not found");
            }

            var foundProduct = await _unitOfWork.ProductRepository.GetAsync(
                refundOrderProductDto.ProductId,
                true
            );
            if (foundProduct == null)
            {
                return NotFound("Product not found");
            }

            var refundOrderProduct = new RefundOrderProduct()
            {
                RefundOrderId = refundOrderProductDto.RefundOrderId,
                ProductId = refundOrderProductDto.ProductId,
                Quantity = refundOrderProductDto.Quantity,
                Status = refundOrderProductDto.Status
            };

            var createdRefundOrderProduct = await _unitOfWork.RefundOrderProductRepository.AddAsync(
                refundOrderProduct
            );

            foundRefundOrder.Products.Add(foundProduct);
            await _unitOfWork.CommitAsync();

            return Ok(createdRefundOrderProduct);
        }

        [HttpPost]
        [Route("UpdateRefundOrderProductRelationship")]
        public async Task<ActionResult<RefundOrderProduct>> UpdateRefundOrderProductRelationship(
            RefundOrderProductDto refundOrderProductDto
        )
        {
            var foundRefundOrderProducts = await _unitOfWork.RefundOrderProductRepository.FindAsync(
                refundOrderProduct =>
                    refundOrderProduct.RefundOrderId == refundOrderProductDto.RefundOrderId
                    && refundOrderProduct.ProductId == refundOrderProductDto.ProductId,
                false
            );

            if (foundRefundOrderProducts.Any())
            {
                return BadRequest("RefundOrderProduct Relationship not found");
            }

            if (foundRefundOrderProducts.Count() > 1)
            {
                return BadRequest(
                    "Multiple RefundOrderProduct Relationship found, should be unique!"
                );
            }

            var foundRefundOrderProduct = foundRefundOrderProducts.First();
            foundRefundOrderProduct.Quantity = refundOrderProductDto.Quantity;
            foundRefundOrderProduct.Status = refundOrderProductDto.Status;

            await _unitOfWork.CommitAsync();

            return Ok(foundRefundOrderProduct);
        }

        [HttpPost]
        [Route("DeleteRefundOrderProductRelationship")]
        public async Task<ActionResult<RefundOrder>> DeleteRefundOrderProductRelationship(
            RefundOrderProductDto refundOrderProductDto
        )
        {
            var foundRefundOrder = await _unitOfWork.RefundOrderRepository.GetAsync(
                refundOrderProductDto.RefundOrderId,
                true
            );
            if (foundRefundOrder == null)
            {
                return NotFound("Refund Order not found");
            }

            var foundProduct = await _unitOfWork.ProductRepository.GetAsync(
                refundOrderProductDto.ProductId,
                false,
                true,
                false,
                false,
                false,
                false
            );
            if (foundProduct == null)
            {
                return NotFound("Product not found");
            }

            var foundRefundOrderProducts = await _unitOfWork.RefundOrderProductRepository.FindAsync(
                refundOrderProduct =>
                    refundOrderProduct.RefundOrderId == refundOrderProductDto.RefundOrderId
                    && refundOrderProduct.ProductId == refundOrderProductDto.ProductId,
                false
            );

            if (foundRefundOrderProducts == null)
            {
                return BadRequest("RefundOrderProduct Relationship not found");
            }

            if (foundRefundOrderProducts.Count() > 1)
            {
                return BadRequest(
                    "Multiple RefundOrderProduct Relationship found, should be unique!"
                );
            }

            var success = await _unitOfWork.RefundOrderProductRepository.DeleteAsync(
                foundRefundOrderProducts.First().Id
            );

            foundRefundOrder.Products.Remove(foundProduct);
            await _unitOfWork.CommitAsync();

            return Ok(foundRefundOrder);
        }
    }
}

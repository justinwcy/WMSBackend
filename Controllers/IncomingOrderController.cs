using Microsoft.AspNetCore.Mvc;
using WMSBackend.DataTransferObject;
using WMSBackend.Interfaces;
using WMSBackend.Models;

namespace WMSBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomingOrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public IncomingOrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("CreateIncomingOrder")]
        public async Task<ActionResult<IncomingOrder>> CreateIncomingOrder(
            IncomingOrderDto incomingOrderDto
        )
        {
            var newIncomingOrder = new IncomingOrder
            {
                IncomingDate = incomingOrderDto.IncomingDate,
                ReceivingDate = incomingOrderDto.ReceivingDate,
                Status = incomingOrderDto.Status,
                PONumber = incomingOrderDto.PONumber
            };

            var createdIncomingOrder = await _unitOfWork.IncomingOrderRepository.AddAsync(
                newIncomingOrder
            );
            await _unitOfWork.CommitAsync();

            return Ok(createdIncomingOrder);
        }

        [HttpGet]
        [Route("GetAllIncomingOrders")]
        public async Task<ActionResult<List<IncomingOrder>>> GetAllIncomingOrders(
            bool isGetRelations
        )
        {
            var allIncomingOrders = await _unitOfWork.IncomingOrderRepository.GetAllAsync(
                isGetRelations
            );

            return Ok(allIncomingOrders);
        }

        [HttpGet]
        [Route("GetIncomingOrder")]
        public async Task<ActionResult<IncomingOrder>> GetIncomingOrder(
            Guid id,
            bool isGetRelations
        )
        {
            var foundIncomingOrder = await _unitOfWork.IncomingOrderRepository.GetAsync(
                id,
                isGetRelations
            );
            if (foundIncomingOrder == null)
            {
                return NotFound("Incoming Order not found");
            }

            return Ok(foundIncomingOrder);
        }

        [HttpPut]
        [Route("UpdateIncomingOrder")]
        public async Task<ActionResult<bool>> UpdateIncomingOrder(
            Guid id,
            IncomingOrderDto incomingOrderDto
        )
        {
            var foundIncomingOrder = await _unitOfWork.IncomingOrderRepository.GetAsync(id, false);
            if (foundIncomingOrder == null)
            {
                return NotFound("Incoming Order not found");
            }

            foundIncomingOrder.IncomingDate = incomingOrderDto.IncomingDate;
            foundIncomingOrder.Status = incomingOrderDto.Status;

            var success = await _unitOfWork.IncomingOrderRepository.UpdateAsync(foundIncomingOrder);
            var saveSuccess = await _unitOfWork.CommitAsync();

            return Ok(success && saveSuccess > 0);
        }

        [HttpDelete]
        [Route("DeleteIncomingOrder")]
        public async Task<ActionResult<bool>> DeleteIncomingOrder(Guid id)
        {
            var foundIncomingOrder = await _unitOfWork.IncomingOrderRepository.GetAsync(id, false);
            if (foundIncomingOrder == null)
            {
                return NotFound("Incoming Order not found");
            }

            var success = await _unitOfWork.IncomingOrderRepository.DeleteAsync(id);
            var saveSuccess = await _unitOfWork.CommitAsync();

            return Ok(success && saveSuccess > 0);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        [Route("CreateIncomingOrderProductRelationship")]
        public async Task<
            ActionResult<IncomingOrderProduct>
        > CreateIncomingOrderProductRelationship(IncomingOrderProductDto incomingOrderProductDto)
        {
            var foundIncomingOrder = await _unitOfWork.IncomingOrderRepository.GetAsync(
                incomingOrderProductDto.IncomingOrderId,
                true
            );
            if (foundIncomingOrder == null)
            {
                return NotFound("Incoming Order not found");
            }

            var foundProduct = await _unitOfWork.ProductRepository.GetAsync(
                incomingOrderProductDto.ProductId,
                true,
                false,
                false,
                false,
                false,
                false
            );

            if (foundProduct == null)
            {
                return NotFound("Product not found");
            }

            var incomingOrderProduct = new IncomingOrderProduct()
            {
                IncomingOrderId = incomingOrderProductDto.IncomingOrderId,
                ProductId = incomingOrderProductDto.ProductId,
                Quantity = incomingOrderProductDto.Quantity,
                Status = incomingOrderProductDto.Status
            };

            var createdIncomingOrderProduct =
                await _unitOfWork.IncomingOrderProductRepository.AddAsync(incomingOrderProduct);
            foundIncomingOrder.Products.Add(foundProduct);
            await _unitOfWork.CommitAsync();

            return Ok(createdIncomingOrderProduct);
        }

        [HttpPost]
        [Route("UpdateIncomingOrderProductRelationship")]
        public async Task<
            ActionResult<IncomingOrderProduct>
        > UpdateIncomingOrderProductRelationship(IncomingOrderProductDto incomingOrderProductDto)
        {
            var foundIncomingOrderProducts =
                await _unitOfWork.IncomingOrderProductRepository.FindAsync(
                    incomingOrderProduct =>
                        incomingOrderProduct.IncomingOrderId
                            == incomingOrderProductDto.IncomingOrderId
                        && incomingOrderProduct.ProductId == incomingOrderProductDto.ProductId,
                    false
                );

            if (foundIncomingOrderProducts.Any())
            {
                return BadRequest("IncomingOrderProduct Relationship not found");
            }

            if (foundIncomingOrderProducts.Count() > 1)
            {
                return BadRequest(
                    "Multiple IncomingOrderProduct Relationship found, should be unique!"
                );
            }

            var foundIncomingOrderProduct = foundIncomingOrderProducts.First();
            foundIncomingOrderProduct.Quantity = incomingOrderProductDto.Quantity;
            foundIncomingOrderProduct.Status = incomingOrderProductDto.Status;

            await _unitOfWork.CommitAsync();

            return Ok(foundIncomingOrderProduct);
        }

        [HttpPost]
        [Route("DeleteIncomingOrderProductRelationship")]
        public async Task<ActionResult<bool>> DeleteIncomingOrderProductRelationship(
            IncomingOrderProductDto incomingOrderProductDto
        )
        {
            var foundIncomingOrder = await _unitOfWork.IncomingOrderRepository.GetAsync(
                incomingOrderProductDto.IncomingOrderId,
                true
            );
            if (foundIncomingOrder == null)
            {
                return NotFound("Incoming Order not found");
            }

            var foundProduct = await _unitOfWork.ProductRepository.GetAsync(
                incomingOrderProductDto.ProductId,
                true,
                false,
                false,
                false,
                false,
                false
            );
            if (foundProduct == null)
            {
                return NotFound("Product not found");
            }

            var foundIncomingOrderProducts =
                await _unitOfWork.IncomingOrderProductRepository.FindAsync(
                    incomingOrderProduct =>
                        incomingOrderProduct.IncomingOrderId
                            == incomingOrderProductDto.IncomingOrderId
                        && incomingOrderProduct.ProductId == incomingOrderProductDto.ProductId,
                    false
                );

            if (foundIncomingOrderProducts == null)
            {
                return BadRequest("IncomingOrderProduct Relationship not found");
            }

            if (foundIncomingOrderProducts.Count() > 1)
            {
                return BadRequest(
                    "Multiple IncomingOrderProduct Relationship found, should be unique!"
                );
            }

            var success = await _unitOfWork.IncomingOrderProductRepository.DeleteAsync(
                foundIncomingOrderProducts.First().Id
            );
            foundIncomingOrder.Products.Remove(foundProduct);
            await _unitOfWork.CommitAsync();

            return Ok(success);
        }
    }
}

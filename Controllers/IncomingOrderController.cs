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
                Status = incomingOrderDto.Status,
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
        [Route("GetIncomingOrder/{id}")]
        public async Task<ActionResult<IncomingOrder>> GetIncomingOrder(int id, bool isGetRelations)
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
            int id,
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
        public async Task<ActionResult<bool>> DeleteIncomingOrder(int id)
        {
            var foundIncomingOrder = await _unitOfWork.IncomingOrderRepository.GetAsync(id, false);
            if (foundIncomingOrder == null)
            {
                return NotFound("Incoming Order not found");
            }

            var success = await _unitOfWork.IncomingOrderRepository.Delete(id);
            var saveSuccess = await _unitOfWork.CommitAsync();

            return Ok(success && saveSuccess > 0);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        [Route("CreateIncomingOrderProductRelationship")]
        public async Task<ActionResult<IncomingOrder>> CreateIncomingOrderProductRelationship(
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

            foundIncomingOrder.Products.Add(foundProduct);
            await _unitOfWork.CommitAsync();

            return Ok(foundIncomingOrder);
        }

        [HttpPost]
        [Route("DeleteIncomingOrderProductRelationship")]
        public async Task<ActionResult<IncomingOrder>> DeleteIncomingOrderProductRelationship(
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

            foundIncomingOrder.Products.Remove(foundProduct);
            await _unitOfWork.CommitAsync();

            return Ok(foundIncomingOrder);
        }
    }
}

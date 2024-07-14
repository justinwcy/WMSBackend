using Microsoft.AspNetCore.Mvc;
using WMSBackend.DataTransferObject;
using WMSBackend.Interfaces;
using WMSBackend.Models;

namespace WMSBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("CreateProduct")]
        public async Task<ActionResult<Product>> CreateProduct(ProductDto productDto)
        {
            var newProduct = new Product
            {
                Sku = productDto.Sku,
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                Tag = productDto.Tag,
                Weight = productDto.Weight,
                Height = productDto.Height,
                Length = productDto.Length,
                Width = productDto.Width
            };

            var createdProduct = await _unitOfWork.ProductRepository.AddAsync(newProduct);
            await _unitOfWork.CommitAsync();

            return Ok(createdProduct);
        }

        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<ActionResult<List<Product>>> GetAllProducts(bool isGetRelations)
        {
            var allProducts = await _unitOfWork.ProductRepository.GetAllAsync(isGetRelations);

            return Ok(allProducts);
        }

        [HttpGet]
        [Route("GetProduct")]
        public async Task<ActionResult<Product>> GetProduct(int id, bool isGetRelations)
        {
            var foundProduct = await _unitOfWork.ProductRepository.GetAsync(id, isGetRelations);
            if (foundProduct == null)
            {
                return NotFound("Incoming Order not found");
            }

            return Ok(foundProduct);
        }

        [HttpPut]
        [Route("UpdateProduct")]
        public async Task<ActionResult<bool>> UpdateProduct(int id, ProductDto productDto)
        {
            var foundProduct = await _unitOfWork.ProductRepository.GetAsync(id, false);
            if (foundProduct == null)
            {
                return NotFound("Incoming Order not found");
            }

            foundProduct.Sku = productDto.Sku;
            foundProduct.Name = productDto.Name;
            foundProduct.Price = productDto.Price;
            foundProduct.Tag = productDto.Tag;
            foundProduct.Weight = productDto.Weight;
            foundProduct.Height = productDto.Height;
            foundProduct.Length = productDto.Length;
            foundProduct.Width = productDto.Width;

            var success = await _unitOfWork.ProductRepository.UpdateAsync(foundProduct);
            var saveSuccess = await _unitOfWork.CommitAsync();

            return Ok(success && saveSuccess > 0);
        }

        [HttpDelete]
        [Route("DeleteProduct")]
        public async Task<ActionResult<bool>> DeleteProduct(int id)
        {
            var foundProduct = await _unitOfWork.ProductRepository.GetAsync(id, false);
            if (foundProduct == null)
            {
                return NotFound("Incoming Order not found");
            }

            var success = await _unitOfWork.ProductRepository.DeleteAsync(id);
            var saveSuccess = await _unitOfWork.CommitAsync();

            return Ok(success && saveSuccess > 0);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        [Route("CreateInventoryItem")]
        public async Task<ActionResult<Inventory>> CreateInventoryItem(InventoryDto inventoryDto)
        {
            var newInventory = new Inventory
            {
                ProductId = inventoryDto.ProductId,
                DaysLeadTime = inventoryDto.DaysLeadTime,
                Quantity = inventoryDto.Quantity
            };

            var createdInventory = await _unitOfWork.InventoryRepository.AddAsync(newInventory);
            await _unitOfWork.CommitAsync();

            return Ok(createdInventory);
        }

        [HttpGet]
        [Route("GetAllInventoryItems")]
        public async Task<ActionResult<List<Inventory>>> GetAllInventoryItems(bool isGetRelations)
        {
            var allProducts = await _unitOfWork.InventoryRepository.GetAllAsync(isGetRelations);

            return Ok(allProducts);
        }

        [HttpGet]
        [Route("GetInventoryItem")]
        public async Task<ActionResult<Inventory>> GetInventoryItem(int id, bool isGetRelations)
        {
            var foundProduct = await _unitOfWork.InventoryRepository.GetAsync(id, isGetRelations);
            if (foundProduct == null)
            {
                return NotFound("Inventory item not found");
            }

            return Ok(foundProduct);
        }

        [HttpPut]
        [Route("UpdateInventoryItem")]
        public async Task<ActionResult<bool>> UpdateInventoryItem(int id, InventoryDto inventoryDto)
        {
            var foundInventoryItem = await _unitOfWork.InventoryRepository.GetAsync(id, false);
            if (foundInventoryItem == null)
            {
                return NotFound("Inventory item not found");
            }

            foundInventoryItem.DaysLeadTime = inventoryDto.DaysLeadTime;
            foundInventoryItem.Quantity = inventoryDto.Quantity;

            var success = await _unitOfWork.InventoryRepository.UpdateAsync(foundInventoryItem);
            var saveSuccess = await _unitOfWork.CommitAsync();

            return Ok(success && saveSuccess > 0);
        }

        [HttpDelete]
        [Route("DeleteInventoryItem")]
        public async Task<ActionResult<bool>> DeleteInventoryItem(int id)
        {
            var foundProduct = await _unitOfWork.InventoryRepository.GetAsync(id, false);
            if (foundProduct == null)
            {
                return NotFound("Inventory item not found");
            }

            var success = await _unitOfWork.InventoryRepository.DeleteAsync(id);
            var saveSuccess = await _unitOfWork.CommitAsync();

            return Ok(success && saveSuccess > 0);
        }
    }
}

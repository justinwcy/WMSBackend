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
        public async Task<ActionResult<Product>> GetProduct(Guid id, bool isGetRelations)
        {
            var foundProduct = await _unitOfWork.ProductRepository.GetAsync(id, isGetRelations);
            if (foundProduct == null)
            {
                return NotFound("Product not found");
            }

            return Ok(foundProduct);
        }

        [HttpPut]
        [Route("UpdateProduct")]
        public async Task<ActionResult<bool>> UpdateProduct(Guid id, ProductDto productDto)
        {
            var foundProduct = await _unitOfWork.ProductRepository.GetAsync(id, false);
            if (foundProduct == null)
            {
                return NotFound("Product not found");
            }

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
        public async Task<ActionResult<bool>> DeleteProduct(Guid id)
        {
            var foundProduct = await _unitOfWork.ProductRepository.GetAsync(id, false);
            if (foundProduct == null)
            {
                return NotFound("Product not found");
            }

            var success = await _unitOfWork.ProductRepository.DeleteAsync(id);
            var saveSuccess = await _unitOfWork.CommitAsync();

            return Ok(success && saveSuccess > 0);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        public async Task<ActionResult<Inventory>> GetInventoryItem(Guid id, bool isGetRelations)
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
        public async Task<ActionResult<bool>> UpdateInventoryItem(
            Guid id,
            InventoryDto inventoryDto
        )
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
        public async Task<ActionResult<bool>> DeleteInventoryItem(Guid id)
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

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        [Route("CreateProductSkuRelationship")]
        public async Task<ActionResult<ProductSku>> CreateProductSkuRelationship(
            ProductSkuDto productSkuDto
        )
        {
            var foundProduct = await _unitOfWork.ProductRepository.GetAsync(
                productSkuDto.ProductId,
                true
            );

            if (foundProduct == null)
            {
                return NotFound("Product not found");
            }

            var newProductSku = new ProductSku()
            {
                ProductId = productSkuDto.ProductId,
                Product = foundProduct,
                Sku = productSkuDto.Sku
            };

            foundProduct.ProductSkus.Add(newProductSku);
            var createdProductSku = await _unitOfWork.ProductSkuRepository.AddAsync(newProductSku);

            await _unitOfWork.CommitAsync();

            return Ok(createdProductSku);
        }

        [HttpDelete]
        [Route("DeleteProductSkuRelationship")]
        public async Task<ActionResult<bool>> DeleteProductSkuRelationship(
            ProductSkuDto productSkuDto
        )
        {
            var foundProduct = await _unitOfWork.ProductRepository.GetAsync(
                productSkuDto.ProductId,
                true
            );

            if (foundProduct == null)
            {
                return NotFound("Product not found");
            }

            var foundProductSkus = await _unitOfWork.ProductSkuRepository.FindAsync(
                productSku => productSku.Sku == productSkuDto.Sku,
                true
            );

            if (!foundProductSkus.Any())
            {
                return NotFound("ProductSku Relationship not found");
            }

            if (foundProductSkus.Count() != 1)
            {
                return NotFound(
                    "Multiple ProductSku Relationship found. Should only have 1 relationship"
                );
            }

            var foundProductSku = foundProductSkus.First();
            foundProduct.ProductSkus.Remove(foundProductSku);
            var deleteSuccess = await _unitOfWork.ProductSkuRepository.DeleteAsync(
                foundProductSku.Id
            );

            await _unitOfWork.CommitAsync();

            return Ok(deleteSuccess);
        }
    }
}

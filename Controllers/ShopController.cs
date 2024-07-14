using Microsoft.AspNetCore.Mvc;
using WMSBackend.DataTransferObject;
using WMSBackend.Interfaces;
using WMSBackend.Models;

namespace WMSBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShopController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("CreateShop")]
        public async Task<ActionResult<Shop>> CreateShop(ShopDto shopDto)
        {
            var newShop = new Shop
            {
                Name = shopDto.Name,
                Address = shopDto.Address,
                Platform = shopDto.Platform,
                Website = shopDto.Website
            };

            var createdShop = await _unitOfWork.ShopRepository.AddAsync(newShop);
            await _unitOfWork.CommitAsync();

            return Ok(createdShop);
        }

        [HttpGet]
        [Route("GetAllShops")]
        public async Task<ActionResult<List<Shop>>> GetAllShops(bool isGetRelations)
        {
            var allShops = await _unitOfWork.ShopRepository.GetAllAsync(isGetRelations);

            return Ok(allShops);
        }

        [HttpGet]
        [Route("GetShop")]
        public async Task<ActionResult<Shop>> GetShop(Guid id, bool isGetRelations)
        {
            var foundShop = await _unitOfWork.ShopRepository.GetAsync(id, isGetRelations);
            if (foundShop == null)
            {
                return NotFound("Shop not found");
            }

            return Ok(foundShop);
        }

        [HttpPut]
        [Route("UpdateShop")]
        public async Task<ActionResult<bool>> UpdateShop(Guid id, ShopDto shopDto)
        {
            var foundShop = await _unitOfWork.ShopRepository.GetAsync(id, false);
            if (foundShop == null)
            {
                return NotFound("Shop not found");
            }

            foundShop.Name = shopDto.Name;
            foundShop.Address = shopDto.Address;
            foundShop.Platform = shopDto.Platform;
            foundShop.Website = shopDto.Website;

            var success = await _unitOfWork.ShopRepository.UpdateAsync(foundShop);
            var saveSuccess = await _unitOfWork.CommitAsync();

            return Ok(success && saveSuccess > 0);
        }

        [HttpDelete]
        [Route("DeleteShop")]
        public async Task<ActionResult<bool>> DeleteShop(Guid id)
        {
            var foundShop = await _unitOfWork.ShopRepository.GetAsync(id, false);
            if (foundShop == null)
            {
                return NotFound("Shop not found");
            }

            var success = await _unitOfWork.ShopRepository.DeleteAsync(id);
            var saveSuccess = await _unitOfWork.CommitAsync();

            return Ok(success && saveSuccess > 0);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        [Route("CreateProductShopRelationship")]
        public async Task<ActionResult<Shop>> CreateShopProductRelationship(
            ProductShopDto productShopDto
        )
        {
            var foundShop = await _unitOfWork.ShopRepository.GetAsync(productShopDto.ShopId, true);
            if (foundShop == null)
            {
                return NotFound("Shop not found");
            }

            var foundProduct = await _unitOfWork.ProductRepository.GetAsync(
                productShopDto.ProductId,
                false,
                false,
                true,
                false,
                false,
                false
            );
            if (foundProduct == null)
            {
                return NotFound("Product not found");
            }

            foundShop.Products.Add(foundProduct);
            await _unitOfWork.CommitAsync();

            return Ok(foundShop);
        }

        [HttpPost]
        [Route("DeleteProductShopRelationship")]
        public async Task<ActionResult<Shop>> DeleteShopProductRelationship(
            ProductShopDto productShopDto
        )
        {
            var foundShop = await _unitOfWork.ShopRepository.GetAsync(productShopDto.ShopId, true);
            if (foundShop == null)
            {
                return NotFound("Shop not found");
            }

            var foundProduct = await _unitOfWork.ProductRepository.GetAsync(
                productShopDto.ProductId,
                false,
                false,
                true,
                false,
                false,
                false
            );
            if (foundProduct == null)
            {
                return NotFound("Product not found");
            }

            foundShop.Products.Remove(foundProduct);
            await _unitOfWork.CommitAsync();

            return Ok(foundShop);
        }
    }
}

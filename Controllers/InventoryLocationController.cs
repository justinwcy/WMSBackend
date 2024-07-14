using Microsoft.AspNetCore.Mvc;
using WMSBackend.DataTransferObject;
using WMSBackend.Interfaces;
using WMSBackend.Models;

namespace WMSBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryLocationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public InventoryLocationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("CreateWarehouse")]
        public async Task<ActionResult<Warehouse>> CreateWarehouse(WarehouseDto warehouseDto)
        {
            var newWarehouse = new Warehouse { Name = warehouseDto.Name, };

            var createdWarehouse = await _unitOfWork.WarehouseRepository.AddAsync(newWarehouse);
            await _unitOfWork.CommitAsync();

            return Ok(createdWarehouse);
        }

        [HttpGet]
        [Route("GetAllWarehouses")]
        public async Task<ActionResult<List<Warehouse>>> GetAllWarehouses(bool isGetRelations)
        {
            var allWarehouses = await _unitOfWork.WarehouseRepository.GetAllAsync(isGetRelations);

            return Ok(allWarehouses);
        }

        [HttpGet]
        [Route("GetWarehouse")]
        public async Task<ActionResult<Warehouse>> GetWarehouse(Guid id, bool isGetRelations)
        {
            var foundWarehouse = await _unitOfWork.WarehouseRepository.GetAsync(id, isGetRelations);
            if (foundWarehouse == null)
            {
                return NotFound("Warehouse not found");
            }

            return Ok(foundWarehouse);
        }

        [HttpPut]
        [Route("UpdateWarehouse")]
        public async Task<ActionResult<bool>> UpdateWarehouse(Guid id, WarehouseDto warehouseDto)
        {
            var foundWarehouse = await _unitOfWork.WarehouseRepository.GetAsync(id, false);
            if (foundWarehouse == null)
            {
                return NotFound("Warehouse not found");
            }

            foundWarehouse.Name = warehouseDto.Name;

            var success = await _unitOfWork.WarehouseRepository.UpdateAsync(foundWarehouse);
            var saveSuccess = await _unitOfWork.CommitAsync();

            return Ok(success && saveSuccess > 0);
        }

        [HttpDelete]
        [Route("DeleteWarehouse")]
        public async Task<ActionResult<bool>> DeleteWarehouse(Guid id)
        {
            var foundWarehouse = await _unitOfWork.WarehouseRepository.GetAsync(id, false);
            if (foundWarehouse == null)
            {
                return NotFound("Warehouse not found");
            }

            var success = await _unitOfWork.WarehouseRepository.DeleteAsync(id);
            var saveSuccess = await _unitOfWork.CommitAsync();

            return Ok(success && saveSuccess > 0);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        [Route("CreateZone")]
        public async Task<ActionResult<Zone>> CreateZone(ZoneDto zoneDto)
        {
            var newZone = new Zone { Name = zoneDto.Name, };

            var createdZone = await _unitOfWork.ZoneRepository.AddAsync(newZone);
            await _unitOfWork.CommitAsync();

            return Ok(createdZone);
        }

        [HttpGet]
        [Route("GetAllZones")]
        public async Task<ActionResult<List<Zone>>> GetAllZones(bool isGetRelations)
        {
            var allZones = await _unitOfWork.ZoneRepository.GetAllAsync(isGetRelations);

            return Ok(allZones);
        }

        [HttpGet]
        [Route("GetZone")]
        public async Task<ActionResult<Zone>> GetZone(Guid id, bool isGetRelations)
        {
            var foundZone = await _unitOfWork.ZoneRepository.GetAsync(id, isGetRelations);
            if (foundZone == null)
            {
                return NotFound("Zone not found");
            }

            return Ok(foundZone);
        }

        [HttpPut]
        [Route("UpdateZone")]
        public async Task<ActionResult<bool>> UpdateZone(Guid id, ZoneDto zoneDto)
        {
            var foundZone = await _unitOfWork.ZoneRepository.GetAsync(id, false);
            if (foundZone == null)
            {
                return NotFound("Zone not found");
            }

            foundZone.Name = zoneDto.Name;

            var success = await _unitOfWork.ZoneRepository.UpdateAsync(foundZone);
            var saveSuccess = await _unitOfWork.CommitAsync();

            return Ok(success && saveSuccess > 0);
        }

        [HttpDelete]
        [Route("DeleteZone")]
        public async Task<ActionResult<bool>> DeleteZone(Guid id)
        {
            var foundZone = await _unitOfWork.ZoneRepository.GetAsync(id, false);
            if (foundZone == null)
            {
                return NotFound("Zone not found");
            }

            var success = await _unitOfWork.ZoneRepository.DeleteAsync(id);
            var saveSuccess = await _unitOfWork.CommitAsync();

            return Ok(success && saveSuccess > 0);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        [Route("CreateRack")]
        public async Task<ActionResult<Rack>> CreateRack(RackDto rackDto)
        {
            var newRack = new Rack()
            {
                Name = rackDto.Name,
                MaxWeight = rackDto.MaxWeight,
                Depth = rackDto.Depth,
                Height = rackDto.Height,
                Width = rackDto.Width
            };

            var createdRack = await _unitOfWork.RackRepository.AddAsync(newRack);
            await _unitOfWork.CommitAsync();

            return Ok(createdRack);
        }

        [HttpGet]
        [Route("GetAllRacks")]
        public async Task<ActionResult<List<Rack>>> GetAllRacks(bool isGetRelations)
        {
            var allRacks = await _unitOfWork.RackRepository.GetAllAsync(isGetRelations);

            return Ok(allRacks);
        }

        [HttpGet]
        [Route("GetRack")]
        public async Task<ActionResult<Rack>> GetRack(Guid id, bool isGetRelations)
        {
            var foundRack = await _unitOfWork.RackRepository.GetAsync(id, isGetRelations);
            if (foundRack == null)
            {
                return NotFound("Rack not found");
            }

            return Ok(foundRack);
        }

        [HttpPut]
        [Route("UpdateRack")]
        public async Task<ActionResult<bool>> UpdateRack(Guid id, RackDto rackDto)
        {
            var foundRack = await _unitOfWork.RackRepository.GetAsync(id, false);
            if (foundRack == null)
            {
                return NotFound("Rack not found");
            }

            foundRack.Name = rackDto.Name;
            foundRack.Depth = rackDto.Depth;
            foundRack.Width = rackDto.Width;
            foundRack.MaxWeight = rackDto.MaxWeight;

            var success = await _unitOfWork.RackRepository.UpdateAsync(foundRack);
            var saveSuccess = await _unitOfWork.CommitAsync();

            return Ok(success && saveSuccess > 0);
        }

        [HttpDelete]
        [Route("DeleteRack")]
        public async Task<ActionResult<bool>> DeleteRack(Guid id)
        {
            var foundRack = await _unitOfWork.RackRepository.GetAsync(id, false);
            if (foundRack == null)
            {
                return NotFound("Rack not found");
            }

            var success = await _unitOfWork.RackRepository.DeleteAsync(id);
            var saveSuccess = await _unitOfWork.CommitAsync();

            return Ok(success && saveSuccess > 0);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        [Route("CreateWarehouseZoneRelationship")]
        public async Task<ActionResult<Warehouse>> CreateWarehouseZoneRelationship(
            WarehouseZoneDto warehouseZoneDto
        )
        {
            var foundWarehouse = await _unitOfWork.WarehouseRepository.GetAsync(
                warehouseZoneDto.WarehouseId,
                true
            );
            if (foundWarehouse == null)
            {
                return NotFound("Warehouse not found");
            }

            var foundZone = await _unitOfWork.ZoneRepository.GetAsync(
                warehouseZoneDto.ZoneId,
                true,
                false,
                false
            );
            if (foundZone == null)
            {
                return NotFound("Zone not found");
            }

            foundWarehouse.Zones.Add(foundZone);
            await _unitOfWork.CommitAsync();

            return Ok(foundWarehouse);
        }

        [HttpPost]
        [Route("DeleteWarehouseZoneRelationship")]
        public async Task<ActionResult<Warehouse>> DeleteWarehouseZoneRelationship(
            WarehouseZoneDto warehouseZoneDto
        )
        {
            var foundWarehouse = await _unitOfWork.WarehouseRepository.GetAsync(
                warehouseZoneDto.WarehouseId,
                true
            );
            if (foundWarehouse == null)
            {
                return NotFound("Warehouse not found");
            }

            var foundZone = await _unitOfWork.ZoneRepository.GetAsync(
                warehouseZoneDto.ZoneId,
                true,
                false,
                false
            );
            if (foundZone == null)
            {
                return NotFound("Zone not found");
            }

            foundWarehouse.Zones.Remove(foundZone);
            await _unitOfWork.CommitAsync();

            return Ok(foundWarehouse);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        [Route("CreateZoneStaffRelationship")]
        public async Task<ActionResult<Zone>> CreateZoneStaffRelationship(ZoneStaffDto zoneStaffDto)
        {
            var foundZone = await _unitOfWork.ZoneRepository.GetAsync(zoneStaffDto.ZoneId, true);
            if (foundZone == null)
            {
                return NotFound("Zone not found");
            }

            var foundStaff = await _unitOfWork.StaffRepository.GetAsync(zoneStaffDto.StaffId, true);
            if (foundStaff == null)
            {
                return NotFound("Staff not found");
            }

            foundZone.Staffs.Add(foundStaff);
            await _unitOfWork.CommitAsync();

            return Ok(foundZone);
        }

        [HttpPost]
        [Route("DeleteZoneStaffRelationship")]
        public async Task<ActionResult<Zone>> DeleteZoneStaffRelationship(ZoneStaffDto zoneStaffDto)
        {
            var foundZone = await _unitOfWork.ZoneRepository.GetAsync(zoneStaffDto.ZoneId, true);
            if (foundZone == null)
            {
                return NotFound("Zone not found");
            }

            var foundStaff = await _unitOfWork.StaffRepository.GetAsync(
                zoneStaffDto.StaffId.ToString(),
                false,
                true,
                false
            );
            if (foundStaff == null)
            {
                return NotFound("Staff not found");
            }

            foundZone.Staffs.Remove(foundStaff);
            await _unitOfWork.CommitAsync();

            return Ok(foundZone);
        }

        /////////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        [Route("CreateZoneRackRelationship")]
        public async Task<ActionResult<Zone>> CreateZoneRackRelationship(ZoneRackDto zoneRackDto)
        {
            var foundZone = await _unitOfWork.ZoneRepository.GetAsync(zoneRackDto.ZoneId, true);
            if (foundZone == null)
            {
                return NotFound("Zone not found");
            }

            var foundRack = await _unitOfWork.RackRepository.GetAsync(zoneRackDto.RackId, true);
            if (foundRack == null)
            {
                return NotFound("Rack not found");
            }

            foundZone.Racks.Add(foundRack);
            await _unitOfWork.CommitAsync();

            return Ok(foundZone);
        }

        [HttpPost]
        [Route("DeleteZoneRackRelationship")]
        public async Task<ActionResult<Zone>> DeleteZoneRackRelationship(ZoneRackDto zoneRackDto)
        {
            var foundZone = await _unitOfWork.ZoneRepository.GetAsync(zoneRackDto.ZoneId, true);
            if (foundZone == null)
            {
                return NotFound("Zone not found");
            }

            var foundRack = await _unitOfWork.RackRepository.GetAsync(
                zoneRackDto.RackId,
                true,
                false
            );
            if (foundRack == null)
            {
                return NotFound("Rack not found");
            }

            foundZone.Racks.Remove(foundRack);
            await _unitOfWork.CommitAsync();

            return Ok(foundZone);
        }

        /////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        [Route("CreateProductRackRelationship")]
        public async Task<ActionResult<Product>> CreateProductRackRelationship(
            ProductRackDto zoneRackDto
        )
        {
            var foundProduct = await _unitOfWork.ProductRepository.GetAsync(
                zoneRackDto.ProductId,
                true
            );
            if (foundProduct == null)
            {
                return NotFound("Product not found");
            }

            var foundRack = await _unitOfWork.RackRepository.GetAsync(zoneRackDto.RackId, true);
            if (foundRack == null)
            {
                return NotFound("Rack not found");
            }

            foundProduct.Racks.Add(foundRack);
            await _unitOfWork.CommitAsync();

            return Ok(foundProduct);
        }

        [HttpPost]
        [Route("DeleteProductRackRelationship")]
        public async Task<ActionResult<Product>> DeleteProductRackRelationship(
            ProductRackDto zoneRackDto
        )
        {
            var foundProduct = await _unitOfWork.ProductRepository.GetAsync(
                zoneRackDto.ProductId,
                true
            );
            if (foundProduct == null)
            {
                return NotFound("Product not found");
            }

            var foundRack = await _unitOfWork.RackRepository.GetAsync(
                zoneRackDto.RackId,
                false,
                true
            );
            if (foundRack == null)
            {
                return NotFound("Rack not found");
            }

            foundProduct.Racks.Remove(foundRack);
            await _unitOfWork.CommitAsync();

            return Ok(foundProduct);
        }
    }
}

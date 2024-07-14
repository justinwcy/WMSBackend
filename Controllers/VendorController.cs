using Microsoft.AspNetCore.Mvc;
using WMSBackend.DataTransferObject;
using WMSBackend.Interfaces;
using WMSBackend.Models;

namespace WMSBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public VendorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        [Route("CreateVendor")]
        public async Task<ActionResult<Vendor>> CreateVendor(VendorDto vendorDto)
        {
            var newVendor = new Vendor
            {
                FirstName = vendorDto.FirstName,
                LastName = vendorDto.LastName,
                UserName = vendorDto.UserName,
                Email = vendorDto.Email,
                Address = vendorDto.Address,
                PhoneNumber = vendorDto.PhoneNumber
            };

            var createdVendor = await _unitOfWork.VendorRepository.AddAsync(newVendor);
            await _unitOfWork.CommitAsync();

            return Ok(createdVendor);
        }

        [HttpGet]
        [Route("GetAllVendors")]
        public async Task<ActionResult<List<Vendor>>> GetAllVendors(bool isGetRelations)
        {
            var allVendors = await _unitOfWork.VendorRepository.GetAllAsync(isGetRelations);

            return Ok(allVendors);
        }

        [HttpGet]
        [Route("GetVendor")]
        public async Task<ActionResult<Vendor>> GetVendor(Guid id, bool isGetRelations)
        {
            var foundVendor = await _unitOfWork.VendorRepository.GetAsync(id, isGetRelations);
            if (foundVendor == null)
            {
                return NotFound("Vendor not found");
            }

            return Ok(foundVendor);
        }

        [HttpPut]
        [Route("UpdateVendor")]
        public async Task<ActionResult<bool>> UpdateVendor(Guid id, VendorDto vendorDto)
        {
            var foundVendor = await _unitOfWork.VendorRepository.GetAsync(id, false);
            if (foundVendor == null)
            {
                return NotFound("Vendor not found");
            }

            foundVendor.FirstName = vendorDto.FirstName;
            foundVendor.LastName = vendorDto.LastName;
            foundVendor.UserName = vendorDto.UserName;
            foundVendor.Email = vendorDto.Email;
            foundVendor.Address = vendorDto.Address;

            var success = await _unitOfWork.VendorRepository.UpdateAsync(foundVendor);
            var saveSuccess = await _unitOfWork.CommitAsync();

            return Ok(success && saveSuccess > 0);
        }

        [HttpDelete]
        [Route("DeleteVendor")]
        public async Task<ActionResult<bool>> DeleteVendor(Guid id)
        {
            var foundVendor = await _unitOfWork.VendorRepository.GetAsync(id, false);
            if (foundVendor == null)
            {
                return NotFound("Vendor not found");
            }

            var success = await _unitOfWork.VendorRepository.DeleteAsync(id);
            var saveSuccess = await _unitOfWork.CommitAsync();

            return Ok(success && saveSuccess > 0);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using WMSBackend.DataTransferObject;
using WMSBackend.Interfaces;
using WMSBackend.Models;

namespace WMSBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("CreateCompany")]
        public async Task<ActionResult<Company>> CreateCompany(CompanyDto companyDto)
        {
            var newCompany = new Company { Name = companyDto.Name, };

            var createdCompany = await _unitOfWork.CompanyRepository.AddAsync(newCompany);
            await _unitOfWork.CommitAsync();

            return Ok(createdCompany);
        }

        [HttpGet]
        [Route("GetAllCompanies")]
        public async Task<ActionResult<List<Company>>> GetAllCompanies(bool isGetRelations)
        {
            var allCompanies = await _unitOfWork.CompanyRepository.GetAllAsync(isGetRelations);

            return Ok(allCompanies);
        }

        [HttpGet]
        [Route("GetCompany")]
        public async Task<ActionResult<Company>> GetCompany(int id, bool isGetRelations)
        {
            var foundCompany = await _unitOfWork.CompanyRepository.GetAsync(id, isGetRelations);
            if (foundCompany == null)
            {
                return NotFound("Company not found");
            }

            return Ok(foundCompany);
        }

        [HttpPut]
        [Route("UpdateCompany")]
        public async Task<ActionResult<bool>> UpdateCompany(int id, CompanyDto companyDto)
        {
            var foundCompany = await _unitOfWork.CompanyRepository.GetAsync(id, false);
            if (foundCompany == null)
            {
                return NotFound("Company not found");
            }

            foundCompany.Name = companyDto.Name;

            var success = await _unitOfWork.CompanyRepository.UpdateAsync(foundCompany);
            var saveSuccess = await _unitOfWork.CommitAsync();

            return Ok(success && saveSuccess > 0);
        }

        [HttpDelete]
        [Route("DeleteCompany")]
        public async Task<ActionResult<bool>> DeleteCompany(int id)
        {
            var foundCompany = await _unitOfWork.CompanyRepository.GetAsync(id, false);
            if (foundCompany == null)
            {
                return NotFound("Company not found");
            }

            var success = await _unitOfWork.CompanyRepository.DeleteAsync(id);
            var saveSuccess = await _unitOfWork.CommitAsync();

            return Ok(success && saveSuccess > 0);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        [Route("CreateStaff")]
        public async Task<ActionResult<Staff>> CreateStaff(StaffDto staffDto)
        {
            var newStaff = new Staff
            {
                FirstName = staffDto.FirstName,
                LastName = staffDto.LastName,
                UserName = staffDto.UserName,
                Email = staffDto.Email
            };

            var createdStaff = await _unitOfWork.StaffRepository.AddAsync(newStaff);
            await _unitOfWork.CommitAsync();

            return Ok(createdStaff);
        }

        [HttpGet]
        [Route("GetAllStaffs")]
        public async Task<ActionResult<List<Staff>>> GetAllStaffs(bool isGetRelations)
        {
            var allCompanies = await _unitOfWork.StaffRepository.GetAllAsync(isGetRelations);

            return Ok(allCompanies);
        }

        [HttpGet]
        [Route("GetStaff")]
        public async Task<ActionResult<Staff>> GetStaff(string id, bool isGetRelations)
        {
            var foundStaff = await _unitOfWork.StaffRepository.GetAsync(id, isGetRelations);
            if (foundStaff == null)
            {
                return NotFound("Staff not found");
            }

            return Ok(foundStaff);
        }

        [HttpPut]
        [Route("UpdateStaff")]
        public async Task<ActionResult<bool>> UpdateStaff(string id, StaffDto staffDto)
        {
            var foundStaff = await _unitOfWork.StaffRepository.GetAsync(id, false);
            if (foundStaff == null)
            {
                return NotFound("Staff not found");
            }

            foundStaff.FirstName = staffDto.FirstName;
            foundStaff.LastName = staffDto.LastName;
            foundStaff.UserName = staffDto.UserName;
            foundStaff.Email = staffDto.Email;

            var success = await _unitOfWork.StaffRepository.UpdateAsync(foundStaff);
            var saveSuccess = await _unitOfWork.CommitAsync();

            return Ok(success && saveSuccess > 0);
        }

        [HttpDelete]
        [Route("DeleteStaff")]
        public async Task<ActionResult<bool>> DeleteStaff(string id)
        {
            var foundStaff = await _unitOfWork.StaffRepository.GetAsync(id, false);
            if (foundStaff == null)
            {
                return NotFound("Staff not found");
            }

            var success = await _unitOfWork.StaffRepository.DeleteAsync(id);
            var saveSuccess = await _unitOfWork.CommitAsync();

            return Ok(success && saveSuccess > 0);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        [Route("CreateStaffNotification")]
        public async Task<ActionResult<StaffNotification>> CreateStaffNotification(
            StaffNotificationDto staffNotificationDto
        )
        {
            var newStaffNotification = new StaffNotification
            {
                Subject = staffNotificationDto.Subject,
                StaffId = staffNotificationDto.StaffId,
                Body = staffNotificationDto.Body,
                IsRead = staffNotificationDto.IsRead
            };

            var createdStaffNotification = await _unitOfWork.StaffNotificationRepository.AddAsync(
                newStaffNotification
            );
            await _unitOfWork.CommitAsync();

            return Ok(createdStaffNotification);
        }

        [HttpGet]
        [Route("GetAllStaffNotifications")]
        public async Task<ActionResult<List<StaffNotification>>> GetAllStaffNotifications(
            bool isGetRelations
        )
        {
            var allCompanies = await _unitOfWork.StaffNotificationRepository.GetAllAsync(
                isGetRelations
            );

            return Ok(allCompanies);
        }

        [HttpGet]
        [Route("GetStaffNotification")]
        public async Task<ActionResult<StaffNotification>> GetStaffNotification(
            int id,
            bool isGetRelations
        )
        {
            var foundStaffNotification = await _unitOfWork.StaffNotificationRepository.GetAsync(
                id,
                isGetRelations
            );
            if (foundStaffNotification == null)
            {
                return NotFound("StaffNotification not found");
            }

            return Ok(foundStaffNotification);
        }

        [HttpPut]
        [Route("UpdateStaffNotification")]
        public async Task<ActionResult<bool>> UpdateStaffNotification(
            int id,
            StaffNotificationDto staffNotificationDto
        )
        {
            var foundStaffNotification = await _unitOfWork.StaffNotificationRepository.GetAsync(
                id,
                false
            );
            if (foundStaffNotification == null)
            {
                return NotFound("StaffNotification not found");
            }

            foundStaffNotification.Subject = staffNotificationDto.Subject;
            foundStaffNotification.StaffId = staffNotificationDto.StaffId;
            foundStaffNotification.Body = staffNotificationDto.Body;
            foundStaffNotification.IsRead = staffNotificationDto.IsRead;

            var success = await _unitOfWork.StaffNotificationRepository.UpdateAsync(
                foundStaffNotification
            );
            var saveSuccess = await _unitOfWork.CommitAsync();

            return Ok(success && saveSuccess > 0);
        }

        [HttpDelete]
        [Route("DeleteStaffNotification")]
        public async Task<ActionResult<bool>> DeleteStaffNotification(int id)
        {
            var foundStaffNotification = await _unitOfWork.StaffNotificationRepository.GetAsync(
                id,
                false
            );
            if (foundStaffNotification == null)
            {
                return NotFound("StaffNotification not found");
            }

            var success = await _unitOfWork.StaffNotificationRepository.DeleteAsync(id);
            var saveSuccess = await _unitOfWork.CommitAsync();

            return Ok(success && saveSuccess > 0);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        [Route("CreateCompanyStaffRelationship")]
        public async Task<ActionResult<Company>> CreateCompanyStaffRelationship(
            CompanyStaffDto companyStaffDto
        )
        {
            var foundCompany = await _unitOfWork.CompanyRepository.GetAsync(
                companyStaffDto.CompanyId,
                true
            );
            if (foundCompany == null)
            {
                return NotFound("Company not found");
            }

            var foundStaff = await _unitOfWork.StaffRepository.GetAsync(
                companyStaffDto.StaffId,
                true,
                false,
                false
            );
            if (foundStaff == null)
            {
                return NotFound("Staff not found");
            }

            foundCompany.Staffs.Add(foundStaff);
            await _unitOfWork.CommitAsync();

            return Ok(foundCompany);
        }

        [HttpPost]
        [Route("DeleteCompanyStaffRelationship")]
        public async Task<ActionResult<Company>> DeleteCompanyStaffRelationship(
            CompanyStaffDto companyStaffDto
        )
        {
            var foundCompany = await _unitOfWork.CompanyRepository.GetAsync(
                companyStaffDto.CompanyId,
                true
            );
            if (foundCompany == null)
            {
                return NotFound("Company not found");
            }

            var foundStaff = await _unitOfWork.StaffRepository.GetAsync(
                companyStaffDto.StaffId,
                true,
                false,
                false
            );
            if (foundStaff == null)
            {
                return NotFound("Staff not found");
            }

            foundCompany.Staffs.Remove(foundStaff);
            await _unitOfWork.CommitAsync();

            return Ok(foundCompany);
        }
    }
}

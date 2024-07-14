using Microsoft.AspNetCore.Mvc;
using WMSBackend.DataTransferObject;
using WMSBackend.Interfaces;
using WMSBackend.Models;

namespace WMSBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public SampleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

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
    }
}

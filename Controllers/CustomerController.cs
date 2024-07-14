using Microsoft.AspNetCore.Mvc;
using WMSBackend.DataTransferObject;
using WMSBackend.Interfaces;
using WMSBackend.Models;

namespace WMSBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("CreateCustomer")]
        public async Task<ActionResult<Customer>> CreateCustomer(CustomerDto customerDto)
        {
            var newCustomer = new Customer
            {
                FirstName = customerDto.FirstName,
                LastName = customerDto.LastName,
                UserName = customerDto.UserName,
                Email = customerDto.Email,
                Address = customerDto.Address
            };

            var createdCustomer = await _unitOfWork.CustomerRepository.AddAsync(newCustomer);
            await _unitOfWork.CommitAsync();

            return Ok(createdCustomer);
        }

        [HttpGet]
        [Route("GetAllCustomers")]
        public async Task<ActionResult<List<Customer>>> GetAllCustomers(bool isGetRelations)
        {
            var allCustomers = await _unitOfWork.CustomerRepository.GetAllAsync(isGetRelations);

            return Ok(allCustomers);
        }

        [HttpGet]
        [Route("GetCustomer")]
        public async Task<ActionResult<Customer>> GetCustomer(string id, bool isGetRelations)
        {
            var foundCustomer = await _unitOfWork.CustomerRepository.GetAsync(id, isGetRelations);
            if (foundCustomer == null)
            {
                return NotFound("Customer not found");
            }

            return Ok(foundCustomer);
        }

        [HttpPut]
        [Route("UpdateCustomer")]
        public async Task<ActionResult<bool>> UpdateCustomer(string id, CustomerDto customerDto)
        {
            var foundCustomer = await _unitOfWork.CustomerRepository.GetAsync(id, false);
            if (foundCustomer == null)
            {
                return NotFound("Customer not found");
            }

            foundCustomer.FirstName = customerDto.FirstName;
            foundCustomer.LastName = customerDto.LastName;
            foundCustomer.UserName = customerDto.UserName;
            foundCustomer.Email = customerDto.Email;
            foundCustomer.Address = customerDto.Address;

            var success = await _unitOfWork.CustomerRepository.UpdateAsync(foundCustomer);
            var saveSuccess = await _unitOfWork.CommitAsync();

            return Ok(success && saveSuccess > 0);
        }

        [HttpDelete]
        [Route("DeleteCustomer")]
        public async Task<ActionResult<bool>> DeleteCustomer(string id)
        {
            var foundCustomer = await _unitOfWork.CustomerRepository.GetAsync(id, false);
            if (foundCustomer == null)
            {
                return NotFound("Customer not found");
            }

            var success = await _unitOfWork.CustomerRepository.DeleteAsync(id);
            var saveSuccess = await _unitOfWork.CommitAsync();

            return Ok(success && saveSuccess > 0);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        [Route("CreateCustomerCustomerOrderRelationship")]
        public async Task<ActionResult<Courier>> CreateCourierProductRelationship(
            CustomerCustomerOrderDto customerCustomerOrderDto
        )
        {
            var foundCustomer = await _unitOfWork.CustomerRepository.GetAsync(
                customerCustomerOrderDto.CustomerId,
                true
            );
            if (foundCustomer == null)
            {
                return NotFound("Customer not found");
            }

            var foundCustomerOrder = await _unitOfWork.CustomerOrderRepository.GetAsync(
                customerCustomerOrderDto.CustomerOrderId,
                true,
                false,
                false,
                false
            );
            if (foundCustomerOrder == null)
            {
                return NotFound("Customer Order not found");
            }

            foundCustomer.CustomerOrders.Add(foundCustomerOrder);
            await _unitOfWork.CommitAsync();

            return Ok(foundCustomer);
        }

        [HttpPost]
        [Route("DeleteCustomerCustomerOrderRelationship")]
        public async Task<ActionResult<Courier>> DeleteCourierProductRelationship(
            CustomerCustomerOrderDto customerCustomerOrderDto
        )
        {
            var foundCustomer = await _unitOfWork.CustomerRepository.GetAsync(
                customerCustomerOrderDto.CustomerId,
                true
            );
            if (foundCustomer == null)
            {
                return NotFound("Customer not found");
            }

            var foundCustomerOrder = await _unitOfWork.CustomerOrderRepository.GetAsync(
                customerCustomerOrderDto.CustomerOrderId,
                true,
                false,
                false,
                false
            );
            if (foundCustomerOrder == null)
            {
                return NotFound("Customer Order not found");
            }

            foundCustomer.CustomerOrders.Remove(foundCustomerOrder);
            await _unitOfWork.CommitAsync();

            return Ok(foundCustomer);
        }
    }
}

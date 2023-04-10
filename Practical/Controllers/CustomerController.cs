using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Entites;
using Models.Models.Request;
using Models.Models.Response;
using PracticalDbFirst.Utils;

namespace PracticalDbFirst.Controllers
{
    public class CustomerController : BaseController<CustomerController>
    {
        public CustomerController(DatabaseContext practiceDbContext, ILogger<CustomerController> logger, IConfiguration config)
            : base(practiceDbContext, logger, config) { }

        [HttpGet]
        public IActionResult GetList()
        {
            var result = _context.Customers.Select(m => 
            new CustomerModel()
                        {
                            Id= m.Id,
                            Name= m.Name,
                            Address= m.Address,
                            Status= m.Status,
                            Age= m.Age,
                            Debit= m.Debit,
                            Description= m.Description,
                            Gender= m.Gender,
                            Username = m.Username
                        }
            );
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetDetails(long id) 
        {
            if (_context.Customers.Find(id) == null)return BadRequest("Not found customer");
            var order = from or in _context.Orders
                        join pr in _context.Products on or.ProductId equals pr.Id
                        where or.CustomerId == id
                        select new Product
                        {
                            Id= pr.Id,
                            Name = pr.Name,
                            Amount= or.Amount,
                            Price= or.Price,
                            Status= pr.Status,
                            Description = pr.Description,
                            ExpDate= pr.ExpDate,
                        };
            ;

            return Ok(new CustomerDetailModel()
            {
                Id= customer.Id,
                Name= customer.Name,
                Age= customer.Age,
                Gender= customer.Gender,
                Description= customer.Description,
                Debit= customer.Debit,
                Username= customer.Username,
                Status= customer.Status,
                Products = order.ToList(),
            });
        }


        [HttpPut]
        public IActionResult Edit([FromBody] EditCustomerModel model)
        {
            if (_context.Customers.Find(model.Id) == null) return NotFound("Customer is not found");

            customer.Name= model.Name;
            customer.Address = model.Address;
            customer.Age = model.Age;
            customer.Status= model.Status;
            customer.Description = model.Description;
            customer.Gender= model.Gender;
            customer.UpdatedBy= model.UpdatedBy;
            customer.UpdatedDate = DateTime.Now;

            _context.Customers.Update(customer);
            var eff = _context.SaveChanges();
            return eff > 0 ? Ok("Create success.") : BadRequest("Create Failed");
        }


        [HttpPut]
        public IActionResult ResetPassword([FromBody] ResetPasswordModel model)
        {
            if (_context.Customers.Find(model.CustomerId) == null) return NotFound("Not found customer.");

            customer.Salt ??= Guid.NewGuid().ToString();

            var passHash = customer.Username.ComputeSha256Hash(customer.Salt, model.Password);

            customer.Password = passHash;
            customer.UpdatedBy = "";
            customer.UpdatedDate = DateTime.Now;

            _context.Customers.Update(customer);
            var eff = _context.SaveChanges();
            return eff > 0 ? Ok("Reset password success.") : BadRequest("Reset failed");
        }

        [HttpPost]
        public IActionResult Add([FromBody] CustomerModel model)
        {
            var customer = new Customer()
            {
                Name= model.Name,
                Age= model.Age,
                Address= model.Address,
                Username= model.Username,
                Status= model.Status,
                Gender= model.Gender,
                Description= model.Description,
                Debit= model.Debit,
            };
            _context.Customers.Add(customer);
            var eff = _context.SaveChanges();
            return eff > 0 ? Ok("Create successfully.") : BadRequest("Create Failed");
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] long id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null) return NotFound("Not found customer.");

            _context.Customers.Remove(customer);
            var eff = _context.SaveChanges();
            return eff > 0 ? Ok("Delete customer successfully.") : BadRequest("Delete Failed");
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] CreateOrderModel model)
        {
            if (_context.Customers.Find(model.CustomerId) == null) return NotFound("Customer is not found.");

            if (_context.Products.Find(model.ProductId) == null) return NotFound("Product is not found.");

            if (_context.Orders.Find(model.ProductId, model.CustomerId) != null) return NotFound("Order is already exist.");

            Order order = new Order()
            {
                ProductId = model.ProductId,
                CustomerId = model.CustomerId,
                Price= model.Price,
                Amount= model.Amount,
                CreatedDate= DateTime.Now
            }; 

            _context.Orders.Add(order);
            var eff = _context.SaveChanges();
            return eff > 0 ? Ok("Order successfully.") : BadRequest("Order failed.") ;
        }

    }
}

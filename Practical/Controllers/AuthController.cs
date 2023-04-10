using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Entites;
using Models.Models.Request;
using PracticalDbFirst.Utils;

namespace PracticalDbFirst.Controllers
{
    public class AuthController : BaseController<AuthController>
    {
        public AuthController(DatabaseContext practiceDbContext, ILogger<AuthController> logger, IConfiguration config)
            : base(practiceDbContext, logger, config) 
        { }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] AuthenticationModel model)
        {
            var customer = _context.Customers.FirstOrDefault(m => m.Username == model.Username);
            if (customer == null) return BadRequest("Username/password incorrect");

            var isValid = model.Username.ValidPassword(customer.Salt, model.Password, customer.Password);
            if (!isValid) return BadRequest("Username/password incorrect");

            var accessToken = GenerateToken(model.Username);
            return Ok(accessToken);
        }
    }
}

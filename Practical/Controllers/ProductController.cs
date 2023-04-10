using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Entites;

namespace PracticalDbFirst.Controllers
{
    public class ProductController : BaseController<ProductController>
    {
        public ProductController(DatabaseContext practiceDbContext, ILogger<ProductController> logger, IConfiguration config)
            : base(practiceDbContext, logger, config) { }

        [HttpGet]
        public IActionResult GetList()
        {
            var result = _context.Products.ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetDetails(long id) 
        {
            var product = _context.Products.Find(id);
            if (_context.Products.Find(id) == null)
            {
                return BadRequest("Product is not found");
            }
            return Ok(product);
        }


        [HttpPut]
        public IActionResult Edit([FromBody] Product model)
        {
            var product = _context.Products.Find(model.Id);
            if (product == null) return NotFound("Product is not found.");

            product.Name = model.Name;
            product.Description = model.Description;
            product.UpdatedDate = DateTime.Now;
            product.UpdatedBy= model.UpdatedBy;
            product.ExpDate = model.ExpDate;
            product.Price = model.Price;
            product.Amount = model.Amount;
            product.Status= model.Status;

            _context.Products.Update(product);
            var eff = _context.SaveChanges();
            return eff > 0 ? Ok("Edit successfully.") : BadRequest("Edit Failed");
        }

        [HttpPost]
        public IActionResult Add([FromBody] Product model)
        {
            _context.Products.Add(model);
            var eff = _context.SaveChanges();
            return eff > 0 ? Ok("Create success.") : BadRequest("Create Failed");
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] long id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound("Product is not found.");

            _context.Products.Remove(product);
            var eff = _context.SaveChanges();
            return eff > 0 ? Ok("Delete Product successfully.") : BadRequest("Delete Failed");
        }

    }
}

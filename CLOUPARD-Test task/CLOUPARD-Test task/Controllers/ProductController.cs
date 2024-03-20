using CLOUPARD_Test_task.DAL;
using CLOUPARD_Test_task.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CLOUPARD_Test_task.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly TestDbContext _dbContext;
        public ProductController(TestDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(_dbContext));
        }

        [HttpGet]
        public async Task<ActionResult<Product>> GetAllAsync()
        {
            var products = await _dbContext.Product.OrderBy(p => p.Name).ToListAsync();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(Guid id)
        {
            var product = await _dbContext.Product.FindAsync(id);

            return product == null ? NotFound() : Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Product product)
        {
            _dbContext.Product.Add(product);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(Post), new { id = product.Id }, product);
        }

        [HttpPut]
        public async Task<ActionResult> Put(Guid Id, [FromBody] Product product)
        {
            if (Id != product.Id)
                return BadRequest(
                    $"Route id: {Id} does not match with parameter id: {product.Id}.");

            _dbContext.Product.Update(product);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(Put), new { id = product.Id }, product);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(Guid Id)
        {
            var product = await _dbContext.Product.FindAsync(Id);
            if (product is null) return NotFound();

            _dbContext.Product.Remove(product);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}

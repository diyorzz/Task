using CLOUPARD_Test_task_MVC.DataAccessLayer;
using CLOUPARD_Test_task_MVC.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CLOUPARD_Test_task_MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly TestDbContext _dbContext;
        public ProductController(TestDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: ProductController
        public async Task<ActionResult> Index(string sortOrder)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["IdSort"] = sortOrder == "Id_asc" ? "Id_desc" : "Id_asc";
            ViewData["NameSort"] = sortOrder == "Name_asc" ? "Name_desc" : "Name_asc";
            ViewData["DescriptionSort"] = sortOrder == "Desc_asc" ? "Desc_desc" : "Desc_asc";

            var products = _dbContext.Product.AsQueryable();

            products = sortOrder switch
            {
                "Id_asc" => products.OrderBy(x => x.Id),
                "Id_desc" => products.OrderByDescending(x => x.Id),
                "Name_asc" => products.OrderBy(x => x.Name),
                "Name_desc" => products.OrderByDescending(x => x.Name),
                "Desc_asc" => products.OrderBy(x => x.Description),
                "Desc_desc" => products.OrderByDescending(x => x.Description),
                _ => products.OrderBy(x => x.Id)
            };
            return View(products);
        }

        // GET: ProductController/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id is null || _dbContext.Product is null)
            {
                return NotFound();
            }

            var product = await _dbContext.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product is null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([Bind("Id,Name,Description")] Product product)
        {
            if (product is not null)
            {
                await _dbContext.Product.AddAsync(product);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(Guid id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, [Bind("Id,Name,Description")] Product product)
        {
            if (id != product.Id)
                return NotFound();
            if (product is not null)
            {
                try
                {
                    _dbContext.Update(product);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                        return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(Guid id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            if (_dbContext.Product == null)
            {
                return Problem("Entity set 'TestDbContext.Product'  is null.");
            }
            var product = await _dbContext.Product.FindAsync(id);
            if (product is not null)
            {
                _dbContext.Product.Remove(product);
            }

            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        private bool ProductExists(Guid id)
        {
            return (_dbContext.Product?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

using AspPage.Dal;
using AspPage.Models;
using AspPage.Utilites;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AspPage.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ProductController : Controller
    {
        private AppDbContext _context { get; }

        private readonly IWebHostEnvironment _env;

        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;

        }
        public IActionResult Index()
        {
          List < Product> product = _context.products.ToList();
            return View(product);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View();

            }
            bool product1= _context.products.Any(x=>x.Description==product.Description);
            if (product1)
            {
                return View();
            }
            if (!product.Photo.ChechSize(100))
            {
                ModelState.AddModelError("Photo","200 KB LIMITINI KECIBSIZ");
                return View();

            }
            if (!product.Photo.CheckType("image/"))
            {
                ModelState.AddModelError("Photo", "img formatinda bir seyler atin");
                return View();

            }
            product.ImgUrl = await product.Photo.SavaChacheAsync(Path.Combine(_env.WebRootPath, "assest", "images"));
            _context.products.Add(product);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int Id)
        {
            Product product = _context.products.Find(Id);
            if (product == null) return BadRequest();
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Update(int Id,Product product)
        {
            Product product1 = _context.products.Find(Id);
            if (product == null) return BadRequest();
            if (!product.Photo.ChechSize(200))
            {
                ModelState.AddModelError("Photo", "200 KB LIMITINI KECIBSIZ");
                return View();

            }
            if (!product.Photo.CheckType("image/"))
            {
                ModelState.AddModelError("Photo", "img formatinda bir seyler atin");
                return View();

            }
            product.ImgUrl = await product.Photo.SavaChacheAsync(Path.Combine(_env.WebRootPath, "assest", "images"));

            product1.ImgUrl = product.ImgUrl;
            product1.Title = product.Title;
            product1.Description = product.Description;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int Id)
        {
            Product product1 = _context.products.Find(Id);
            if (product1==null)
            {
                return BadRequest();

            }
            _context.products.Remove(product1);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}

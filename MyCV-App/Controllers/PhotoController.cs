using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCV_App.Data;
using MyCV_App.Models;

namespace MyCV_App.Controllers
{
    public class PhotoController : Controller
    {
        private readonly IWebHostEnvironment environment;
        private readonly PhotoDbContext photoDbContext;

        public PhotoController(IWebHostEnvironment environment, PhotoDbContext photoDbContext)
        {
            this.environment = environment;
            this.photoDbContext = photoDbContext;
        }

        // 📌 Display Gallery
        public IActionResult Index()
        {
            var photos = photoDbContext.Photos.ToList();
            return View(photos);
        }

        // 📌 Upload Form
        public IActionResult Create()
        {
            return View();
        }

        // 📌 Handle Upload
        [HttpPost]
        public async Task<IActionResult> Create(IFormFile file, string title)
        {
            if (file != null && file.Length > 0)
            {
                string uploads = Path.Combine(environment.WebRootPath, "images");

                if (!Directory.Exists(uploads))
                    Directory.CreateDirectory(uploads);

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string filePath = Path.Combine(uploads, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var photo = new Photo
                {
                    ImagePath = "/images/" + fileName,
                    Title = title
                };

                photoDbContext.Photos.Add(photo);
                await photoDbContext.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var photo = await photoDbContext.Photos.FindAsync(id);

            if (photo == null)
                return NotFound();

            return View(photo);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var photo = await photoDbContext.Photos.FindAsync(id);

            if (photo != null)
            {
                // DELETE IMAGE FILE
                var filePath = Path.Combine(environment.WebRootPath, photo.ImagePath.TrimStart('/'));

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                // DELETE FROM DATABASE
                photoDbContext.Photos.Remove(photo);
                await photoDbContext.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

    }
}

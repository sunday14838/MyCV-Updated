using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCV_App.Data;
using MyCV_App.Models;

namespace MyCV_App.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly PhotoDbContext photoDbContext;

        public ContactUsController(PhotoDbContext photoDbContext)
        {
            this.photoDbContext = photoDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ContactUs contactUs)
        {
            if (ModelState.IsValid)
            {
                photoDbContext.Contacts.Add(contactUs);
                await photoDbContext.SaveChangesAsync();

                ViewBag.Message = "Message sent successfully!";
            }

            return View();
        }
    }
}

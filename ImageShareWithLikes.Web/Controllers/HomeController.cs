using ImageShareWithLikes.Data;
using ImageShareWithLikes.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace ImageShareWithLikes.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly string _connectionString;
        private readonly IWebHostEnvironment _webhostenvironment;

        public HomeController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
            _webhostenvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {

            ImageRepository repo = new ImageRepository(_connectionString);

            return View(new IndexPageViewModel
            {
                Images = repo.GetAll().OrderByDescending(i => i.DateCreated).ToList()
                
            }) ;
        }


        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(IFormFile imagepath, string name)
        {

            var fileName = $"{Guid.NewGuid()}-{imagepath.FileName}";
            var fullImagePath = Path.Combine(_webhostenvironment.WebRootPath, "uploads", fileName);
            using FileStream fs = new FileStream(fullImagePath, FileMode.Create);
            imagepath.CopyTo(fs);
            ImageRepository repo = new ImageRepository(_connectionString);

            repo.AddImage(new()
            {
                Title = name,
                ImagePath = fileName,
                DateCreated = DateTime.Now,
                Likes = 0
            });

            return Redirect("/");

        }

        public IActionResult ViewImage(int id)
        {
            ImageRepository repo = new ImageRepository(_connectionString);
            ViewImageViewModel vm = new ViewImageViewModel();
            vm.Image = repo.GetById(id);
            vm.Ids = GetIdsFromSession();
            return View(vm);
        }

        [HttpPost]
        public void ImageLikes(int id)
        {
            ImageRepository repo = new ImageRepository(_connectionString);
            repo.AddLikes(id);
            List<int> sessionIds = GetIdsFromSession();
            sessionIds.Add(id);
            HttpContext.Session.Set("ids", sessionIds);
        }

        public IActionResult GetLikesById(int id)
        {
            ImageRepository repo = new ImageRepository(_connectionString);
            int numOfLikes = repo.GetLikes(id);
            return Json(numOfLikes);
        }

        public List<int> GetIdsFromSession()
        {
            List<int> sessionIds = HttpContext.Session.Get<List<int>>("ids");
            if (sessionIds == null)
            {
                return new List<int>();
            }
            return sessionIds;
        }

    }

    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            string value = session.GetString(key);

            return value == null ? default(T) :
                JsonSerializer.Deserialize<T>(value);
        }
    }
    }
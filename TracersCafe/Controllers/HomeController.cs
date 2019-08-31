using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TracersCafe.Data;
using TracersCafe.Models;

namespace TracersCafe.Controllers
{
    public class HomeController : Controller
    {
        Model Model { get; set; }
        public HomeController()
        {
            this.Model = new Model();
            this.Model.Database.EnsureCreated();
        }

        public IActionResult Index()
        {
            ViewData["title"] = "Home";
            return View();
        }

        [Route("add")]
        public IActionResult Add()
        {
            ViewData["title"] = "Add";
            return View();
        }

        [Route("remove")]
        public IActionResult Remove()
        {
            ViewData["title"] = "Remove";
            return View();
        }

        [Route("edit")]
        public IActionResult Edit([FromQuery] int id = -1)
        {
            var asdsa = Model.Information.ToList();
            ViewData["title"] = "Edit";
            return View(new EditModel()
            {
                Information = Model.Information.FirstOrDefault(x => x.ID == id) ?? Model.Information.First()
            });
        }

        [Route("search")]
        public IActionResult Search()
        {
            ViewData["title"] = "Search";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

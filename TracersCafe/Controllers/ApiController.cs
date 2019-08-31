using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TracersCafe.Data;

namespace TracersCafe.Controllers
{
    [Route("[controller]")]
    public class ApiController : Controller
    {
        Model Model { get; set; }
        public ApiController()
        {
            this.Model = new Model();
            this.Model.Database.EnsureCreated();
        }

        [Route("getall"), HttpGet]
        public string GetAll()
        {
            return JsonConvert.SerializeObject(Model.Information);
        }

        [Route("get/search"), HttpGet]
        public string SearchByName([FromQuery]string name)
        {
            var arr = name.Split(' ');
            return JsonConvert.SerializeObject(Model.Information.Where(x => arr.Any(y => x.Firstname.Contains(y)) || arr.Any(y => x.Surname.Contains(y))));
        }

        [Route("remove")]
        public IActionResult Remove([FromQuery]int id)
        {
            var data = Model.Information.First(x => x.ID == id);
            Model.Remove(data);
            Model.SaveChanges();

            //Can be improved by creating a "success" page instead of just returning to home
            ViewData["title"] = "Index";
            return Ok();
        }

        [Route("edit")]
        public IActionResult Edit(PersonInformation information)
        {
            if (information.Title == null) return BadRequest();
            var data = Model.Information.FirstOrDefault(x => x.ID == information.ID);
            data = information;

            Model.Update(data);
            Model.SaveChanges();

            ViewData["title"] = "Index";
            return View("/Views/Home/Index.cshtml");
        }

        [Route("add"), HttpPost]
        public IActionResult AddAsync(PersonInformation info)
        {
            Model.Information.Add(info);
            Model.SaveChanges();
            ViewData["title"] = "Index";
            return View("/Views/Home/Index.cshtml");
        }
    }
}
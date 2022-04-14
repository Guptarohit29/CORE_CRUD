using CORE12.db_context;
using CORE12.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CORE12.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            studentContext DB = new studentContext();
            List<MODELCLASS> empobj = new List<MODELCLASS>();
            var RES = DB.Table1s.ToList();
            foreach (var item in RES)
            {
                empobj.Add(new MODELCLASS
                {
                    Id= item.Id,
                    Name=item.Name,
                    Surname=item.Surname,
                    City=item.City,
                    Age =item.Age,
                    State=item.State
                });

            }
            return View(empobj);
        }
        public ActionResult Delete(int Id)
        {
            studentContext db = new studentContext();
            var del = db.Table1s.Where(m => m.Id == Id).First();
            db.Table1s.Remove(del);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult add_emp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult add_emp(MODELCLASS mod)
        {
            studentContext ent = new studentContext();
            Table1 tbl = new Table1();
            tbl.Id = mod.Id;
            tbl.Name = mod.Name;
            tbl.Surname = mod.Surname;
            tbl.City = mod.City;
            tbl.Age = mod.Age;
            tbl.State = mod.State;
            if (mod.Id == 0)
            {
                ent.Table1s.Add(tbl);
                ent.SaveChanges();
                return RedirectToAction("Privacy");
            }
            else
            {
                ent.Entry(tbl).State = EntityState.Modified;
                ent.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        public IActionResult Edit(int id)
        {
            studentContext ent = new studentContext();
            var edit = ent.Table1s.Where(m => m.Id == id).First();
            MODELCLASS mod = new MODELCLASS();
            mod.Id = edit.Id;
            mod.Name = edit.Name;
            mod.Surname = edit.Surname;
            mod.City = edit.City;
            mod.Age = edit.Age;
            mod.State = edit.State;
            return View("add_emp", mod);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

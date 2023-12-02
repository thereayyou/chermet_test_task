using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Diagnostics;
using testTask.Data;
using testTask.Models;
using testTask.Models.ViewModel;

namespace testTask.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly MVCDemoDbContext _mvcDemoDbContext;

        private readonly IWebHostEnvironment _appEnvironment;

        public EmployeeController(MVCDemoDbContext mvcDemoDbContext, IWebHostEnvironment appEnvironment)
        {
            _mvcDemoDbContext = mvcDemoDbContext;
            _appEnvironment = appEnvironment;
        }

        [HttpGet]

        public IActionResult Index(int page = 1, string searchByValue = "", string searchBy = "")
        {

            var models = GetList(page, searchByValue, searchBy);

            return View(models);

        }

        public EmployeeListView GetList(int page, string searchByValue, string searchBy)
        {

            int pageSize = 5;

            var viewModel = new EmployeeListView(_mvcDemoDbContext)
            { 
                SizePage = pageSize,

                CurrentPage = page,

                SearchByValue = searchByValue,

                SearchBy = searchBy,
            };

            return viewModel;
        }

        [HttpGet]

        public IActionResult Add()
        {

            ViewBag.Department = GetDepartmentsList();

            return View("Add");
        }

        [HttpPost]

        public IActionResult AddEmployee(Employee employee)
        {

            string error = employee.Validate();

            if (!string.IsNullOrEmpty(error))
            {

                TempData["error"] = error;

                return Add();
            }

            employee.Prepare(_appEnvironment, _mvcDemoDbContext);

            _mvcDemoDbContext.Employee.Add(employee);

            _mvcDemoDbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]

        public IActionResult Detail(int? id)
        {
            var employee = _mvcDemoDbContext.Employee.AsNoTracking().FirstOrDefault(e => e.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            ViewBag.Department = GetDepartmentsList();

            return View(employee);
        }

        [HttpPost]

        public IActionResult SaveChanges(Employee employee)
        {

            string error = employee.Validate();

            if(!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            employee.Prepare(_appEnvironment, _mvcDemoDbContext);

            _mvcDemoDbContext.Employee.Update(employee);

            _mvcDemoDbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]

        public IActionResult Delete(int? id)
        {
            var employee = _mvcDemoDbContext.Employee.Find(id);

            if (employee == null)
            {
                return NotFound();
            }

            _mvcDemoDbContext.Employee.Remove(employee);
            _mvcDemoDbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public SelectList GetDepartmentsList()
        {
            return new SelectList(_mvcDemoDbContext.Department, "Id", "Name");
        }

        public IActionResult StepBack()
        {
            return RedirectToAction("Index");
        }

    }
}

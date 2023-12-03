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

        /// <summary>
        ///  Отображение списка сотрудников.
        /// </summary>

        [HttpGet]
        public IActionResult Index(int page = 1, string searchByValue = "", string searchBy = "")
        {
       
            try
            {
                var models = GetList(page, searchByValue, searchBy);

                return View(models);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Получение списка сотрудников.
        /// </summary>
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
        /// <summary>
        /// Отображение добавления сотрудника.
        /// </summary>
        [HttpGet]
        public IActionResult Add()
        {
            try
            {
                ViewBag.Department = GetDepartmentsList();

                return View("Add");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Добавить сотрудника.
        /// </summary>
        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
            try
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
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;

                return Add();
            }
        }
        /// <summary>
        /// Отображение сотрудника по id.
        /// </summary>
        [HttpGet]
        public IActionResult Detail(int? id)
        {
            try
            {
                var employee = _mvcDemoDbContext.Employee.AsNoTracking().FirstOrDefault(e => e.Id == id);

                if (employee == null)
                {
                    return NotFound();
                }

                ViewBag.Department = GetDepartmentsList();

                return View(employee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// <summary>
        /// Обновление информации о сотруднике.
        /// </summary>
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

        /// <summary>
        /// Удаление сотрудника по id.
        /// </summary>
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

        /// <summary>
        /// Получения списка отделов.
        /// </summary>

        public SelectList GetDepartmentsList()
        {
            return new SelectList(_mvcDemoDbContext.Department, "Id", "Name");
        }

        /// <summary>
        ///  Метод для возвращения на главную страницу.
        /// </summary>

        public IActionResult StepBack()
        {
            return RedirectToAction("Index");
        }

    }
}

using Microsoft.EntityFrameworkCore;
using testTask.Data;

namespace testTask.Models.ViewModel
{
    public class EmployeeListView
    {

        private MVCDemoDbContext _context;

        public int SizePage { get; set; }

        public int CurrentPage { get; set; }

        public string SearchByValue { get; set; }

        public string SearchBy { get; set; }

        public EmployeeListView(MVCDemoDbContext context)
        {
            _context = context;
        }

        /// <summary>
        ///  Фильтрация работников из контекста
        /// </summary>
        private IQueryable<Employee> _Employees 
        { 
            get 
            {
                IQueryable<Employee> result = _context.Employee.Include(x => x.Department);

                if (!string.IsNullOrEmpty(SearchByValue)) 
                {
                    switch (SearchBy)
                    {
                        case "FullName":
                            result = result.Where(x => x.FullName.Contains(SearchByValue));
                            break;
                        case "Phone":
                            result = result.Where(x => x.Phone.StartsWith(SearchByValue));
                            break;
                        default:
                            break;
                    }
                }


                return result;
            }
        }

        /// <summary>
        ///  Возвращаем количество работников
        /// </summary>
        public int TotalPage 
        { 
            get 
            {
               return _Employees.Count();
            }
        }

        /// <summary>
        ///  Получаем общее количество страниц
        /// </summary>

        public int TotalPageCount 
        { 
            get { return (int)(Math.Ceiling((decimal)TotalPage / SizePage)); }  
        }

        /// <summary>
        ///  Получаем список работников для конкретной страницы
        /// </summary>

        public IQueryable<Employee> Employees 
        { 
            get
            {
               return _Employees.OrderBy(x => x.Id).Skip((CurrentPage - 1) * SizePage).Take(SizePage);
            }
        }

    }
}

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using VismaCase.Models;
using VismaCase.Services;

namespace VismaCase.Pages
{
    public class AddModel : PageModel
    {
        private readonly ILogger<AddModel> _logger;
        private readonly IEmployeeProvider _employeeProvider;
        private readonly IEmployeeValidator _employeeValidator;
        private readonly IPositionProvider _positionProvider;
        private readonly IPositionValidator _positionValidator;
        private readonly IWorkTaskProvider _workTaskProvider;
        private readonly IWorkTaskValidator _workTaskValidator;

        public Employee[] Employees { get; set; }
        public Position[] Positions { get; set; }
        public WorkTask[] WorkTasks { get; set; }

        public AddModel(ILogger<AddModel> logger,
                        IEmployeeProvider employeeProvider,
                        IEmployeeValidator employeeValidator,
                        IPositionProvider positionProvider,
                        IPositionValidator positionValidator,
                        IWorkTaskProvider workTaskProvider,
                        IWorkTaskValidator workTaskValidator)
        {
            _logger = logger;
            _employeeProvider = employeeProvider;
            _employeeValidator = employeeValidator;
            _positionProvider = positionProvider;
            _positionValidator = positionValidator;
            _workTaskProvider = workTaskProvider;
            _workTaskValidator = workTaskValidator;
        }

        public async void OnGet()
        {
            Employees = await _employeeProvider.GetAll();
            Positions = await _positionProvider.GetAll();
            WorkTasks = await _workTaskProvider.GetAll();
        }

        public async Task<IActionResult> AddEmployee()
        {
            var firstName = Request.Form["FirstName"].ToString();
            var lastName = Request.Form["LastName"].ToString();

            var employee = new Employee();
            employee.FirstName = firstName;
            employee.LastName = lastName;

            if (_employeeValidator.IsValid(employee).Length == 0)
            {
                await _employeeProvider.Add(employee);
            }
            return Page();
        }
        public async Task<IActionResult> AddPosition()
        {
            var name = Request.Form["PosName"];
            var employeeString = Request.Form["PosEmployee"].ToString();
            var employeeId = int.Parse(employeeString.Split('-')[0].Trim());
            var employee = await _employeeProvider.GetById(employeeId);

            var fromDateString = Request.Form["PosDateFrom"].ToString();
            var fromDate = DateTime.ParseExact(fromDateString, "yyy-MM-dd",
                                                System.Globalization.CultureInfo.InvariantCulture);

            var toDateString = Request.Form["PosDateTo"].ToString();
            var toDate = DateTime.ParseExact(toDateString, "yyy-MM-dd",
                                                System.Globalization.CultureInfo.InvariantCulture);

            var position = new Position();
            position.Name = name;
            position.Employee = employee;
            position.StartTime = fromDate;
            position.EndTime = toDate;

            if (_positionValidator.IsValid(position).Length == 0)
            {
                await _positionProvider.Add(position);
            }

            return Page();
        }
        public async Task<IActionResult> AddTask()
        {
            var name = Request.Form["TaskName"].ToString();
            var employeeString = Request.Form["TaskEmployee"].ToString();
            var employeeId = int.Parse(employeeString.Split('-')[0].Trim());
            var employee = await _employeeProvider.GetById(employeeId);

            var dateString = Request.Form["TaskDate"].ToString();
            var date = DateTime.ParseExact(dateString, "yyy-MM-dd",
                                                System.Globalization.CultureInfo.InvariantCulture);

            var task = new WorkTask();
            task.Name = name;
            task.Employee = employee;
            task.Date = date;

            if (_workTaskValidator.IsValid(task).Length == 0)
            {
                await _workTaskProvider.Add(task);
            }

            return Page();
        }
    }
}
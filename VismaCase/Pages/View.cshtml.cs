using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using VismaCase.Models;
using VismaCase.Services;

namespace VismaCase.Pages
{
    public class ViewModel : PageModel
    {
        private readonly ILogger<ViewModel> _logger;
        private readonly IEmployeeProvider _employeeProvider;
        private readonly IEmployeeValidator _employeeValidator;
        private readonly IPositionProvider _positionProvider;
        private readonly IPositionValidator _positionValidator;
        private readonly IWorkTaskProvider _workTaskProvider;
        private readonly IWorkTaskValidator _workTaskValidator;

        public Employee[] Employees { get; set; }
        public Position[] Positions { get; set; }
        public WorkTask[] WorkTasks { get; set; }

        public ViewModel(ILogger<ViewModel> logger,
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
    }
}
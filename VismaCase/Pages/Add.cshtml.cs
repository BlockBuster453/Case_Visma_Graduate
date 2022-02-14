using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace VismaCase.Pages
{
    public class AddModel : PageModel
    {
        private readonly ILogger<AddModel> _logger;
        private readonly IEmployeeProvider _employeeProvider;
        private readonly IPositionProvider _positionProvider;
        private readonly ITaskProvider _taskProvider;

        public AddModel(ILogger<AddModel> logger, IEmployeeProvider employeeProvider, IPositionProvider positionProvider, ITaskProvider taskProvider)
        {
            _logger = logger;
            _employeeProvider = employeeProvider;
            _positionProvider = positionProvider;
            _taskProvider = taskProvider;
        }
    }
}
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using VismaCase.Models;
using VismaCase.Services;

namespace VismaCase.Pages
{
    public class SearchModel : PageModel
    {
        private readonly ILogger<SearchModel> _logger;
        private readonly IEmployeeProvider _employeeProvider;
        private readonly IPositionProvider _positionProvider;
        private readonly IWorkTaskProvider _workTaskProvider;

        [ViewData]
        public Employee Employee { get; set; }
        [ViewData]
        public Position Position { get; set; }
        [ViewData]
        public WorkTask WorkTask { get; set; }

        public SearchModel(ILogger<SearchModel> logger,
                        IEmployeeProvider employeeProvider,
                        IPositionProvider positionProvider,
                        IWorkTaskProvider workTaskProvider)
        {
            _logger = logger;
            _employeeProvider = employeeProvider;
            _positionProvider = positionProvider;
            _workTaskProvider = workTaskProvider;
        }

        public async void OnGet() {
            var id = 0;
            var type = 0;
            try 
            {
                id = int.Parse(HttpContext.Request.Query["id"]);
                type = int.Parse(HttpContext.Request.Query["type"]);
            } catch (Exception)
            {
                return;
            }

            if (type == 1)
            {
                Employee = await _employeeProvider.GetById(id);
            } else if (type == 2)
            {
                Position = await _positionProvider.GetById(id);
            } else if (type == 3)
            {
                WorkTask = await _workTaskProvider.GetById(id);
            }
        }

        public IActionResult OnPost()
        {
            var id = Request.Form["Id"].ToString();
            var type = "0";
            if (Request.Form["Type"].ToString() == "Ansatte")
            {
                type = "1";
            } else if (Request.Form["Type"].ToString() == "Stillinger")
            {
                type = "2";
            } else if (Request.Form["Type"].ToString() == "Oppgaver")
            {
                type = "3";
            }
            return Redirect($"https://localhost:5001/Search?id={id}&type={type}");
        }
    }
}
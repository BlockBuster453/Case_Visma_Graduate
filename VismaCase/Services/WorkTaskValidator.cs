using System.Collections.Generic;
using VismaCase.Models;

namespace VismaCase.Services
{
    public class WorkTaskValidator : IWorkTaskValidator
    {
        public string[] IsValid(WorkTask task)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(task.Name))
            {
                errors.Add("Oppgaven må ha et navn");
            }

            if (task.EmployeeId == 0)
            {
                errors.Add("Oppgaven må ha en ansatt Id");
            }

            if (task.Date.Ticks == 0)
            {
                errors.Add("Oppgaven må ha en dato");
            }

            var errorArray = errors.ToArray();
            return errorArray;
        }
    }
}
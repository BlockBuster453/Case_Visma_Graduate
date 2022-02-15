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

            var errorArray = errors.ToArray();
            return errorArray;
        }
    }
}
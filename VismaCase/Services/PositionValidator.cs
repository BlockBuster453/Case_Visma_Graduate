using System.Collections.Generic;
using VismaCase.Models;

namespace VismaCase.Services
{
    public class PositionValidator : IPositionValidator
    {
        public string[] IsValid(Position position)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(position.Name))
            {
                errors.Add("Stillingen må ha et navn");
            }

            var errorArray = errors.ToArray();
            return errorArray;
        }
    }
}
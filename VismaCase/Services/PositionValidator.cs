using System;
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

            if (position.EmployeeId == 0)
            {
                errors.Add("Stillingen må ha Id til en ansatt");
            }

            if (position.StartTime.Ticks == 0)
            {
                errors.Add("Stillingen må ha en starttid");
            }

            if (position.EndTime.Ticks == 0)
            {
                errors.Add("Stillingen må ha en slutttid");
            }

            var errorArray = errors.ToArray();
            return errorArray;
        }
    }
}
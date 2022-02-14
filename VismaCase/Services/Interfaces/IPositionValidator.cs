using VismaCase.Models;

namespace VismaCase.Services
{
    public interface IPositionValidator
    {
        public string[] IsValid(Position position);
    }
}
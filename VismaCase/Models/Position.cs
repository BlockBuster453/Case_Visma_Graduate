using System;

namespace VismaCase.Models
{
    public class Position
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int Employee_Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
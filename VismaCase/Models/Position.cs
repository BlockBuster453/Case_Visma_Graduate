using System;

namespace VismaCase
{
    public class Position
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public Guid Employee_Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
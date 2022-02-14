using System;

namespace VismaCase.Models
{
    public class WorkTask
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public Employee Employee { get; set; }
        public DateTime Date { get; set; }
    }
}
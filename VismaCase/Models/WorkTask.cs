using System;

namespace VismaCase.Models
{
    public class WorkTask
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int Employee_Id { get; set; }
        public DateTime Date { get; set; }
    }
}
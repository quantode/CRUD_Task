namespace CRUD_Task.Models
{
    public class Employee
    {

        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Position { get; set; }
        public decimal Salary { get; set; }
    }
}


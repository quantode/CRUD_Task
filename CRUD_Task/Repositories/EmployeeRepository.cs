using CRUD_Task.DBContext;
using CRUD_Task.Interfaces;
using CRUD_Task.Models;
using Dapper;

namespace CRUD_Task.Repositories
{
    public class EmployeeRepository : IEmployee
    {
        private readonly DapperDbContext context;
        private string StoredProcedure = "spTestEmployee";
        public EmployeeRepository(DapperDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Employee>> Get()
        {
            var sql = "EXEC spGetAllEmployees"; // Use the stored procedure for fetching all employees
            using var connection = context.CreateConnection();
            return await connection.QueryAsync<Employee>(sql);
        }

        public async Task<Employee> Find(int id)
        {

            try
            {
                var sql = "EXEC spGetEmployeeById @Id";
                using var connection = context.CreateConnection();
                var employee = await connection.QueryFirstOrDefaultAsync<Employee>(sql, new { Id = id });


                return employee;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching employee with ID {id} :{ex.Message}");
                return null;
            }
        }

        public async Task<Employee> Add(Employee model)
        {
            var sql = "EXEC spInsertEmployee @Name, @Position, @Salary";
            using var connection = context.CreateConnection();

            var insertedId = await connection.ExecuteScalarAsync<int>(sql, new
            {
                model.Name,
                model.Position,
                model.Salary
            });

            model.Id = insertedId; // Set the newly inserted Id to the model
            return model;
        }

        public async Task<Employee> Update(Employee model)
        {
            var sql = @"
                UPDATE Employees
                SET Name = @Name,
                    Position = @Position,
                    Salary = @Salary
                WHERE Id = @Id";

            using var connection = context.CreateConnection();
            await connection.ExecuteAsync(sql, model);
            return model;
        }

        public async Task<Employee> Remove(Employee model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model), "Employee model cannot be null");
            }

            if (model.Id <= 0)
            {
                throw new ArgumentException("Employee ID is invalid.");
            }

            var sql = "DELETE FROM Employees WHERE Id = @Id";
            using var connection = context.CreateConnection();
            await connection.ExecuteAsync(sql, new { model.Id });
            return model;
        }




    }
}

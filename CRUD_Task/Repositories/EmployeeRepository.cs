using CRUD_Task.DBContext;
using CRUD_Task.Interfaces;
using CRUD_Task.Models;
using Dapper;

namespace CRUD_Task.Repositories
{
    public class EmployeeRepository : IEmployee
    {
        private readonly DapperDbContext context;
       
        public EmployeeRepository(DapperDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Employee>> Get()
        {
            var sql = "EXEC spGetAllEmployees"; 
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

            model.Id = insertedId; 
            return model;
        }

        public async Task<Employee> Update(Employee model)
        {

            var sql = "EXEC spUpdateEmployee @Id, @Name, @Position, @Salary";

            using var connection = context.CreateConnection();
            await connection.ExecuteAsync(sql, new
            {
                model.Id,
                model.Name,
                model.Position,
                model.Salary
            });
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

            var sql = "EXEC spDeleteEmployee @Id";
            using var connection = context.CreateConnection();
            await connection.ExecuteAsync(sql, new {
               Id= model.Id,
              
            });
            return model;
        }




    }
}

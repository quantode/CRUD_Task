
using CRUD_Task.Interfaces;
using CRUD_Task.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_Task.Controllers
{
   
    public class EmployeeController : Controller
    {
        private readonly IEmployee employeeRepository;

        public EmployeeController(IEmployee employee)
        {
           employeeRepository = employee;
        }


        [HttpGet]

        public async Task<IActionResult> Index()
        {
            var employees = await employeeRepository.Get();
            return View(employees);
        }

        [HttpGet]
        
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var employee = await employeeRepository.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee model)
        {
            if (ModelState.IsValid)
            {
                await employeeRepository.Add(model);
                return RedirectToAction(nameof(Index));
            }

         
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var employee = await employeeRepository.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Employee model)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var employee = await employeeRepository.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            await employeeRepository.Update(model);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
       
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var employee = await employeeRepository.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            Console.WriteLine($"Employee found: {employee.Name}");
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
       
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var employee = await employeeRepository.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
          
            Console.WriteLine($"Deleting Employee: {employee.Name}");

            await employeeRepository.Remove(employee);
            return RedirectToAction(nameof(Index));
        }
    }
}

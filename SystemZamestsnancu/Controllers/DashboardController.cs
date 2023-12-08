using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SystemZamestsnancu.Data;

namespace SystemZamestsnancu.Controllers;

public class DashboardController : Controller
{
    private ApplicationDbContext _database;
    private UserManager<IdentityUser> _manager;

    public DashboardController(ApplicationDbContext database, UserManager<IdentityUser> manager)
    {
        _database = database;
        _manager = manager;
    }

    [Authorize]
    public IActionResult Index()
    {
        List<Employee> employees;
        string userId = _manager.GetUserId(User);
        if (_database.UserRoles.Any(x => x.UserId == userId && x.RoleId == "admin"))
        {
            employees = _database.Employees.Include(x => x.Address).ToList();
        }
        else
        {
            employees = _database.Employees.ToList();
        }

        return View(employees);
    }

    [Authorize]
    [HttpGet]
    public IActionResult Edit(string employeeId)
    {
        string userId = _manager.GetUserId(User);
        if (!_database.UserRoles.Any(x => x.UserId == userId && x.RoleId == "admin"))
            return RedirectToAction("Index");
        var id = Guid.Parse(employeeId);
        var item = _database.Employees.Include(x => x.Address).FirstOrDefault(x => x.EmployeeId == id);
        if(item == null)
            return RedirectToAction("Index");
        return View(item);
    }
    
    [Authorize]
    [HttpPost]
    public IActionResult Edit(Employee employee)
    {
        string userId = _manager.GetUserId(User);
        if (!_database.UserRoles.Any(x => x.UserId == userId && x.RoleId == "admin"))
            return RedirectToAction("Index");
        var item = _database.Employees.Include(x => x.Address).FirstOrDefault(x => x.EmployeeId == employee.EmployeeId);
        item.Name = employee.Name;
        item.Surname = employee.Surname;
        item.Birthdate = employee.Birthdate;
        _database.Employees.Update(item);
        _database.SaveChanges();
        return RedirectToAction("Index");
    }
}
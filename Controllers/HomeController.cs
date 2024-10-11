using Microsoft.AspNetCore.Mvc;
using Project_SpenSmart.Models;
using Project_SpenSmart.Models.Data;
using System.Diagnostics;

namespace Project_SpenSmart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SpenSmartDBContext _Context;

        public HomeController(ILogger<HomeController> logger, SpenSmartDBContext context)
        {
            _logger = logger;
            _Context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Expenses()
        {
            var allExpenses = _Context.Expenses.ToList();

            var totalEcpenses = allExpenses.Sum(x => x.Value);
            ViewBag.Expenses = totalEcpenses;
            return View(allExpenses);
        }

        public IActionResult CreateEditExpense(int? id)
        {
            if(id != null)
            {
                var expenseInDb = _Context.Expenses.SingleOrDefault(expense => expense.Id == id);
                return View(expenseInDb);
            }
            

            return View();
        }

        public IActionResult DeleteExpense(int id)
        {
            var expenseInDb = _Context.Expenses.SingleOrDefault(expense => expense.Id == id);
            _Context.Expenses.Remove(expenseInDb);
            _Context.SaveChanges();
            return RedirectToAction("Expenses");
        }

        public IActionResult CreateEditExpenseForm(Expense model)
        {
            if (model.Id == 0)
            {
                // Create
                _Context.Expenses.Add(model);

            }
            else
            {
                // Editing
                _Context.Expenses.Update(model);
            }          
            _Context.SaveChanges();

            return RedirectToAction("Expenses");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

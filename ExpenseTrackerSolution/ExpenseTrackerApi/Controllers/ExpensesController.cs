using ExpenseTrackerApi.Models;
using ExpenseTrackerApi.Services;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/expenses")]
public class ExpensesController : ControllerBase{
    private readonly ExpenseManager manager;

    public ExpensesController(ExpenseManager manager){
        this.manager = manager;
        manager.Load();
    }

    [HttpPost]
    public IActionResult AddExpense(Expense expense){
        manager.AddExpense(expense);
        manager.Save();
        return Ok();
    }

    [HttpGet]
    public IActionResult GetAll(){
        return Ok(manager.GetAll());
    }

    [HttpGet("total")]
    public IActionResult GetTotal(){
        return Ok(manager.GetTotal());
    }

    [HttpGet("monthly-summary")]
    public IActionResult MonthlySummary(){
        return Ok(manager.GetMonthlySummary());
    }

    [HttpGet("category-totals")]
    public IActionResult CategoryTotals(){
        return Ok(manager.GetCategoryTotals());
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id){
        var deleted = manager.DeleteExpense(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }

}
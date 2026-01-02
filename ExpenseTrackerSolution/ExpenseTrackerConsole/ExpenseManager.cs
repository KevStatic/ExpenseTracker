namespace ExpenseTrackerConsole;
using System.Text.Json;

public class ExpenseManager{
    private List<Expense> expenses = new();

    public void Load(){
        if (File.Exists("expenses.json")){
            var json = File.ReadAllText("expenses.json");
            expenses = System.Text.Json.JsonSerializer
                .Deserialize<List<Expense>>(json) ?? new();
        }
    }

    public void Save(){
        var json = System.Text.Json.JsonSerializer.Serialize(expenses);
        File.WriteAllText("expenses.json", json);
    }

    public void AddExpense(Expense e){
        expenses.Add(e);
    }

    public List<Expense> GetAll(){
        return expenses;
    }

    public decimal GetTotal(){
        return expenses.Sum(e => e.Amount);
    }

    public Dictionary<string, decimal> GetMonthlySummary(){
        return expenses
            .GroupBy(e => e.Date.ToString("MMMM yyyy"))
            .ToDictionary(
                g => g.Key,
                g => g.Sum(e => e.Amount)
            );
    }

    public Dictionary<string, decimal> GetCategoryTotals(){
        return expenses
            .GroupBy(e => e.Category)
            .ToDictionary(
                g => g.Key,
                g => g.Sum(e => e.Amount)
            );
    }

    public bool DeleteExpense(Guid id){
        Load();
        var expense = expenses.FirstOrDefault(e => e.Id == id);
        if (expense == null) return false;

        expenses.Remove(expense);
        Save();
        return true;
    }
}
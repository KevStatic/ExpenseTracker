using System.Text.Json;

public class ExpenseManager{
    private List<Expense> expenses = new();

    public void AddExpense(Expense e){
        expenses.Add(e);
    }

    public List<Expense> GetAll(){
        return expenses;
    }

    public decimal GetTotal(){
        return expenses.Sum(e => e.Amount);
    }

    public void Save(){
        var json = JsonSerializer.Serialize(expenses);
        File.WriteAllText("expenses.json", json);
    }

    public void Load(){
        if (File.Exists("expenses.json")){
            var json = File.ReadAllText("expenses.json");
            expenses = JsonSerializer.Deserialize<List<Expense>>(json) ?? new();
        }
    }
}
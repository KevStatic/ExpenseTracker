using ExpenseTrackerConsole;
using System;

class Program{
    static void Main(){
        var manager = new ExpenseManager();
        manager.Load();

        while (true){
            Console.WriteLine("\n1. Add Expense");
            Console.WriteLine("2. View Expenses");
            Console.WriteLine("3. Total Spent");
            Console.WriteLine("4. Monthly Summary");
            Console.WriteLine("5. Category Summary");
            Console.WriteLine("6. Delete Expense");
            Console.WriteLine("7. Exit");
            
            Console.Write("Choice: ");

            var choice = Console.ReadLine();

            if (choice == "1"){
                string title;
                do {
                    Console.Write("Title: ");
                    title = Console.ReadLine();
                } while (string.IsNullOrWhiteSpace(title));

                decimal amount;
                while (true){
                    Console.Write("Amount: ");
                    if (decimal.TryParse(Console.ReadLine(), out amount) && amount >= 0){
                        break;
                    }
                    Console.WriteLine("Invalid amount. Please enter a valid number.");
                }

                string category;
                do {
                    Console.Write("Category: ");
                    category = Console.ReadLine();
                } while (string.IsNullOrWhiteSpace(category));

                manager.AddExpense(new Expense{
                    Title = title,
                    Amount = amount,
                    Category = category,
                    Date = DateTime.Now
                });

                manager.Save();
                Console.WriteLine("Expense added!");
            }
            else if (choice == "2"){
                foreach (var e in manager.GetAll()){
                    Console.WriteLine($"{e.Date:d} | {e.Title} | ₹{e.Amount} | {e.Category}");
                }
            }

            else if (choice == "3"){
                Console.WriteLine("Total Spent: ₹" + manager.GetTotal());
            }

            else if (choice == "4"){
                var summary = manager.GetMonthlySummary();

                if (summary.Count == 0){
                    Console.WriteLine("No expenses found.");
                    continue;
                }

                foreach (var item in summary){
                    Console.WriteLine($"{item.Key} → ₹{item.Value}");
                }
            }

            else if (choice == "5"){
                var totals = manager.GetCategoryTotals();

                if (totals.Count == 0){
                    Console.WriteLine("No expenses found.");
                    continue;
                }

                foreach (var item in totals){
                    Console.WriteLine($"{item.Key} → ₹{item.Value}");
                }
            }

            else if (choice == "6"){
                var expenses = manager.GetAll();

                if (expenses.Count == 0)
                {
                    Console.WriteLine("No expenses to delete.");
                    continue;
                }

                foreach (var e in expenses)
                {
                    Console.WriteLine($"{e.Id} | {e.Title} | ₹{e.Amount} | {e.Category}");
                }

                Console.Write("Enter Expense ID to delete: ");
                var input = Console.ReadLine();

                if (!Guid.TryParse(input, out var id))
                {
                    Console.WriteLine("Invalid ID format.");
                    continue;
                }

                var deleted = manager.DeleteExpense(id);

                if (deleted)
                    Console.WriteLine("Expense deleted successfully.");
                else
                    Console.WriteLine("Expense not found.");
            }

            else if (choice == "7"){
                break;
            }

            else{
                Console.WriteLine("Invalid choice");
            }
        }
    }
}
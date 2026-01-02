using System;

class Program
{
    static void Main()
    {
        var manager = new ExpenseManager();
        manager.Load();

        while (true)
        {
            Console.WriteLine("\n1. Add Expense");
            Console.WriteLine("2. View Expenses");
            Console.WriteLine("3. Total Spent");
            Console.WriteLine("4. Exit");
            Console.Write("Choice: ");

            var choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("Title: ");
                var title = Console.ReadLine();

                Console.Write("Amount: ");
                var amount = decimal.Parse(Console.ReadLine());

                Console.Write("Category: ");
                var category = Console.ReadLine();

                manager.AddExpense(new Expense
                {
                    Title = title,
                    Amount = amount,
                    Category = category,
                    Date = DateTime.Now
                });

                manager.Save();
                Console.WriteLine("Expense added!");
            }
            else if (choice == "2")
            {
                foreach (var e in manager.GetAll())
                {
                    Console.WriteLine($"{e.Date:d} | {e.Title} | ₹{e.Amount} | {e.Category}");
                }
            }
            else if (choice == "3")
            {
                Console.WriteLine("Total Spent: ₹" + manager.GetTotal());
            }
            else if (choice == "4")
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid choice");
            }
        }
    }
}
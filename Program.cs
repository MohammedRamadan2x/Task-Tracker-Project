using System.Threading.Tasks;

namespace Task_Tracker_project
{
    internal class Program
    {
        enum Options : short { Add = 1, View, Complete, Remove, Exit }

        static void Main(string[] args)
        {
            // Welcome user
            // -------------

            // 1. Add task.
            // 2. View all tasks.
            // 3. Mark task complete.
            // 4. Remove task.
            // 5. Exit.
            
            List<string> myTasks = new List<string>();

            Console.WriteLine("Welcome to my task tracker :-)");
            Console.WriteLine("------------------------------");

            StartProgram(myTasks);
            Console.ReadKey();
        }

        static void StartProgram(List<string> tasks)
        {
            while (true)
            {
                Options option = ReadUserChoice();

                switch (option)
                {
                    case Options.Add:
                        AddTask(tasks);
                        break;

                    case Options.Remove:
                        RemoveTask(tasks);
                        break;

                    case Options.Complete:
                        CompleteTask(tasks);
                        break;

                    case Options.View:
                        DisplayTasks(tasks);
                        break;

                    case Options.Exit:
                        EndProgram();
                        return;
                }

                if (AskToContinue()) Console.Clear();
                else return;
            }
        }

        static Options ReadUserChoice()
        {
            int choice;

            while (true)
            {
                Console.WriteLine("Enter your choice from 1 to 5:" +
                                  "\n1. Add" +
                                  "\n2. View" +
                                  "\n3. Complete" +
                                  "\n4. Remove" +
                                  "\n5. Exit");

                if (int.TryParse(Console.ReadLine(), out choice) && choice is >= 1 and <= 5)
                    return (Options)choice; // Correct input

                // Incorrect input
                PrintError("Invalid input! Please enter a number between 1 and 5.\n");
            }
        }

        static void AddTask(List<string> tasks)
        {
            Console.Write("→ Please enter task title: ");
            string? rawInput = Console.ReadLine();

            // validate input (null/whitespace).
            if (string.IsNullOrWhiteSpace(rawInput))
            {
                PrintError("→ Invalid input! Title cannot be empty.");
                return;
            }

            // normalize (trim) so "task" and "task " are treated as one.
            string newTask = rawInput.Trim();

            // If the item exists, I will not add it.
            if (tasks.Any(t => t.Equals(newTask, StringComparison.OrdinalIgnoreCase)))
            {
                PrintError("→ This task already exists.");
                return;
            }

            tasks.Add(newTask);
            PrintTheSuccessOfTheOperation("→ Task added to the list :-)");
        }

        static void CompleteTask(List<string> tasks)
        {
            if (TasksIsEmpty(tasks)) return;

            DisplayTasks(tasks);

            Console.Write("→ Please enter task number: ");
            int choice;

            if (ValidateTaskNumberChoice(tasks, out choice))
            {
                tasks[choice - 1] += " -- Completed";
                PrintTheSuccessOfTheOperation("→ The task has been completed :-)");
            } else PrintError("Invalid input!");
        }

        static void RemoveTask(List<string> tasks)
        {
            if (TasksIsEmpty(tasks)) return;

            DisplayTasks(tasks);

            Console.Write("→ Please enter task number to delete: ");
            int choice;

            if (ValidateTaskNumberChoice(tasks, out choice))
            {
                tasks.RemoveAt(choice - 1);
                PrintTheSuccessOfTheOperation("→ Task removed from the list :-)");
            } else PrintError("Invalid input!");
        }

        static void DisplayTasks(List<string> tasks)
        {
            if (TasksIsEmpty(tasks)) return;

            short counter = 1;

            Console.WriteLine("\n→ Your Tasks");
            Console.WriteLine("---------------------");

            foreach (var task in tasks)
            {
                if (task.Contains("-- Completed", StringComparison.OrdinalIgnoreCase))
                {
                    Console.ForegroundColor = ConsoleColor.Green;  
                    Console.WriteLine($"{counter++}) {task}");
                    Console.ResetColor();
                }
                else Console.WriteLine($"{counter++}) {task}");
            }
        }

        static bool TasksIsEmpty(List<string> tasks)
        {
            if (tasks.Count == 0)
            {
                PrintError("→ There are no tasks!");
                return true;
            }
            return false;
        }

        static bool ValidateTaskNumberChoice(List<string> tasks, out int choice)
        {
            return int.TryParse(Console.ReadLine(), out choice) && choice > 0 && choice <= tasks.Count;
        }

        static void PrintTheSuccessOfTheOperation(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        static void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        static void EndProgram()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\a\n\n\n\n\n\n\n\n\n\n\n\n\n\n\t\t\t\t\t\tThanks for using Task Tracker :-)");
            Console.ResetColor();
        }

        static bool AskToContinue()
        {
            Console.WriteLine("\nDo you want to continue?" +
                              "\nWrite anything if you want," +
                              " or 'No' if you not:");

            string? answer = Console.ReadLine();

            if (answer?.Trim().ToLower() == "no")
            {
                EndProgram();
                return false;
            }

            return true;
        }
    }
}